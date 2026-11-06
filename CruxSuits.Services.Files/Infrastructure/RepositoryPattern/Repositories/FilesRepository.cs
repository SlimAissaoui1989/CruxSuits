using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories
{
    public class FilesRepository<TKey, TEntity> : Repository<IFilesUnitOfWork, TKey, TEntity>, IFilesEntityRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
        public FilesRepository(IFilesUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
