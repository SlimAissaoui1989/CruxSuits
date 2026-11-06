using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories
{
    public class CurrencyRepository : ReferenceDataEntityRepository<string, Currency>, ICurrencyRepository
    {
        public CurrencyRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
