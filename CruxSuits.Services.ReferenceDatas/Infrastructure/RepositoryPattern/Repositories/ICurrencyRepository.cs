using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public interface ICurrencyRepository : IReferenceDataEntityRepository<string, Currency>
    {
        Task<List<TResult>> GetAllLocalizedAsync<TResult>(string languageId, CancellationToken cancellationToken = default);
    }
}
