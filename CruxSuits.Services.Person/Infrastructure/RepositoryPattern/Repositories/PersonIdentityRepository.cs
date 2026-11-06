using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class PersonIdentityRepository : PersonEntityRepository<int, PersonIdentity>, IPersonIdentityRepository
    {
        public PersonIdentityRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
