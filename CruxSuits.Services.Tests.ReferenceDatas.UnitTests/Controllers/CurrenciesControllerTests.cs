using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxIT.Library;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.Constants;
using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class CurrenciesControllerTests
{
    #region GetAll

    // Success case: verifies GetAll uses the Accept-Language header to return localized currencies;
    // A pass confirms the header value is passed to the service;
    // Failure signals the controller ignored or misread the header.
    [Fact]
    public async Task GetAll_UsesAcceptLanguageHeaderForLocalizedCurrencies()
    {
        const string languageId = "fr";
        using var cancellation = new CancellationTokenSource();
        var currencies = new List<CurrencyResponse>
        {
            new() { Id = "EUR", Name = "euro", NumericCode = "978" }
        };
        var currencyService = new Mock<ICurrencyService>();
        currencyService
            .Setup(service => service.GetAllAsync(languageId, cancellation.Token))
            .ReturnsAsync(currencies);
        var controller = CreateController(currencyService.Object, languageId);

        var result = await controller.GetAll(cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(currencies, okResult.Value);
        currencyService.Verify(service => service.GetAllAsync(languageId, cancellation.Token), Times.Once);
    }

    // Success case: verifies GetAll resolves a language added to the catalog at runtime;
    // A pass confirms the controller uses the current catalog rather than a compiled-in list;
    // Failure signals the database language list is not used for negotiation.
    [Fact]
    public async Task GetAll_UsesLanguageAddedToCatalog()
    {
        const string languageId = "de";
        using var cancellation = new CancellationTokenSource();
        var currencyService = new Mock<ICurrencyService>();
        var languageRequestResolver = new Mock<ILanguageRequestResolver>();
        languageRequestResolver
            .Setup(resolver => resolver.ResolveAsync(It.IsAny<HttpRequest>(), cancellation.Token))
            .ReturnsAsync(languageId);
        currencyService
            .Setup(service => service.GetAllAsync(languageId, cancellation.Token))
            .ReturnsAsync([]);
        var controller = CreateController(currencyService.Object, languageId, languageRequestResolver.Object);

        var result = await controller.GetAll(cancellation.Token);

        Assert.IsType<OkObjectResult>(result);
        currencyService.Verify(service => service.GetAllAsync(languageId, cancellation.Token), Times.Once);
        languageRequestResolver.Verify(
            resolver => resolver.ResolveAsync(controller.Request, cancellation.Token),
            Times.Once);
    }

    // Failure case: verifies GetAll returns not acceptable when all supported languages are excluded;
    // A pass confirms the controller does not serve an explicitly rejected language;
    // Failure means the response violates the client's language preferences.
    [Fact]
    public async Task GetAll_WhenResolverRejectsAllLanguages_PropagatesNotAcceptableException()
    {
        using var cancellation = new CancellationTokenSource();
        var currencyService = new Mock<ICurrencyService>();
        var languageRequestResolver = new Mock<ILanguageRequestResolver>();
        languageRequestResolver
            .Setup(resolver => resolver.ResolveAsync(It.IsAny<HttpRequest>(), cancellation.Token))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotAcceptable));
        var controller = CreateController(currencyService.Object, "en;q=0, fr;q=0, ar;q=0", languageRequestResolver.Object);

        var exception = await Assert.ThrowsAsync<CxException>(() => controller.GetAll(cancellation.Token));

        Assert.Equal(CxHttpStatusCode.NotAcceptable, exception.StatusCode);
        currencyService.Verify(service => service.GetAllAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies GetAll declares not acceptable for excluded language preferences;
    // A pass confirms API metadata describes the negotiation failure;
    // Failure means generated API documentation omits the 406 response.
    [Fact]
    public void GetAll_DeclaresNotAcceptableResponse()
    {
        var method = typeof(CurrenciesController).GetMethod(nameof(CurrenciesController.GetAll));

        Assert.NotNull(method);
        Assert.Contains(method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
            .Cast<ProducesResponseTypeAttribute>(),
            attribute => attribute.StatusCode == StatusCodes.Status406NotAcceptable);
    }

    #endregion

    #region GetById

    // Success case: verifies GetById returns ok with currency details;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetById_ReturnsOkWithCurrencyDetails()
    {
        const string currencyId = "USD";
        using var cancellation = new CancellationTokenSource();
        var currency = new CurrencyDetailsResponse { Id = currencyId, NumericCode = "840" };
        var currencyService = new Mock<ICurrencyService>();
        currencyService
            .Setup(service => service.GetByIdAsync(currencyId, cancellation.Token))
            .ReturnsAsync(currency);
        var controller = CreateController(currencyService.Object);

        var result = await controller.GetById(currencyId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(currency, okResult.Value);
        currencyService.Verify(service => service.GetByIdAsync(currencyId, cancellation.Token), Times.Once);
    }

    // Contract check: verifies GetById declares not found response;
    // A pass confirms the declared contract;
    // Failure means the expected contract is missing.
    [Fact]
    public void GetById_DeclaresNotFoundResponse()
    {
        var method = typeof(CurrenciesController).GetMethod(nameof(CurrenciesController.GetById));

        Assert.NotNull(method);
        Assert.Contains(method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
            .Cast<ProducesResponseTypeAttribute>(),
            attribute => attribute.StatusCode == StatusCodes.Status404NotFound);
    }

    #endregion

    #region TestHelpers

    private static CurrenciesController CreateController(
        ICurrencyService currencyService,
        string acceptLanguage = "en",
        ILanguageRequestResolver? languageRequestResolver = null)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Localization:DefaultLanguageId"] = "en"
            })
            .Build();

        if (languageRequestResolver is null)
        {
            var defaultCatalog = (IReadOnlyList<LanguageResponse>)[
                new() { Id = "en", EnglishName = "English", NativeName = "English" },
                new() { Id = "fr", EnglishName = "French", NativeName = "Français" },
                new() { Id = "ar", EnglishName = "Arabic", NativeName = "العربية" }
            ];
            var languageCatalogCache = new Mock<ILanguageCatalogCache>();
            languageCatalogCache.Setup(cache => cache.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(defaultCatalog);
            languageRequestResolver = new LanguageRequestResolver(languageCatalogCache.Object, configuration);
        }

        var controller = new CurrenciesController(
            configuration,
            NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
            Mock.Of<IReferenceDataUnitOfWork>(),
            Mock.Of<IMapper>(),
            currencyService,
            languageRequestResolver);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        controller.Request.Headers[HttpHeaderNames.AcceptLanguage] = acceptLanguage;
        return controller;
    }

    #endregion
}
