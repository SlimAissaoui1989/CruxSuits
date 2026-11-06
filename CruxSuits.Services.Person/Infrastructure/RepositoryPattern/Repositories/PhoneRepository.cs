using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class PhoneRepository : PersonEntityRepository<int, Phone>, IPhoneRepository
    {
        public PhoneRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
