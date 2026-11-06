using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class IBANRepository : PersonEntityRepository<int, IBAN>, IIBANRepository
    {
        public IBANRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
