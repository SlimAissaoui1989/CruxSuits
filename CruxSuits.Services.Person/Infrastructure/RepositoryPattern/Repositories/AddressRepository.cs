using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class AddressRepository : PersonEntityRepository<int, Address>, IAddressRepository
    {
        public AddressRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
