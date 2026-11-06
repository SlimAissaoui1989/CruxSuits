using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories
{
    public interface IFilesEntityRepository<TKey, TEntity> : IRepository<IFilesUnitOfWork, TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
    }
}
