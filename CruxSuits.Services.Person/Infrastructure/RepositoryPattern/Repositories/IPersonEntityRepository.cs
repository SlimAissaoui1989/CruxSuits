using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Services.Person.Infrastructure.EntityFramework;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public interface IPersonEntityRepository<TKey, TEntity> : IRepository<IPersonUnitOfWork, TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
    }
}
