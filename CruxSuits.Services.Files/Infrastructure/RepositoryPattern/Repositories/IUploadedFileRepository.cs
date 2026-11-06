using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories
{
    public interface IUploadedFileRepository : IFilesEntityRepository<Guid, UploadedFile>
    {
        Task<UploadedFile?> GetForDownloadAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
