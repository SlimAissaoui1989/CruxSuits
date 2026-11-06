using PersonEntity = CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities.Person;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class PersonRepository : PersonEntityRepository<Guid, PersonEntity>, IPersonRepository
    {
        public PersonRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
