using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Localization;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;
using AutoMapper;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Localization;

public sealed class LanguageCatalogProviderTests
{
    private readonly Mock<ILanguageRepository> _languageRepository = new();
    private readonly Mock<ILanguageCatalogCache> _languageCatalogCache = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly LanguageCatalogProvider _provider;

    public LanguageCatalogProviderTests() =>
        _provider = new LanguageCatalogProvider(
            _mapper.Object,
            _languageRepository.Object,
            _languageCatalogCache.Object,
            NullLogger<LanguageCatalogProvider>.Instance,
            CreateConfiguration("en"));

    #region GetLanguagesAsync

    // Cache-miss case: verifies GetLanguagesAsync loads the language list from the repository and publishes it;
    // A pass confirms a cold shared cache is hydrated from the source of truth;
    // Failure means the catalog remains empty after a normal cache miss.
    [Fact]
    public async Task GetLanguagesAsync_WhenCacheMisses_LoadsAndCachesRepositorySnapshot()
    {
        using var cancellation = new CancellationTokenSource();

        Language languageEn = new() { Id = "en", EnglishName = "English", NativeName = "English" };
        Language languageDe = new() { Id = "de", EnglishName = "German", NativeName = "Deutsch" };

        LanguageResponse languageResponceEn = new() { Id = "en", EnglishName = "English", NativeName = "English" };
        LanguageResponse languageResponceDe = new() { Id = "de", EnglishName = "German", NativeName = "Deutsch" };


        var languages = new List<Language> { languageEn, languageDe };

        IReadOnlyList<LanguageResponse>? publishedSnapshot = null;
        _languageCatalogCache
            .Setup(cache => cache.GetAsync(cancellation.Token))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>?)null);
        _languageRepository
            .Setup(repository => repository.GetAllAsync(null, cancellation.Token))
            .ReturnsAsync(languages.ToList());
        _languageCatalogCache
            .Setup(cache => cache.SetAsync(It.IsAny<IReadOnlyList<LanguageResponse>>(), cancellation.Token))
            .Callback<IReadOnlyList<LanguageResponse>, CancellationToken>((snapshot, _) => publishedSnapshot = snapshot)
            .Returns(Task.CompletedTask);

        _mapper.Setup(mapper => mapper.Map<LanguageResponse>(languageEn)).Returns(languageResponceEn);
        _mapper.Setup(mapper => mapper.Map<LanguageResponse>(languageDe)).Returns(languageResponceDe);

        var result = await _provider.GetLanguagesAsync(cancellation.Token);

        Assert.Equal(new[] { "en", "de" }, result.Select(language => language.Id));
        Assert.Equal(new[] { "en", "de" }, publishedSnapshot!.Select(language => language.Id));
        _languageRepository.Verify(repository => repository.GetAllAsync(null, cancellation.Token), Times.Once);
        _languageCatalogCache.Verify(cache => cache.SetAsync(
            It.IsAny<IReadOnlyList<LanguageResponse>>(), cancellation.Token), Times.Once);
    }

    // Cache-hit case: verifies GetLanguagesAsync returns the cached snapshot without querying the repository;
    // A pass confirms warm requests avoid a database read;
    // Failure signals the cache is bypassed.
    [Fact]
    public async Task GetLanguagesAsync_WhenCacheContainsSnapshot_SkipsRepositoryRead()
    {
        using var cancellation = new CancellationTokenSource();
        IReadOnlyList<LanguageResponse> languages =
        [
            new() { Id = "en", EnglishName = "English", NativeName = "English" },
            new() { Id = "de", EnglishName = "German", NativeName = "Deutsch" }
        ];
        _languageCatalogCache
            .Setup(cache => cache.GetAsync(cancellation.Token))
            .ReturnsAsync(languages);

        var result = await _provider.GetLanguagesAsync(cancellation.Token);

        Assert.Same(languages, result);
        _languageRepository.Verify(repository => repository.GetAllAsync<LanguageResponse>(
            It.IsAny<System.Linq.Expressions.Expression<Func<Language, bool>>?>(),
            It.IsAny<CancellationToken>()), Times.Never);
        _languageCatalogCache.Verify(cache => cache.SetAsync(
            It.IsAny<IReadOnlyList<LanguageResponse>>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies GetLanguagesAsync safely returns an empty snapshot if the repository is unavailable;
    // A pass confirms a cold cache and database outage do not fail request handling;
    // Failure means an infrastructure exception escapes or an invented catalog is returned.
    [Fact]
    public async Task GetLanguagesAsync_WhenCacheMissesAndRepositoryFails_ReturnsEmptySnapshot()
    {
        using var cancellation = new CancellationTokenSource();
        _languageCatalogCache
            .Setup(cache => cache.GetAsync(cancellation.Token))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>?)null);
        _languageRepository
            .Setup(repository => repository.GetAllAsync<LanguageResponse>(null, cancellation.Token))
            .ThrowsAsync(new InvalidOperationException("Reference database unavailable"));

        var result = await _provider.GetLanguagesAsync(cancellation.Token);

        Assert.Empty(result);
        _languageCatalogCache.Verify(cache => cache.SetAsync(
            It.IsAny<IReadOnlyList<LanguageResponse>>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    // Ordering case: verifies the configured default language is first and remaining languages are sorted;
    // A pass gives wildcard negotiation a deterministic fallback order;
    // Failure signals result order depends on database query order.
    [Fact]
    public async Task GetLanguagesAsync_WhenLoadingRepositorySnapshot_PutsDefaultFirstThenSortsRemainingIds()
    {
        using var cancellation = new CancellationTokenSource();

        Language languageFr = new() { Id = "fr", EnglishName = "French", NativeName = "Français" };
        Language languageEn = new() { Id = "en", EnglishName = "English", NativeName = "English" };
        Language languageAr = new() { Id = "ar", EnglishName = "Arabic", NativeName = "العربية" };
        Language languageDe = new() { Id = "de", EnglishName = "German", NativeName = "Deutsch" };

        LanguageResponse languageResponseFr = new() { Id = "fr", EnglishName = "French", NativeName = "Français" };
        LanguageResponse languageResponseEn = new() { Id = "en", EnglishName = "English", NativeName = "English" };
        LanguageResponse languageResponseAr = new() { Id = "ar", EnglishName = "Arabic", NativeName = "العربية" };
        LanguageResponse languageResponseDe = new() { Id = "de", EnglishName = "German", NativeName = "Deutsch" };

        var languages = new List<Language> { languageFr, languageEn, languageAr , languageDe};

        _languageCatalogCache
            .Setup(cache => cache.GetAsync(cancellation.Token))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>?)null);
        _languageRepository
            .Setup(repository => repository.GetAllAsync(null, cancellation.Token))
            .ReturnsAsync(languages);
        _languageCatalogCache
            .Setup(cache => cache.SetAsync(It.IsAny<IReadOnlyList<LanguageResponse>>(), cancellation.Token))
            .Returns(Task.CompletedTask);

        _mapper.Setup(mapper => mapper.Map<LanguageResponse>(languageFr)).Returns(languageResponseFr);
        _mapper.Setup(mapper => mapper.Map<LanguageResponse>(languageEn)).Returns(languageResponseEn);
        _mapper.Setup(mapper => mapper.Map<LanguageResponse>(languageAr)).Returns(languageResponseAr);
        _mapper.Setup(mapper => mapper.Map<LanguageResponse>(languageDe)).Returns(languageResponseDe);

        var provider = new LanguageCatalogProvider(
            _mapper.Object,
            _languageRepository.Object,
            _languageCatalogCache.Object,
            NullLogger<LanguageCatalogProvider>.Instance,
            CreateConfiguration("de"));

        var result = await provider.GetLanguagesAsync(cancellation.Token);

        Assert.Equal(new[] { "de", "ar", "en", "fr" }, result.Select(language => language.Id));
    }

    #endregion

    private static IConfiguration CreateConfiguration(string defaultLanguageId) =>
        new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Localization:DefaultLanguageId"] = defaultLanguageId
            })
            .Build();
}
