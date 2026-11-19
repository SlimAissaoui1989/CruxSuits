using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public interface ICountryRepository : IReferenceDataEntityRepository<string, Country>
    {
    }
}
