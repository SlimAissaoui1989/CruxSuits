using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.Constants;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using CruxSuits.Shared.DTOs.ReferenceDatas.Country;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class CountriesControllerTests
{
    #region GetAll

    // Success case: verifies GetAll uses the Accept-Language header to return localized countries;
    // A pass confirms the header value is passed to the service;
    // Failure signals the controller ignored or misread the header.
    [Fact]
    public async Task GetAll_UsesAcceptLanguageHeaderForLocalizedCountries()
    {
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var countries = new List<CountryResponse>
        {
            new() { Id = "US", Name = "United States", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "USD" }
        };
        var countryService = new Mock<ICountryService>();
        countryService
            .Setup(service => service.GetAllAsync(languageId, cancellation.Token))
            .ReturnsAsync(countries);
        var controller = CreateController(countryService.Object, languageId);

        var result = await controller.GetAll(cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(countries, okResult.Value);
        countryService.Verify(service => service.GetAllAsync(languageId, cancellation.Token), Times.Once);
    }

    // Success case: verifies GetAll resolves a language added to the catalog at runtime;
    // A pass confirms the controller uses the current catalog rather than a compiled-in list;
    // Failure signals the database language list is not used for negotiation.
    [Fact]
    public async Task GetAll_UsesLanguageAddedToCatalog()
    {
        const string languageId = "de";
        using var cancellation = new CancellationTokenSource();
        var countryService = new Mock<ICountryService>();
        var languageRequestResolver = new Mock<ILanguageRequestResolver>();
        languageRequestResolver
            .Setup(resolver => resolver.ResolveAsync(It.IsAny<HttpRequest>(), cancellation.Token))
            .ReturnsAsync(languageId);
        countryService
            .Setup(service => service.GetAllAsync(languageId, cancellation.Token))
            .ReturnsAsync([]);
        var controller = CreateController(countryService.Object, languageId, languageRequestResolver.Object);

        var result = await controller.GetAll(cancellation.Token);

        Assert.IsType<OkObjectResult>(result);
        countryService.Verify(service => service.GetAllAsync(languageId, cancellation.Token), Times.Once);
        languageRequestResolver.Verify(
            resolver => resolver.ResolveAsync(controller.Request, cancellation.Token),
            Times.Once);
    }

    // Failure case: verifies GetAll returns 406 when the shared resolver finds no acceptable language;
    // A pass confirms the controller preserves negotiation failure behavior;
    // Failure signals it queries localized data after language resolution failed.
    [Fact]
    public async Task GetAll_WhenResolverRejectsAllLanguages_PropagatesNotAcceptableException()
    {
        using var cancellation = new CancellationTokenSource();
        var countryService = new Mock<ICountryService>();
        var languageRequestResolver = new Mock<ILanguageRequestResolver>();
        languageRequestResolver
            .Setup(resolver => resolver.ResolveAsync(It.IsAny<HttpRequest>(), cancellation.Token))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotAcceptable));
        var controller = CreateController(
            countryService.Object,
            acceptLanguage: "en;q=0",
            languageRequestResolver: languageRequestResolver.Object);

        var exception = await Assert.ThrowsAsync<CxException>(() => controller.GetAll(cancellation.Token));

        Assert.Equal(CxHttpStatusCode.NotAcceptable, exception.StatusCode);
        countryService.Verify(service => service.GetAllAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies GetAll declares not acceptable when every supported language is rejected;
    // A pass confirms API metadata documents language negotiation failure;
    // Failure means clients cannot discover the 406 response contract.
    [Fact]
    public void GetAll_DeclaresNotAcceptableResponse()
    {
        var method = typeof(CountriesController).GetMethod(nameof(CountriesController.GetAll));

        Assert.NotNull(method);
        Assert.Contains(method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
            .Cast<ProducesResponseTypeAttribute>(),
            attribute => attribute.StatusCode == StatusCodes.Status406NotAcceptable);
    }

    #endregion

    #region GetPhoneCodeOptions

    // Success case: verifies GetPhoneCodeOptions uses the Accept-Language header to return localized options;
    // A pass confirms the header value is passed to the service;
    // Failure signals the controller ignored or misread the header.
    [Fact]
    public async Task GetPhoneCodeOptions_UsesAcceptLanguageHeaderForLocalizedOptions()
    {
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var options = new List<CountryPhoneCodeResponse>
        {
            new() { Id = "US", Name = "United States", PhoneCode = "+1" }
        };
        var countryService = new Mock<ICountryService>();
        countryService
            .Setup(service => service.GetPhoneCodeOptionsAsync(languageId, cancellation.Token))
            .ReturnsAsync(options);
        var controller = CreateController(countryService.Object, languageId);

        var result = await controller.GetPhoneCodeOptions(cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(options, okResult.Value);
        countryService.Verify(service => service.GetPhoneCodeOptionsAsync(languageId, cancellation.Token), Times.Once);
    }

    // Contract check: verifies GetPhoneCodeOptions declares not acceptable when every supported language is rejected;
    // A pass confirms API metadata documents language negotiation failure;
    // Failure means clients cannot discover the 406 response contract.
    [Fact]
    public void GetPhoneCodeOptions_DeclaresNotAcceptableResponse()
    {
        var method = typeof(CountriesController).GetMethod(nameof(CountriesController.GetPhoneCodeOptions));

        Assert.NotNull(method);
        Assert.Contains(method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
            .Cast<ProducesResponseTypeAttribute>(),
            attribute => attribute.StatusCode == StatusCodes.Status406NotAcceptable);
    }

    #endregion

    #region GetById

    // Success case: verifies GetById returns ok with country details;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetById_ReturnsOkWithCountryDetails()
    {
        const string countryId = "US";
        using var cancellation = new CancellationTokenSource();
        var country = new CountryDetailsResponse
        {
            Id = countryId,
            Alpha3Code = "USA",
            NumericCode = "840",
            PhoneCode = "+1",
            CurrencyId = "USD"
        };
        var countryService = new Mock<ICountryService>();
        countryService
            .Setup(service => service.GetByIdAsync(countryId, cancellation.Token))
            .ReturnsAsync(country);
        var controller = CreateController(countryService.Object);

        var result = await controller.GetById(countryId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(country, okResult.Value);
        countryService.Verify(service => service.GetByIdAsync(countryId, cancellation.Token), Times.Once);
    }

    // Contract check: verifies GetById declares not found response;
    // A pass confirms the declared contract;
    // Failure means the expected contract is missing.
    [Fact]
    public void GetById_DeclaresNotFoundResponse()
    {
        var method = typeof(CountriesController).GetMethod(nameof(CountriesController.GetById));

        Assert.NotNull(method);
        Assert.Contains(method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
            .Cast<ProducesResponseTypeAttribute>(),
            attribute => attribute.StatusCode == StatusCodes.Status404NotFound);
    }

    #endregion

    #region Create

    // Success case: verifies Create returns created at country details;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Create_ReturnsCreatedAtCountryDetails()
    {
        using var cancellation = new CancellationTokenSource();
        var request = new CountryRequest
        {
            Id = "US",
            Alpha3Code = "USA",
            NumericCode = "840",
            PhoneCode = "+1",
            CurrencyId = "USD"
        };
        var response = new CountryResponse
        {
            Id = request.Id,
            Alpha3Code = request.Alpha3Code,
            NumericCode = request.NumericCode,
            PhoneCode = request.PhoneCode,
            CurrencyId = request.CurrencyId
        };
        var countryService = new Mock<ICountryService>();
        countryService
            .Setup(service => service.CreateAsync(request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(countryService.Object);

        var result = await controller.Create(request, cancellation.Token);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(CountriesController.GetById), createdResult.ActionName);
        Assert.Equal(request.Id, createdResult.RouteValues!["id"]);
        Assert.Same(response, createdResult.Value);
        countryService.Verify(service => service.CreateAsync(request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Create propagates bad request exception when request validation fails;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenRequestValidationFails_PropagatesBadRequestException()
    {
        var request = new CountryRequest
        {
            Id = "U",
            Alpha3Code = "USA",
            NumericCode = "840",
            CurrencyId = "USD"
        };
        var countryService = new Mock<ICountryService>();
        countryService
            .Setup(service => service.CreateAsync(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.BadRequest, "Country ID must contain exactly two ASCII letters."));
        var controller = CreateController(countryService.Object);

        await Assert.ThrowsAsync<CxException>(() => controller.Create(request, CancellationToken.None));

        countryService.Verify(service => service.CreateAsync(request, CancellationToken.None), Times.Once);
    }

    // Contract check: verifies Create declares created response;
    // A pass confirms the declared contract;
    // Failure means the expected contract is missing.
    [Fact]
    public void Create_DeclaresCreatedResponse()
    {
        var method = typeof(CountriesController).GetMethod(nameof(CountriesController.Create));

        Assert.NotNull(method);
        Assert.Contains(method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
            .Cast<ProducesResponseTypeAttribute>(),
            attribute => attribute.StatusCode == StatusCodes.Status201Created
                && attribute.Type == typeof(CountryResponse));
    }

    #endregion

    #region Update

    // Success case: verifies Update returns ok with updated country;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Update_ReturnsOkWithUpdatedCountry()
    {
        const string countryId = "US";
        using var cancellation = new CancellationTokenSource();
        var request = new UpdateCountryRequest { PhoneCode = "+1" };
        var response = new CountryResponse
        {
            Id = countryId,
            Alpha3Code = "USA",
            NumericCode = "840",
            PhoneCode = request.PhoneCode,
            CurrencyId = "USD"
        };
        var countryService = new Mock<ICountryService>();
        countryService
            .Setup(service => service.UpdateAsync(countryId, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(countryService.Object);

        var result = await controller.Update(countryId, request, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(response, okResult.Value);
        countryService.Verify(service => service.UpdateAsync(countryId, request, cancellation.Token), Times.Once);
    }

    // Contract check: verifies Update declares ok and not found responses;
    // A pass confirms the declared contract;
    // Failure means the expected contract is missing.
    [Fact]
    public void Update_DeclaresOkAndNotFoundResponses()
    {
        var method = typeof(CountriesController).GetMethod(nameof(CountriesController.Update));

        Assert.NotNull(method);
        var responseTypes = method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
            .Cast<ProducesResponseTypeAttribute>()
            .ToArray();
        Assert.Contains(responseTypes, attribute => attribute.StatusCode == StatusCodes.Status200OK
            && attribute.Type == typeof(CountryResponse));
        Assert.Contains(responseTypes, attribute => attribute.StatusCode == StatusCodes.Status404NotFound);
    }

    #endregion

    #region Remove

    // Success case: verifies Remove returns no content after removing country;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Remove_ReturnsNoContentAfterRemovingCountry()
    {
        const string countryId = "US";
        using var cancellation = new CancellationTokenSource();
        var countryService = new Mock<ICountryService>();
        countryService
            .Setup(service => service.RemoveAsync(countryId, cancellation.Token))
            .Returns(Task.CompletedTask);
        var controller = CreateController(countryService.Object);

        var result = await controller.Remove(countryId, cancellation.Token);

        Assert.IsType<NoContentResult>(result);
        countryService.Verify(service => service.RemoveAsync(countryId, cancellation.Token), Times.Once);
    }

    // Contract check: verifies Remove declares no content and not found responses;
    // A pass confirms the declared contract;
    // Failure means the expected contract is missing.
    [Fact]
    public void Remove_DeclaresNoContentAndNotFoundResponses()
    {
        var method = typeof(CountriesController).GetMethod(nameof(CountriesController.Remove));

        Assert.NotNull(method);
        var responseTypes = method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
            .Cast<ProducesResponseTypeAttribute>()
            .ToArray();
        Assert.Contains(responseTypes, attribute => attribute.StatusCode == StatusCodes.Status204NoContent);
        Assert.Contains(responseTypes, attribute => attribute.StatusCode == StatusCodes.Status404NotFound);
    }

    #endregion

    private static CountriesController CreateController(
        ICountryService countryService,
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

        var controller = new CountriesController(
            configuration,
            NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
            Mock.Of<IReferenceDataUnitOfWork>(),
            Mock.Of<IMapper>(),
            countryService,
            languageRequestResolver);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        controller.Request.Headers[HttpHeaderNames.AcceptLanguage] = acceptLanguage;
        return controller;
    }
}
