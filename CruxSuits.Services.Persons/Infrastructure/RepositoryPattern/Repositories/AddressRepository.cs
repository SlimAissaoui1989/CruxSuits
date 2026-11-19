using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class AddressRepository : PersonEntityRepository<int, Address>, IAddressRepository
    {
        public AddressRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
