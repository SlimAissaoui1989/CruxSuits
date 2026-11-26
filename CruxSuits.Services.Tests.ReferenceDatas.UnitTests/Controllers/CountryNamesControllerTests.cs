using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class CountryNamesControllerTests
{
    #region GetAll

    // Success case: verifies GetAll returns ok with all names for country;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAll_ReturnsOkWithAllNamesForCountry()
    {
        const string countryId = "US";
        using var cancellation = new CancellationTokenSource();
        var names = new List<CountryNameResponse>
        {
            new() { LanguageId = "en", Name = "United States", Nationality = "American" },
            new() { LanguageId = "fr", Name = "États-Unis", Nationality = "Américaine" },
            new() { LanguageId = "ar", Name = "الولايات المتحدة", Nationality = "أمريكي" }
        };
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.GetAllAsync(countryId, cancellation.Token))
            .ReturnsAsync(names);
        var controller = CreateController(countryNameService.Object);

        var result = await controller.GetAll(countryId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(names, okResult.Value);
        countryNameService.Verify(service => service.GetAllAsync(countryId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies GetAll propagates not found exception when country does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetAll_WhenCountryDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "XX";
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.GetAllAsync(countryId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(countryNameService.Object);

        await Assert.ThrowsAsync<CxException>(() => controller.GetAll(countryId, CancellationToken.None));

        countryNameService.Verify(service => service.GetAllAsync(countryId, CancellationToken.None), Times.Once);
    }

    #endregion

    #region GetByLanguage

    // Success case: verifies GetByLanguage returns ok with the requested country name;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByLanguage_ReturnsOkWithTheRequestedCountryName()
    {
        const string countryId = "US";
        const string languageId = "fr";
        using var cancellation = new CancellationTokenSource();
        var expectedName = new CountryNameResponse
        {
            LanguageId = languageId,
            LanguageName = "French",
            Name = "États-Unis",
            Nationality = "Américaine"
        };
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.GetByLanguageAsync(countryId, languageId, cancellation.Token))
            .ReturnsAsync(expectedName);
        var controller = CreateController(countryNameService.Object);

        var result = await controller.GetByLanguage(countryId, languageId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(expectedName, okResult.Value);
        countryNameService.Verify(
            service => service.GetByLanguageAsync(countryId, languageId, cancellation.Token),
            Times.Once);
    }

    // Failure case: verifies GetByLanguage propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByLanguage_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "US";
        const string languageId = "fr";
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.GetByLanguageAsync(countryId, languageId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(countryNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.GetByLanguage(countryId, languageId, CancellationToken.None));

        countryNameService.Verify(
            service => service.GetByLanguageAsync(countryId, languageId, CancellationToken.None),
            Times.Once);
    }

    #endregion

    #region Create

    // Success case: verifies Create returns created at country name in language;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Create_ReturnsCreatedAtCountryNameInLanguage()
    {
        const string countryId = "US";
        using var cancellation = new CancellationTokenSource();
        var request = new CountryNameRequest
        {
            LanguageId = "fr",
            Name = "États-Unis",
            Nationality = "Américaine"
        };
        var response = new CountryNameResponse
        {
            LanguageId = request.LanguageId,
            LanguageName = "French (Français)",
            Name = request.Name,
            Nationality = request.Nationality
        };
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.CreateAsync(countryId, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(countryNameService.Object);

        var result = await controller.Create(countryId, request, cancellation.Token);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(CountryNamesController.GetByLanguage), createdResult.ActionName);
        Assert.Equal(countryId, createdResult.RouteValues!["countryId"]);
        Assert.Equal(request.LanguageId, createdResult.RouteValues["languageId"]);
        Assert.Same(response, createdResult.Value);
        countryNameService.Verify(service => service.CreateAsync(countryId, request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Create propagates conflict exception when translation already exists;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenTranslationAlreadyExists_PropagatesConflictException()
    {
        const string countryId = "US";
        var request = new CountryNameRequest
        {
            LanguageId = "en",
            Name = "United States",
            Nationality = "American"
        };
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.CreateAsync(countryId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.Conflict));
        var controller = CreateController(countryNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, request, CancellationToken.None));
        countryNameService.Verify(service => service.CreateAsync(countryId, request, CancellationToken.None), Times.Once);
    }

    // Failure case: verifies Create propagates not found exception when country does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenCountryDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "XX";
        var request = new CountryNameRequest
        {
            LanguageId = "fr",
            Name = "Pays inexistant",
            Nationality = "Inconnue"
        };
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.CreateAsync(countryId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(countryNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, request, CancellationToken.None));

        countryNameService.Verify(
            service => service.CreateAsync(countryId, request, CancellationToken.None),
            Times.Once);
    }

    // Failure case: verifies Create propagates not found exception when language does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenLanguageDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "US";
        var request = new CountryNameRequest
        {
            LanguageId = "zz",
            Name = "Unknown language",
            Nationality = "Unknown"
        };
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.CreateAsync(countryId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(countryNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, request, CancellationToken.None));

        countryNameService.Verify(
            service => service.CreateAsync(countryId, request, CancellationToken.None),
            Times.Once);
    }

    #endregion

    #region Update

    // Success case: verifies Update returns ok with updated country name;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Update_ReturnsOkWithUpdatedCountryName()
    {
        const string countryId = "US";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var request = new CountryNameUpdateRequest
        {
            Name = "United States of America",
            Nationality = "American"
        };
        var response = new CountryNameResponse
        {
            LanguageId = languageId,
            LanguageName = "English (English)",
            Name = request.Name,
            Nationality = request.Nationality
        };
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.UpdateAsync(countryId, languageId, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(countryNameService.Object);

        var result = await controller.Update(countryId, languageId, request, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(response, okResult.Value);
        countryNameService.Verify(service => service.UpdateAsync(countryId, languageId, request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Update propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Update_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "US";
        const string languageId = "en";
        var request = new CountryNameUpdateRequest
        {
            Name = "United States of America",
            Nationality = "American"
        };
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.UpdateAsync(countryId, languageId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(countryNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Update(countryId, languageId, request, CancellationToken.None));

        countryNameService.Verify(service => service.UpdateAsync(countryId, languageId, request, CancellationToken.None), Times.Once);
    }

    #endregion

    #region Delete

    // Success case: verifies Delete returns no content when translation is deleted;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Delete_ReturnsNoContentWhenTranslationIsDeleted()
    {
        const string countryId = "US";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var countryNameService = new Mock<ICountryNameService>();
        var controller = CreateController(countryNameService.Object);

        var result = await controller.Delete(countryId, languageId, cancellation.Token);

        Assert.IsType<NoContentResult>(result);
        countryNameService.Verify(service => service.RemoveAsync(countryId, languageId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Delete propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Delete_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "US";
        const string languageId = "en";
        var countryNameService = new Mock<ICountryNameService>();
        countryNameService
            .Setup(service => service.RemoveAsync(countryId, languageId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(countryNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Delete(countryId, languageId, CancellationToken.None));

        countryNameService.Verify(service => service.RemoveAsync(countryId, languageId, CancellationToken.None), Times.Once);
    }

    #endregion

    #region TestHelpers

    private static CountryNamesController CreateController(ICountryNameService countryNameService) => new(
        new ConfigurationBuilder().Build(),
        NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
        Mock.Of<IReferenceDataUnitOfWork>(),
        Mock.Of<IMapper>(),
        countryNameService);

    #endregion
}
