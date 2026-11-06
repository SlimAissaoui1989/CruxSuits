using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class PersonIdentityTypeRepository : PersonEntityRepository<string, PersonIdentityType>, IPersonIdentityTypeRepository
    {
        public PersonIdentityTypeRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
