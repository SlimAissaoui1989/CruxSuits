using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories
{
    public class UploadedFileTraceRepository : FilesRepository<long, UploadedFileTrace>, IUploadedFileTraceRepository
    {
        public UploadedFileTraceRepository(IFilesUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            UseTrace = false;
        }

        public override Task<UploadedFileTrace> AddAsync(UploadedFileTrace entity, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(entity);
        }

        public override Task<UploadedFileTrace> UpdateAsync(UploadedFileTrace entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<UploadedFileTrace>> UpdateRangeAsync(IEnumerable<UploadedFileTrace> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveAsync(UploadedFileTrace entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveRangeAsync(CancellationToken cancellationToken = default, params UploadedFileTrace[] entities)
        {
            throw new NotImplementedException();
        }
    }
}
