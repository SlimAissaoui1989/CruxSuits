using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories
{
    public interface IUploadedFileRepository : IFilesEntityRepository<Guid, UploadedFile>
    {
        Task<UploadedFile?> GetPublicForDownloadAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UploadedFile?> GetPrivateForDownloadAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
