using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;

namespace CruxSuits.Services.ReferenceDatas.Services;

public interface ICountryNameService
{
    Task<List<CountryNameResponse>> GetAllAsync(string countryId, CancellationToken cancellationToken = default);
    Task<CountryNameResponse> GetByLanguageAsync(string countryId, string languageId, CancellationToken cancellationToken = default);
    Task<CountryNameResponse> CreateAsync(string countryId, CountryNameRequest request, CancellationToken cancellationToken = default);
    Task<CountryNameResponse> UpdateAsync(string countryId, string languageId, CountryNameUpdateRequest request, CancellationToken cancellationToken = default);
    Task RemoveAsync(string countryId, string languageId, CancellationToken cancellationToken = default);
}
