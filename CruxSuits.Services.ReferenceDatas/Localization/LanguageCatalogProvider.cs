using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace CruxSuits.Services.ReferenceDatas.Localization;

public sealed class LanguageCatalogProvider : ILanguageCatalogProvider
{
    private readonly ILanguageRepository _languageRepository;
    private readonly ILanguageCatalogCache _languageCatalogCache;
    private readonly ILogger<LanguageCatalogProvider> _logger;
    private readonly IMapper _mapper;
    private readonly string _defaultLanguageId;

    public LanguageCatalogProvider(
        IMapper mapper,
        ILanguageRepository languageRepository,
        ILanguageCatalogCache languageCatalogCache,
        ILogger<LanguageCatalogProvider> logger,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _languageRepository = languageRepository;
        _languageCatalogCache = languageCatalogCache;
        _logger = logger;
        _defaultLanguageId = configuration["Localization:DefaultLanguageId"] ?? string.Empty;
    }

    public async Task<IReadOnlyList<LanguageResponse>> GetLanguagesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var cachedLanguages = await _languageCatalogCache.GetAsync(cancellationToken);
            if (cachedLanguages is not null)
                return cachedLanguages;

            var languages = await LoadLanguagesAsync(cancellationToken);
            await _languageCatalogCache.SetAsync(languages, cancellationToken);
            return languages;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Could not load the language catalog from the reference data store.");
            return [];
        }
    }

    public async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var languages = await LoadLanguagesAsync(cancellationToken);
            await _languageCatalogCache.SetAsync(languages, cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Could not refresh the language catalog after a language change.");
        }
    }

    private async Task<List<LanguageResponse>> LoadLanguagesAsync(CancellationToken cancellationToken)
    {
        var languages = await _languageRepository.GetAllAsync(cancellationToken: cancellationToken);
        return languages
            .Select(language => _mapper.Map<LanguageResponse>(language))
            .OrderBy(language => string.Equals(language.Id, _defaultLanguageId, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
            .ThenBy(language => language.Id, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
