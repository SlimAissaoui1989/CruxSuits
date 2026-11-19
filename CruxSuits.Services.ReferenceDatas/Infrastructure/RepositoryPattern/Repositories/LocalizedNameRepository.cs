using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public class LocalizedNameRepository : ReferenceDataEntityRepository<long, LocalizedName>, ILocalizedNameRepository
    {
        public LocalizedNameRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
