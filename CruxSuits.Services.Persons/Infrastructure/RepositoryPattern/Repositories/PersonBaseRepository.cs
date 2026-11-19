using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class PersonBaseRepository<TEntity> : PersonEntityRepository<Guid, TEntity>, IPersonBaseRepository<TEntity>
        where TEntity : PersonBase
    {
        public PersonBaseRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Id = Guid.NewGuid();
            return base.AddAsync(entity, cancellationToken);
        }
    }
}
