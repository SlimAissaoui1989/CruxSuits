using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories
{
    public class LocalizedNameRepository : ReferenceDataEntityRepository<long, LocalizedName>, ILocalizedNameRepository
    {
        public LocalizedNameRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
