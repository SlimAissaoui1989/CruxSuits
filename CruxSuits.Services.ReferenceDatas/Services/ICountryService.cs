using CruxSuits.Shared.DTOs.ReferenceDatas.Country;

namespace CruxSuits.Services.ReferenceDatas.Services
{
    public interface ICountryService
    {
        Task<List<CountryResponse>> GetAllAsync(string languageId, CancellationToken cancellationToken = default);
        Task<List<CountryPhoneCodeResponse>> GetPhoneCodeOptionsAsync(string languageId, CancellationToken cancellationToken = default);
        Task<CountryDetailsResponse> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<CountryResponse> CreateAsync(CountryRequest request, CancellationToken cancellationToken = default);
        Task<CountryResponse> UpdateAsync(string id, UpdateCountryRequest request, CancellationToken cancellationToken = default);
        Task RemoveAsync(string id, CancellationToken cancellationToken = default);
    }
}
