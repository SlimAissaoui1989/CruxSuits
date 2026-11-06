using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories
{
    public interface ICountryRepository : IReferenceDataEntityRepository<string, Country>
    {
    }
}
