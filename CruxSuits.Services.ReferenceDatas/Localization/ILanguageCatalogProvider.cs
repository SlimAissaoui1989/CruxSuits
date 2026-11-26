using CruxSuits.Shared.DTOs.ReferenceDatas.Language;

namespace CruxSuits.Services.ReferenceDatas.Localization;

public interface ILanguageCatalogProvider
{
    Task<IReadOnlyList<LanguageResponse>> GetLanguagesAsync(CancellationToken cancellationToken = default);

    Task RefreshAsync(CancellationToken cancellationToken = default);
}
