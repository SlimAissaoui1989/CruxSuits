using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using CruxSuits.Shared.DTOs.ReferenceDatas.Country;
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

    [Fact]
    public async Task GetAll_ReturnsOkWithCountriesForLanguage()
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
        var controller = CreateController(countryService.Object);

        var result = await controller.GetAll(languageId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(countries, okResult.Value);
        countryService.Verify(service => service.GetAllAsync(languageId, cancellation.Token), Times.Once);
    }

    #endregion

    #region GetPhoneCodeOptions

    [Fact]
    public async Task GetPhoneCodeOptions_ReturnsOkWithOptionsForLanguage()
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
        var controller = CreateController(countryService.Object);

        var result = await controller.GetPhoneCodeOptions(languageId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(options, okResult.Value);
        countryService.Verify(service => service.GetPhoneCodeOptionsAsync(languageId, cancellation.Token), Times.Once);
    }

    #endregion

    #region GetById

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

    private static CountriesController CreateController(ICountryService countryService) => new(
        new ConfigurationBuilder().Build(),
        NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
        Mock.Of<IReferenceDataUnitOfWork>(),
        Mock.Of<IMapper>(),
        countryService);
}
