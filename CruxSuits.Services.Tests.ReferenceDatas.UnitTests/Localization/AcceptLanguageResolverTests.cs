using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.Constants;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Localization;

public sealed class AcceptLanguageResolverTests
{
    private static readonly string[] SupportedLanguageIds = ["en", "ar", "fr"];

    #region Resolve

    // Success case: verifies Resolve returns the supported language in the header;
    // A pass confirms an exact language tag is recognized;
    // Failure signals a valid preference was ignored.
    [Fact]
    public void Resolve_WhenHeaderContainsSupportedLanguage_ReturnsLanguage()
    {
        var request = CreateRequest("fr");

        var languageId = Resolve(request);

        Assert.Equal("fr", languageId);
    }

    // Success case: verifies Resolve prefers the language with the highest quality value;
    // A pass confirms quality weights control preference order;
    // Failure signals the wrong language was selected.
    [Fact]
    public void Resolve_WhenHeaderContainsQualityWeights_ReturnsHighestQualitySupportedLanguage()
    {
        var request = CreateRequest("en;q=0.5, fr;q=0.9, ar;q=0.7");

        var languageId = Resolve(request);

        Assert.Equal("fr", languageId);
    }

    // Success case: verifies Resolve maps a supported regional language tag to its base language;
    // A pass confirms fr-CA resolves to the supported fr language;
    // Failure signals regional preferences do not match supported languages.
    [Fact]
    public void Resolve_WhenHeaderContainsSupportedRegionalTag_ReturnsBaseLanguage()
    {
        var request = CreateRequest("fr-CA, en;q=0.8");

        var languageId = Resolve(request);

        Assert.Equal("fr", languageId);
    }

    // Failure case: verifies Resolve uses English when no requested language is supported;
    // A pass confirms unsupported preferences fall back to the application default;
    // Failure means an unsupported language was passed to localization queries.
    [Fact]
    public void Resolve_WhenNoHeaderLanguageIsSupported_ReturnsEnglishDefault()
    {
        var request = CreateRequest("de-DE, es;q=0.8");

        var languageId = Resolve(request);

        Assert.Equal("en", languageId);
    }

    // Failure case: verifies Resolve uses English when the header is absent;
    // A pass confirms requests without language preferences use the application default;
    // Failure means the resolver returns an empty or unsupported language ID.
    [Fact]
    public void Resolve_WhenHeaderIsMissing_ReturnsEnglishDefault()
    {
        var request = CreateRequest(null);

        var languageId = Resolve(request);

        Assert.Equal("en", languageId);
    }

    // Failure case: verifies Resolve ignores a language with quality zero;
    // A pass confirms explicitly rejected preferences are skipped;
    // Failure means a q=0 language is selected.
    [Fact]
    public void Resolve_WhenLanguageHasZeroQuality_SkipsIt()
    {
        var request = CreateRequest("fr;q=0, ar;q=0.5");

        var languageId = Resolve(request);

        Assert.Equal("ar", languageId);
    }

    // Failure case: verifies Resolve honors an excluded default when a wildcard accepts other supported languages;
    // A pass confirms a different supported language is selected;
    // Failure means the explicitly rejected default was returned.
    [Fact]
    public void Resolve_WhenWildcardAcceptsLanguagesButExcludesDefault_UsesNextSupportedLanguage()
    {
        var request = CreateRequest("en;q=0, *;q=1");

        var languageId = Resolve(request);

        Assert.Equal("ar", languageId);
    }

    // Success case: verifies Resolve uses the first supported language for a wildcard-only preference;
    // A pass confirms wildcard negotiation returns a supported language;
    // Failure means wildcard-only headers are ignored or mishandled.
    [Fact]
    public void Resolve_WhenHeaderContainsOnlyWildcard_ReturnsFirstSupportedLanguage()
    {
        var request = CreateRequest("*;q=1");

        var languageId = Resolve(request);

        Assert.Equal("en", languageId);
    }

    // Failure case: verifies Resolve rejects a header that excludes every supported language;
    // A pass confirms explicitly rejected languages are never served;
    // Failure means the resolver returns a language the client marked unacceptable.
    [Fact]
    public void Resolve_WhenAllSupportedLanguagesAreExcluded_ReturnsNoLanguage()
    {
        var request = CreateRequest("en;q=0, fr;q=0, ar;q=0");

        var languageId = Resolve(request);

        Assert.Null(languageId);
    }

    // Fallback case: verifies Resolve picks the first acceptable supported language when all listed preferences are excluded;
    // A pass confirms partial exclusions still produce a supported response language;
    // Failure means the resolver serves an excluded language or fails unnecessarily.
    [Fact]
    public void Resolve_WhenOnlySomeSupportedLanguagesAreExcluded_UsesFirstRemainingLanguage()
    {
        var request = CreateRequest("en;q=0, ar;q=0");

        var languageId = Resolve(request);

        Assert.Equal("fr", languageId);
    }

    // Failure case: verifies Resolve rejects a zero-quality wildcard that excludes every supported language;
    // A pass confirms wildcard q=0 is treated as an explicit exclusion;
    // Failure means a language excluded by the wildcard is still served.
    [Fact]
    public void Resolve_WhenWildcardHasZeroQuality_ReturnsNoLanguage()
    {
        var request = CreateRequest("*;q=0");

        var languageId = Resolve(request);

        Assert.Null(languageId);
    }

    // Success case: verifies Resolve honors an explicit supported language alongside a zero-quality wildcard;
    // A pass confirms an explicit positive range overrides wildcard exclusion;
    // Failure means an accepted explicit language is rejected with the wildcard.
    [Fact]
    public void Resolve_WhenExplicitLanguageIsAcceptedAndWildcardIsExcluded_ReturnsExplicitLanguage()
    {
        var request = CreateRequest("fr;q=0.8, *;q=0");

        var languageId = Resolve(request);

        Assert.Equal("fr", languageId);
    }

    // Failure case: verifies Resolve ignores malformed header entries and uses the next valid language;
    // A pass confirms malformed preferences do not cause request failure;
    // Failure means malformed input is selected or prevents valid fallback.
    [Fact]
    public void Resolve_WhenHeaderContainsMalformedEntry_UsesNextValidLanguage()
    {
        var request = CreateRequest("fr;q=not-a-number, ar;q=0.8");

        var languageId = Resolve(request);

        Assert.Equal("ar", languageId);
    }

    // Success case: verifies Resolve accepts languages supplied by the catalog rather than a fixed list;
    // A pass confirms a newly added language can be negotiated;
    // Failure signals supported IDs are still hard-coded.
    [Fact]
    public void Resolve_WhenCatalogContainsAdditionalLanguage_ReturnsThatLanguage()
    {
        var request = CreateRequest("de");

        var languageId = Resolve(request, ["en", "fr", "de"]);

        Assert.Equal("de", languageId);
    }

    // Fallback case: verifies Resolve uses the configured default when no preference is present;
    // A pass confirms fallback does not depend on a compiled-in English value;
    // Failure signals the configured default is ignored.
    [Fact]
    public void Resolve_WhenHeaderIsMissing_UsesConfiguredDefaultLanguage()
    {
        var request = CreateRequest(null);

        var languageId = Resolve(request, ["en", "fr", "de"], "de");

        Assert.Equal("de", languageId);
    }

    #endregion

    #region TestHelpers

    private static string? Resolve(
        HttpRequest request,
        IReadOnlyCollection<string>? supportedLanguageIds = null,
        string defaultLanguageId = "en") =>
        AcceptLanguageResolver.Resolve(request, supportedLanguageIds ?? SupportedLanguageIds, defaultLanguageId);

    private static HttpRequest CreateRequest(string? acceptLanguage)
    {
        var request = new DefaultHttpContext().Request;
        if (acceptLanguage is not null)
            request.Headers[HttpHeaderNames.AcceptLanguage] = acceptLanguage;
        return request;
    }

    #endregion
}

