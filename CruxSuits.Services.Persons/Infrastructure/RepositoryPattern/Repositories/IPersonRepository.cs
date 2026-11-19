using PersonEntity = CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities.Person;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public interface IPersonRepository : IPersonBaseRepository<PersonEntity>
    {
    }
}
