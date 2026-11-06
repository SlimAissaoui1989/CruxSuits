using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class PhoneTypeRepository : PersonEntityRepository<string, PhoneType>, IPhoneTypeRepository
    {
        public PhoneTypeRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
