using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class PersonIdentityTypeRepository : PersonEntityRepository<string, PersonIdentityType>, IPersonIdentityTypeRepository
    {
        public PersonIdentityTypeRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
