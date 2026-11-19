using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class IBANRepository : PersonEntityRepository<int, IBAN>, IIBANRepository
    {
        public IBANRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
