using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;

namespace CruxSuits.Services.ReferenceDatas.Services;

public interface IAdministrativeAreaService
{
    Task<List<AdministrativeAreaDetailsResponse>> GetAllAsync(string countryId, CancellationToken cancellationToken = default);
    Task<AdministrativeAreaDetailsResponse> GetByCodeAsync(string countryId, string areaCode, CancellationToken cancellationToken = default);
    Task<AdministrativeAreaDetailsResponse> CreateAsync(string countryId, AdministrativeAreaRequest request, CancellationToken cancellationToken = default);
    Task RemoveAsync(string countryId, string areaCode, CancellationToken cancellationToken = default);
    // Stages deletion in the shared unit of work. The caller saves the changes.
    Task RemoveByCountryIdAsync(string countryId, CancellationToken cancellationToken = default);
}
