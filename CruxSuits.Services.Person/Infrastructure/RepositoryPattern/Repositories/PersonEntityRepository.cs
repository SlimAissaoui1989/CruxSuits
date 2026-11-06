using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class PersonEntityRepository<TKey, TEntity> : Repository<IPersonUnitOfWork, TKey, TEntity>, IPersonEntityRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
        public PersonEntityRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
