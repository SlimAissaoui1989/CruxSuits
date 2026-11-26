using CruxSuits.Shared.DTOs.ReferenceDatas.Language;

namespace CruxSuits.Services.ReferenceDatas.Services;

public interface ILanguageService
{
    Task<List<LanguageResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LanguageDetailsResponse> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<LanguageResponse> CreateAsync(LanguageRequest request, CancellationToken cancellationToken = default);
    Task<LanguageResponse> UpdateAsync(string id, UpdateLanguageRequest request, CancellationToken cancellationToken = default);
    Task RemoveAsync(string id, CancellationToken cancellationToken = default);
}
