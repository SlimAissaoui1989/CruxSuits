using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class PersonIdentityRepository : PersonEntityRepository<int, PersonIdentity>, IPersonIdentityRepository
    {
        public PersonIdentityRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
