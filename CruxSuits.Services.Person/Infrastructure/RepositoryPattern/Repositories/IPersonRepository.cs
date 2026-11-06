using PersonEntity = CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities.Person;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public interface IPersonRepository : IPersonEntityRepository<Guid, PersonEntity>
    {
    }
}
