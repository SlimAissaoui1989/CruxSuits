using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class PhoneRepository : PersonEntityRepository<int, Phone>, IPhoneRepository
    {
        public PhoneRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
