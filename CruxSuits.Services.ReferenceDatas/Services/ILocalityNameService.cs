using CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;

namespace CruxSuits.Services.ReferenceDatas.Services;

public interface ILocalityNameService
{
    Task<List<LocalityNameResponse>> GetAllAsync(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken = default);
    Task<LocalityNameResponse> GetByLanguageAsync(string countryId, string areaCode, string localityCode, string languageId, CancellationToken cancellationToken = default);
    Task<LocalityNameResponse> CreateAsync(string countryId, string areaCode, string localityCode, LocalityNameRequest request, CancellationToken cancellationToken = default);
    Task<LocalityNameResponse> UpdateAsync(string countryId, string areaCode, string localityCode, string languageId, LocalityNameUpdateRequest request, CancellationToken cancellationToken = default);
    Task RemoveAsync(string countryId, string areaCode, string localityCode, string languageId, CancellationToken cancellationToken = default);
}
