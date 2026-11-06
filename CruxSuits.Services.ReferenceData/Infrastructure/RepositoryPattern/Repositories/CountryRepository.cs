using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories
{
    public class CountryRepository : ReferenceDataEntityRepository<string, Country>, ICountryRepository
    {
        public CountryRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
