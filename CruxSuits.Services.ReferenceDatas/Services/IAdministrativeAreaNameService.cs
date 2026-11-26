using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeAreaName;

namespace CruxSuits.Services.ReferenceDatas.Services;

public interface IAdministrativeAreaNameService
{
    Task<List<AdministrativeAreaNameResponse>> GetAllAsync(string countryId, string areaCode, CancellationToken cancellationToken = default);
    Task<AdministrativeAreaNameResponse> GetByLanguageAsync(string countryId, string areaCode, string languageId, CancellationToken cancellationToken = default);
    Task<AdministrativeAreaNameResponse> CreateAsync(string countryId, string areaCode, AdministrativeAreaNameRequest request, CancellationToken cancellationToken = default);
    Task<AdministrativeAreaNameResponse> UpdateAsync(string countryId, string areaCode, string languageId, AdministrativeAreaNameUpdateRequest request, CancellationToken cancellationToken = default);
    Task RemoveAsync(string countryId, string areaCode, string languageId, CancellationToken cancellationToken = default);
}
