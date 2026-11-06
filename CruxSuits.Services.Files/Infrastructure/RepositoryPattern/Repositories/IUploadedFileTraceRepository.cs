using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories
{
    public interface IUploadedFileTraceRepository : IFilesEntityRepository<long, UploadedFileTrace>
    {
    }
}
