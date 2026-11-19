using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public class CountryNameRepository : ReferenceDataEntityRepository<int, CountryName>, ICountryNameRepository
    {
        public CountryNameRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
