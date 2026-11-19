using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using System.Threading;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public interface ICountryRepository : IReferenceDataEntityRepository<string, Country>
    {
        Task<List<TResult>> GetAllLocalizedAsync<TResult>(string languageId, CancellationToken cancellationToken = default);
    }
}
