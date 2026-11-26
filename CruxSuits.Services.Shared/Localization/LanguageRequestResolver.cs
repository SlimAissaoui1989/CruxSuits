using CruxIT.Library;
using CruxIT.Library.Exceptions;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CruxSuits.Services.Shared.Localization;

public sealed class LanguageRequestResolver : ILanguageRequestResolver
{
    private readonly ILanguageCatalogCache _languageCatalogCache;
    private readonly string _defaultLanguageId;
    private readonly ILogger<LanguageRequestResolver>? _logger;

    public LanguageRequestResolver(
        ILanguageCatalogCache languageCatalogCache,
        IConfiguration configuration,
        ILogger<LanguageRequestResolver>? logger = null)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        _languageCatalogCache = languageCatalogCache;
        _logger = logger;
        var defaultLanguageId = configuration["Localization:DefaultLanguageId"];
        if (string.IsNullOrWhiteSpace(defaultLanguageId))
        {
            throw new InvalidOperationException(
                "Configuration value 'Localization:DefaultLanguageId' is required to resolve request languages.");
        }

        _defaultLanguageId = defaultLanguageId.Trim();
    }

    public async Task<string> ResolveAsync(HttpRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var catalog = await _languageCatalogCache.GetAsync(cancellationToken);
        var catalogLanguageIds = catalog?
            .Select(language => language.Id)
            .Where(languageId => !string.IsNullOrWhiteSpace(languageId))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray()
            ?? [];

        var effectiveDefaultLanguageId = _defaultLanguageId;
        if (catalogLanguageIds.Length > 0
            && !catalogLanguageIds.Contains(_defaultLanguageId, StringComparer.OrdinalIgnoreCase))
        {
            effectiveDefaultLanguageId = catalogLanguageIds[0];
            _logger?.LogWarning(
                "Configured default language {ConfiguredDefaultLanguageId} is missing from the language catalog; using {FallbackLanguageId} instead.",
                _defaultLanguageId,
                effectiveDefaultLanguageId);
        }

        var supportedLanguageIds = catalogLanguageIds
            .Append(effectiveDefaultLanguageId)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var languageId = AcceptLanguageResolver.Resolve(request, supportedLanguageIds, effectiveDefaultLanguageId);
        if (languageId is null)
            throw new CxException(CxHttpStatusCode.NotAcceptable);

        return languageId;
    }
}
