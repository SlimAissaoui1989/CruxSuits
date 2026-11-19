using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public interface IPersonBaseRepository<TEntity> : IPersonEntityRepository<Guid, TEntity>
        where TEntity : PersonBase
    {
    }
}
