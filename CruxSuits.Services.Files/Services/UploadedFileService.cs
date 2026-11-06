using CruxIT.Library.Exceptions;
using AutoMapper;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.Files.DTOs;
using CruxSuits.Shared.DTOs.Files;

namespace CruxSuits.Services.Files.Services
{
    public class UploadedFileService : IUploadedFileService
    {
        private readonly IFilesUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public UploadedFileService(IFilesUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitofwork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UploadFileResponse> UploadAsync(
            UploadFileRequest request,
            CancellationToken cancellationToken = default)
        {
            var file = request.File;

            await using var inputStream = file.OpenReadStream();
            using var contentStream = new MemoryStream();
            await inputStream.CopyToAsync(contentStream, cancellationToken);

            var uploadedFile = _mapper.Map<UploadedFile>(request);
            uploadedFile.Content = contentStream.ToArray();

            var savedFile = await _unitofwork.UploadedFile.AddAsync(uploadedFile, cancellationToken);
            await _unitofwork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UploadFileResponse>(savedFile);
        }

        public async Task<FileDownloadResult> DownloadAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var uploadedFile = await _unitofwork.UploadedFile.GetForDownloadAsync(id, cancellationToken);
            if (uploadedFile is null)
            {
                throw new CxException(CxExceptionTypes.NotFound);
            }

            return new FileDownloadResult(
                new MemoryStream(uploadedFile.Content!, writable: false),
                uploadedFile.FileType,
                uploadedFile.OriginalFileName);
        }

        public async Task<UploadFileResponse> UpdateAsync(
            Guid id,
            UpdateFileRequest request,
            CancellationToken cancellationToken = default)
        {
            UploadedFile? existingEntity = await _unitofwork.UploadedFile.GetByIdOrDefaultAsync(id);
            if (existingEntity is null)
                throw new CxException(CxExceptionTypes.NotFound);

            UploadedFile MergingEntity = _mapper.Map(request, existingEntity);

            var updatedFile = await _unitofwork.UploadedFile.UpdateAsync(MergingEntity, cancellationToken);
            await _unitofwork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UploadFileResponse>(updatedFile);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            UploadedFile? entity = await _unitofwork.UploadedFile.GetByIdOrDefaultAsync(id);
            if (entity is null)
                throw new CxException(CxExceptionTypes.NotFound);

            await _unitofwork.UploadedFile.RemoveAsync(entity, cancellationToken);

            await _unitofwork.SaveChangesAsync(cancellationToken);
        }
    }
}
