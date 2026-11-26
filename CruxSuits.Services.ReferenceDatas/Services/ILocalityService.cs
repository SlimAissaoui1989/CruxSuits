using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;

namespace CruxSuits.Services.ReferenceDatas.Services;

public interface ILocalityService
{
    Task<List<LocalitySuggestionResponse>> AutocompleteAsync(string query, string languageId, CancellationToken cancellationToken = default);
    Task<List<LocalityDetailsResponse>> GetAllAsync(string countryId, string areaCode, CancellationToken cancellationToken = default);
    Task<LocalityDetailsResponse> GetByCodeAsync(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken = default);
    Task<LocalityDetailsResponse> CreateAsync(string countryId, string areaCode, LocalityRequest request, CancellationToken cancellationToken = default);
    Task RemoveAsync(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken = default);
    // Stages deletion in the shared unit of work. The caller saves the changes.
    Task RemoveByAdministrativeAreaIdAsync(string administrativeAreaId, CancellationToken cancellationToken = default);
}
