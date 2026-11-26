using CruxSuits.Shared.DTOs.ReferenceDatas.Language;

namespace CruxSuits.Services.Shared.Localization;

public interface ILanguageCatalogCache
{
    Task<IReadOnlyList<LanguageResponse>?> GetAsync(CancellationToken cancellationToken = default);

    Task SetAsync(
        IReadOnlyList<LanguageResponse> languages,
        CancellationToken cancellationToken = default);
}
