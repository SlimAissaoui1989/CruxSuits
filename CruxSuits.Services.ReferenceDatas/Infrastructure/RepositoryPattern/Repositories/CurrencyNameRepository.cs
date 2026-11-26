using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public class CurrencyNameRepository : ReferenceDataEntityRepository<int, CurrencyName>, ICurrencyNameRepository
    {
        public CurrencyNameRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
