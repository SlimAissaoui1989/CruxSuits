using CruxIT.Library;
using CruxIT.Library.DataToolkit.EntityFramework.Enums;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Interceptors;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Linq.Expressions;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories
{
    public class UploadedFileRepository : FilesRepository<Guid, UploadedFile>, IUploadedFileRepository
    {
        private readonly string _fileStorageRoot;
        private readonly PendingFileStorageOperations _pendingFileStorageOperations;

        public UploadedFileRepository(
            IFilesUnitOfWork unitOfWork,
            IConfiguration configuration,
            IWebHostEnvironment environment,
            PendingFileStorageOperations pendingFileStorageOperations)
            : base(unitOfWork)
        {
            _pendingFileStorageOperations = pendingFileStorageOperations;
            var configuredRoot = configuration["FileStorage:RootPath"];
            _fileStorageRoot = string.IsNullOrWhiteSpace(configuredRoot)
                ? Path.Combine(environment.ContentRootPath, "UploadedFiles")
                : Path.IsPathRooted(configuredRoot)
                    ? configuredRoot
                    : Path.Combine(environment.ContentRootPath, configuredRoot);
        }

        public override async Task<UploadedFile> AddAsync(UploadedFile entity, CancellationToken cancellationToken = default)
        {
            var content = entity.Content
                ?? throw new InvalidOperationException("File content is required.");

            entity.Id = Guid.NewGuid();
            entity.Extension = Path.GetExtension(entity.OriginalFileName);
            entity.FileType = string.IsNullOrEmpty(entity.Extension)
                ? "application/octet-stream"
                : CxMimeTypeHelper.GetContentType(entity.Extension);
            entity.SystemFileName = CreateSystemFileName(entity);
            entity.SizeInBytes = content.LongLength;

            if (entity.IsSavedInDatabase)
            {
                return await base.AddAsync(entity, cancellationToken);
            }

            var contextId = UnitOfWork.DbContext.ContextId.InstanceId;
            string? temporaryPath = null;

            try
            {
                var stagedFile = await StageFileAsync(entity, content, cancellationToken);
                temporaryPath = stagedFile.TemporaryPath;
                entity.Content = null;
                entity.AbsolutePath = stagedFile.DestinationPath;

                return await base.AddAsync(entity, cancellationToken);
            }
            catch
            {
                if (temporaryPath is not null)
                    _pendingFileStorageOperations.Remove(contextId, temporaryPath);
                entity.AbsolutePath = null;
                entity.Content = content;
                throw;
            }
        }

        public override async Task<UploadedFile> UpdateAsync(UploadedFile entity, CancellationToken cancellationToken = default)
        {
            var context = UnitOfWork.DbContext;

            var existingFile = await DbSet.FindAsync(entity.Id, cancellationToken) ?? throw NotFoundByIdException(entity.Id);

            var storageChanged = existingFile.IsSavedInDatabase != entity.IsSavedInDatabase;
            var requestedStorageLocation = entity.IsSavedInDatabase;

            var contextId = context.ContextId.InstanceId;
            string? temporaryPath = null;
            string? previousDiskPath = null;

            try
            {
                if (storageChanged)
                {
                    await NormalizeStorageContentAsync(existingFile, cancellationToken);
                    if (requestedStorageLocation)
                    {
                        previousDiskPath = GetRequiredAbsolutePath(existingFile);
                        _pendingFileStorageOperations.RegisterDeleteOnCommit(contextId, previousDiskPath);

                        existingFile.IsSavedInDatabase = true;
                        existingFile.AbsolutePath = null;
                    }
                    else
                    {
                        var content = existingFile.Content
                            ?? throw new InvalidOperationException($"Uploaded file '{existingFile.Id}' is marked as stored in the database, but its content is missing.");

                        var stagedFile = await StageFileAsync(existingFile, content, cancellationToken);
                        temporaryPath = stagedFile.TemporaryPath;
                        existingFile.Content = null;
                        existingFile.AbsolutePath = stagedFile.DestinationPath;
                        existingFile.IsSavedInDatabase = false;
                    }
                }
                existingFile.Description = entity.Description;
                existingFile.IsPublic = entity.IsPublic;

                return await base.UpdateAsync(existingFile, cancellationToken);
            }
            catch
            {
                if (previousDiskPath is not null)
                {
                    _pendingFileStorageOperations.CancelDeleteOnCommit(
                        contextId,
                        previousDiskPath);
                }

                if (temporaryPath is not null)
                {
                    _pendingFileStorageOperations.Remove(contextId, temporaryPath);
                }

                throw;
            }
        }

        public override async Task RemoveAsync(UploadedFile entity, CancellationToken cancellationToken = default)
        {
            var context = UnitOfWork.DbContext;
            var existingFile = await DbSet.FindAsync(entity.Id, cancellationToken) ?? throw NotFoundByIdException(entity.Id);
            var contextId = context.ContextId.InstanceId;
            string? diskPath = null;

            try
            {
                if (!existingFile.IsSavedInDatabase)
                {
                    diskPath = GetRequiredAbsolutePath(existingFile);
                    _pendingFileStorageOperations.RegisterDeleteOnCommit(contextId, diskPath);
                }

                await base.RemoveAsync(entity, cancellationToken);
            }
            catch
            {
                if (diskPath is not null)
                    _pendingFileStorageOperations.CancelDeleteOnCommit(contextId, diskPath);

                throw;
            }
        }

        public async Task<UploadedFile?> GetForDownloadAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var uploadedFile = await this.AsNoTracking()
                .GetFirstOrDefaultAsync(file => file.Id == id, cancellationToken);

            if (uploadedFile is null) return null;
            await NormalizeStorageContentAsync(uploadedFile, cancellationToken);
            return uploadedFile;
        }

        private async Task NormalizeStorageContentAsync(UploadedFile entity, CancellationToken cancellationToken = default)
        {
            if (entity.IsSavedInDatabase)
            {
                if (entity.Content is null)
                    throw new InvalidOperationException($"Uploaded file '{entity.Id}' is marked as stored in the database, but its content is missing.");

                return;
            }

            var filePath = GetRequiredAbsolutePath(entity);

            if (!File.Exists(filePath))
                throw new InvalidOperationException($"Uploaded file '{entity.Id}' is missing from disk at the recorded path '{filePath}'.");

            entity.Content = await File.ReadAllBytesAsync(filePath, cancellationToken);
        }

        private async Task<StagedFile> StageFileAsync(
            UploadedFile entity,
            byte[] content,
            CancellationToken cancellationToken)
        {
            var directoryPath = Path.Combine(_fileStorageRoot, CreateUserFolderName());
            var destinationPath = Path.GetFullPath(Path.Combine(directoryPath, entity.SystemFileName));
            var temporaryPath = $"{destinationPath}.pending";
            var contextId = UnitOfWork.DbContext.ContextId.InstanceId;

            try
            {
                Directory.CreateDirectory(directoryPath);
                await File.WriteAllBytesAsync(temporaryPath, content, cancellationToken);
                _pendingFileStorageOperations.Register(contextId, temporaryPath, destinationPath);
                return new StagedFile(temporaryPath, destinationPath);
            }
            catch
            {
                if (File.Exists(temporaryPath))
                    File.Delete(temporaryPath);
                throw;
            }
        }

        private static string GetRequiredAbsolutePath(UploadedFile entity)
        {
            if (string.IsNullOrWhiteSpace(entity.AbsolutePath))
            {
                throw new InvalidOperationException(
                    $"Uploaded file '{entity.Id}' is stored on disk, but its path is missing.");
            }

            return Path.GetFullPath(entity.AbsolutePath);
        }

        private sealed record StagedFile(string TemporaryPath, string DestinationPath);

        private string CreateSystemFileName(UploadedFile entity)
        {
            var originalName = entity.OriginalFileName.Replace('\\', '/');
            originalName = Path.GetFileNameWithoutExtension(originalName);
            originalName = SanitizeFileNamePart(originalName);

            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfffffff", CultureInfo.InvariantCulture);

            var extensionValue = entity.Extension;
            var extension = Truncate(SanitizeFileNamePart(extensionValue.Trim().TrimStart('.')), 10);
            var extensionSuffix = string.IsNullOrEmpty(extension) ? string.Empty : $".{extension}";
            var suffix = $"_{timestamp}{extensionSuffix}";
            var maxOriginalNameLength = Math.Max(1, 255 - suffix.Length);

            return $"{Truncate(string.IsNullOrWhiteSpace(originalName) ? "file" : originalName, maxOriginalNameLength)}{suffix}";
        }

        private string CreateUserFolderName()
        {
            string currentUserName = UnitOfWork.CurrentUserName!;
            var userName = SanitizeFileNamePart(currentUserName);

            var userId = UnitOfWork.CurrentUserId!;
            return $"{userName}_{userId}";
        }

        private static string SanitizeFileNamePart(string value)
        {
            const string invalidCharacters = "<>:\"/\\|?*";

            return new string(value
                .Select(character => char.IsControl(character) || invalidCharacters.Contains(character) ? '_' : character)
                .ToArray())
                .Trim()
                .Trim('.');
        }

        private static string Truncate(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value[..maxLength];
        }

        public override Task<List<UploadedFile>> GetAllAsync(Expression<Func<UploadedFile, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected async override void AddTrace(UploadedFile entity, ActionTypes actionType, CancellationToken cancellationToken = default)
        {
            UploadedFileTrace trace = new()
            {
                ActionDate = DateTime.UtcNow,
                ActionType = actionType,
                Description = entity.Description,
                DigitalFileId = entity.Id,
                Extension = entity.Extension,
                OriginalFileName = entity.OriginalFileName,
                SystemFileName = entity.SystemFileName,
                FileType = entity.FileType,
                UserId = UnitOfWork.CurrentUserId!.Value,
                UserName = UnitOfWork.CurrentUserName!,
            };

            await UnitOfWork.UploadedFileTrace.AddAsync(trace, cancellationToken);
        }
    }
}
