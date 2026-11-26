using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public interface ILocalityRepository : IReferenceDataEntityRepository<string, Locality>
    {
        string BuildId(string administrativeAreaId, string localityCode);
        Task<List<LocalitySuggestionResponse>> AutocompleteAsync(string query, string languageId, CancellationToken cancellationToken = default);
        Task<bool> ExistsIncludingDeletedAsync(string administrativeAreaId, string localityCode, CancellationToken cancellationToken = default);
    }
}
