using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.Constants;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using CruxIT.Library;
using CruxIT.Library.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Localization;

public sealed class LanguageRequestResolverTests
{
    #region ResolveAsync

    // Success case: verifies ResolveAsync selects a supported language from the shared cache;
    // A pass confirms consumers negotiate from the current catalog;
    // Failure signals the resolver ignores cached supported languages.
    [Fact]
    public async Task ResolveAsync_WhenHeaderLanguageExistsInCache_ReturnsThatLanguage()
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        languageCache
            .Setup(cache => cache.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>)[CreateLanguage("en"), CreateLanguage("de")]);
        var resolver = CreateResolver(languageCache.Object, defaultLanguageId: "en");
        var request = CreateRequest("de-DE, en;q=0.5");

        var result = await resolver.ResolveAsync(request);

        Assert.Equal("de", result);
        languageCache.Verify(cache => cache.GetAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Fallback case: verifies ResolveAsync uses the service's configured default when the cache is cold;
    // A pass confirms a cache miss does not require a ReferenceData dependency;
    // Failure signals the resolver throws or returns an empty language ID.
    [Fact]
    public async Task ResolveAsync_WhenCacheMissesAndHeaderIsMissing_UsesConfiguredDefault()
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        languageCache
            .Setup(cache => cache.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>?)null);
        var resolver = CreateResolver(languageCache.Object, defaultLanguageId: "de");

        var result = await resolver.ResolveAsync(CreateRequest(null));

        Assert.Equal("de", result);
    }

    // Failure case: verifies ResolveAsync throws the standard 406 exception when all cached choices are excluded;
    // A pass confirms HTTP status mapping is centralized in the shared resolver;
    // Failure signals the resolver returns a null language instead of raising the API exception.
    [Fact]
    public async Task ResolveAsync_WhenEveryCachedLanguageIsExcluded_ThrowsNotAcceptableException()
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        languageCache
            .Setup(cache => cache.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>)[CreateLanguage("en"), CreateLanguage("fr")]);
        var resolver = CreateResolver(languageCache.Object, defaultLanguageId: "en");

        var exception = await Assert.ThrowsAsync<CxException>(
            () => resolver.ResolveAsync(CreateRequest("en;q=0, fr;q=0")));

        Assert.Equal(CxHttpStatusCode.NotAcceptable, exception.StatusCode);
    }

    // Fallback case: verifies ResolveAsync uses the configured default when the cache is empty;
    // A pass confirms empty and missing shared snapshots behave safely for ordinary requests;
    // Failure signals the resolver requires a populated catalog to serve the default language.
    [Fact]
    public async Task ResolveAsync_WhenCacheIsEmptyAndHeaderIsUnsupported_UsesConfiguredDefault()
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        languageCache
            .Setup(cache => cache.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>)[]);
        var resolver = CreateResolver(languageCache.Object, defaultLanguageId: "fr");

        var result = await resolver.ResolveAsync(CreateRequest("de-DE"));

        Assert.Equal("fr", result);
    }

    // Fallback case: verifies ResolveAsync uses the first published language when the configured default is absent;
    // A pass confirms a non-empty catalog remains usable when service configuration is out of date;
    // Failure means the resolver selects a language that the catalog does not contain.
    [Fact]
    public async Task ResolveAsync_WhenCatalogOmitsConfiguredDefault_UsesFirstAvailableLanguage()
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        languageCache
            .Setup(cache => cache.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>)[CreateLanguage("fr"), CreateLanguage("ar")]);
        var resolver = CreateResolver(languageCache.Object, defaultLanguageId: "en");

        var result = await resolver.ResolveAsync(CreateRequest("de-DE"));

        Assert.Equal("fr", result);
    }

    // Configuration case: verifies LanguageRequestResolver rejects a blank or whitespace-only default ID;
    // A pass ensures requests cannot silently fall back to an unusable language code;
    // Failure means invalid localization configuration is accepted by all consumers.
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Constructor_WhenDefaultLanguageIdIsBlank_ThrowsInvalidOperationException(string defaultLanguageId)
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Localization:DefaultLanguageId"] = defaultLanguageId
            })
            .Build();

        Assert.Throws<InvalidOperationException>(() => new LanguageRequestResolver(languageCache.Object, configuration));
    }

    // Configuration case: verifies LanguageRequestResolver rejects a missing default language setting;
    // A pass ensures every consuming service has an explicit fallback language;
    // Failure means misconfigured services silently accept an unusable default.
    [Fact]
    public void Constructor_WhenDefaultLanguageIdIsMissing_ThrowsInvalidOperationException()
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        var configuration = new ConfigurationBuilder().Build();

        Assert.Throws<InvalidOperationException>(() => new LanguageRequestResolver(languageCache.Object, configuration));
    }

    // Configuration case: verifies ResolveAsync trims surrounding whitespace from the configured default;
    // A pass confirms configuration formatting does not produce an invalid language ID;
    // Failure signals default language settings are used verbatim when they contain padding.
    [Fact]
    public async Task ResolveAsync_WhenConfiguredDefaultHasSurroundingWhitespace_TrimsDefaultLanguageId()
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        languageCache
            .Setup(cache => cache.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>?)null);
        var resolver = CreateResolver(languageCache.Object, defaultLanguageId: " en ");

        var result = await resolver.ResolveAsync(CreateRequest(null));

        Assert.Equal("en", result);
    }

    // Failure case: verifies ResolveAsync throws 406 when a cold-cache default is explicitly excluded;
    // A pass confirms Redis outage does not bypass the client's language exclusion;
    // Failure signals the configured default is returned after the client rejected it.
    [Fact]
    public async Task ResolveAsync_WhenCacheMissesAndDefaultIsExcluded_ThrowsNotAcceptableException()
    {
        var languageCache = new Mock<ILanguageCatalogCache>();
        languageCache
            .Setup(cache => cache.GetAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>?)null);
        var resolver = CreateResolver(languageCache.Object, defaultLanguageId: "fr");

        var exception = await Assert.ThrowsAsync<CxException>(
            () => resolver.ResolveAsync(CreateRequest("fr;q=0")));

        Assert.Equal(CxHttpStatusCode.NotAcceptable, exception.StatusCode);
    }

    // Cancellation case: verifies ResolveAsync forwards cancellation to the shared catalog cache;
    // A pass confirms the cache operation respects the request's cancellation token;
    // Failure signals canceled requests can continue waiting on cache access.
    [Fact]
    public async Task ResolveAsync_ForwardsCancellationTokenToCache()
    {
        using var cancellation = new CancellationTokenSource();
        var languageCache = new Mock<ILanguageCatalogCache>();
        languageCache
            .Setup(cache => cache.GetAsync(cancellation.Token))
            .ReturnsAsync((IReadOnlyList<LanguageResponse>)[CreateLanguage("en")]);
        var resolver = CreateResolver(languageCache.Object, defaultLanguageId: "en");

        await resolver.ResolveAsync(CreateRequest("en"), cancellation.Token);

        languageCache.Verify(cache => cache.GetAsync(cancellation.Token), Times.Once);
    }

    #endregion

    #region TestHelpers

    private static LanguageRequestResolver CreateResolver(ILanguageCatalogCache cache, string defaultLanguageId)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Localization:DefaultLanguageId"] = defaultLanguageId
            })
            .Build();

        return new LanguageRequestResolver(cache, configuration);
    }

    private static LanguageResponse CreateLanguage(string id) => new()
    {
        Id = id,
        EnglishName = id,
        NativeName = id
    };

    private static HttpRequest CreateRequest(string? acceptLanguage)
    {
        var request = new DefaultHttpContext().Request;
        if (acceptLanguage is not null)
            request.Headers[HttpHeaderNames.AcceptLanguage] = acceptLanguage;
        return request;
    }

    #endregion
}
