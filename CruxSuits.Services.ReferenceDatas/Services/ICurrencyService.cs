using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;

namespace CruxSuits.Services.ReferenceDatas.Services;

public interface ICurrencyService
{
    Task<List<CurrencyResponse>> GetAllAsync(string languageId, CancellationToken cancellationToken = default);
    Task<CurrencyDetailsResponse> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<CurrencyResponse> CreateAsync(CurrencyRequest request, CancellationToken cancellationToken = default);
    Task<CurrencyDetailsResponse> UpdateAsync(string id, UpdateCurrencyRequest request, CancellationToken cancellationToken = default);
    Task RemoveAsync(string id, CancellationToken cancellationToken = default);
}
