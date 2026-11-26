using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public class AdministrativeAreaNameRepository : ReferenceDataEntityRepository<int, AdministrativeAreaName>, IAdministrativeAreaNameRepository
    {
        public AdministrativeAreaNameRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
