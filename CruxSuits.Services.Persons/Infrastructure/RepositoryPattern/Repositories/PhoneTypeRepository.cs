using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class PhoneTypeRepository : PersonEntityRepository<string, PhoneType>, IPhoneTypeRepository
    {
        public PhoneTypeRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
