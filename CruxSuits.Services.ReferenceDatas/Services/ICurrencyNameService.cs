using CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;

namespace CruxSuits.Services.ReferenceDatas.Services;

public interface ICurrencyNameService
{
    Task<List<CurrencyNameResponse>> GetAllAsync(string currencyId, CancellationToken cancellationToken = default);
    Task<CurrencyNameResponse> GetByLanguageAsync(string currencyId, string languageId, CancellationToken cancellationToken = default);
    Task<CurrencyNameResponse> CreateAsync(string currencyId, CurrencyNameRequest request, CancellationToken cancellationToken = default);
    Task<CurrencyNameResponse> UpdateAsync(string currencyId, string languageId, CurrencyNameUpdateRequest request, CancellationToken cancellationToken = default);
    Task RemoveAsync(string currencyId, string languageId, CancellationToken cancellationToken = default);
}
