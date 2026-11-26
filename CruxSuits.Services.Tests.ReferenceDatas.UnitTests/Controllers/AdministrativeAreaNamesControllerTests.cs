using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeAreaName;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class AdministrativeAreaNamesControllerTests
{
    #region GetAll

    // Success case: verifies GetAll returns ok with all names for administrative area;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAll_ReturnsOkWithAllNamesForAdministrativeArea()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        using var cancellation = new CancellationTokenSource();
        var names = new List<AdministrativeAreaNameResponse>
        {
            new() { LanguageId = "en", Name = "Sfax" },
            new() { LanguageId = "fr", Name = "Sfax" },
            new() { LanguageId = "ar", Name = "صفاقس" }
        };
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.GetAllAsync(countryId, areaCode, cancellation.Token))
            .ReturnsAsync(names);
        var controller = CreateController(administrativeAreaNameService.Object);

        var result = await controller.GetAll(countryId, areaCode, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(names, okResult.Value);
        administrativeAreaNameService.Verify(service => service.GetAllAsync(countryId, areaCode, cancellation.Token), Times.Once);
    }

    // Failure case: verifies GetAll propagates not found exception when administrative area does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetAll_WhenAdministrativeAreaDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "ZZ";
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.GetAllAsync(countryId, areaCode, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(administrativeAreaNameService.Object);

        await Assert.ThrowsAsync<CxException>(() => controller.GetAll(countryId, areaCode, CancellationToken.None));

        administrativeAreaNameService.Verify(service => service.GetAllAsync(countryId, areaCode, CancellationToken.None), Times.Once);
    }

    #endregion

    #region GetByLanguage

    // Success case: verifies GetByLanguage returns ok with the requested administrative area name;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByLanguage_ReturnsOkWithTheRequestedAdministrativeAreaName()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        const string languageId = "fr";
        using var cancellation = new CancellationTokenSource();
        var expectedName = new AdministrativeAreaNameResponse
        {
            LanguageId = languageId,
            LanguageName = "French",
            Name = "Sfax"
        };
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.GetByLanguageAsync(countryId, areaCode, languageId, cancellation.Token))
            .ReturnsAsync(expectedName);
        var controller = CreateController(administrativeAreaNameService.Object);

        var result = await controller.GetByLanguage(countryId, areaCode, languageId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(expectedName, okResult.Value);
        administrativeAreaNameService.Verify(
            service => service.GetByLanguageAsync(countryId, areaCode, languageId, cancellation.Token),
            Times.Once);
    }

    // Failure case: verifies GetByLanguage propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByLanguage_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        const string languageId = "fr";
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.GetByLanguageAsync(countryId, areaCode, languageId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(administrativeAreaNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.GetByLanguage(countryId, areaCode, languageId, CancellationToken.None));

        administrativeAreaNameService.Verify(
            service => service.GetByLanguageAsync(countryId, areaCode, languageId, CancellationToken.None),
            Times.Once);
    }

    #endregion

    #region Create

    // Success case: verifies Create returns created at administrative area name in language;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Create_ReturnsCreatedAtAdministrativeAreaNameInLanguage()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        using var cancellation = new CancellationTokenSource();
        var request = new AdministrativeAreaNameRequest
        {
            LanguageId = "fr",
            Name = "Sfax"
        };
        var response = new AdministrativeAreaNameResponse
        {
            LanguageId = request.LanguageId,
            LanguageName = "French (Français)",
            Name = request.Name
        };
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.CreateAsync(countryId, areaCode, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(administrativeAreaNameService.Object);

        var result = await controller.Create(countryId, areaCode, request, cancellation.Token);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(AdministrativeAreaNamesController.GetByLanguage), createdResult.ActionName);
        Assert.Equal(countryId, createdResult.RouteValues!["countryId"]);
        Assert.Equal(areaCode, createdResult.RouteValues["areaCode"]);
        Assert.Equal(request.LanguageId, createdResult.RouteValues["languageId"]);
        Assert.Same(response, createdResult.Value);
        administrativeAreaNameService.Verify(service => service.CreateAsync(countryId, areaCode, request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Create propagates conflict exception when translation already exists;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenTranslationAlreadyExists_PropagatesConflictException()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        var request = new AdministrativeAreaNameRequest
        {
            LanguageId = "en",
            Name = "Sfax"
        };
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.CreateAsync(countryId, areaCode, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.Conflict));
        var controller = CreateController(administrativeAreaNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, areaCode, request, CancellationToken.None));
        administrativeAreaNameService.Verify(service => service.CreateAsync(countryId, areaCode, request, CancellationToken.None), Times.Once);
    }

    // Failure case: verifies Create propagates not found exception when administrative area does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenAdministrativeAreaDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "ZZ";
        var request = new AdministrativeAreaNameRequest
        {
            LanguageId = "fr",
            Name = "Unknown administrative area"
        };
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.CreateAsync(countryId, areaCode, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(administrativeAreaNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, areaCode, request, CancellationToken.None));

        administrativeAreaNameService.Verify(
            service => service.CreateAsync(countryId, areaCode, request, CancellationToken.None),
            Times.Once);
    }

    // Failure case: verifies Create propagates bad request exception when language does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenLanguageDoesNotExist_PropagatesBadRequestException()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        var request = new AdministrativeAreaNameRequest
        {
            LanguageId = "zz",
            Name = "Unknown language"
        };
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.CreateAsync(countryId, areaCode, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.BadRequest));
        var controller = CreateController(administrativeAreaNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, areaCode, request, CancellationToken.None));

        administrativeAreaNameService.Verify(
            service => service.CreateAsync(countryId, areaCode, request, CancellationToken.None),
            Times.Once);
    }

    #endregion

    #region Update

    // Success case: verifies Update returns ok with updated administrative area name;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Update_ReturnsOkWithUpdatedAdministrativeAreaName()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var request = new AdministrativeAreaNameUpdateRequest
        {
            Name = "Greater Sfax"
        };
        var response = new AdministrativeAreaNameResponse
        {
            LanguageId = languageId,
            LanguageName = "English (English)",
            Name = request.Name
        };
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.UpdateAsync(countryId, areaCode, languageId, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(administrativeAreaNameService.Object);

        var result = await controller.Update(countryId, areaCode, languageId, request, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(response, okResult.Value);
        administrativeAreaNameService.Verify(service => service.UpdateAsync(countryId, areaCode, languageId, request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Update propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Update_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        const string languageId = "en";
        var request = new AdministrativeAreaNameUpdateRequest
        {
            Name = "Greater Sfax"
        };
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.UpdateAsync(countryId, areaCode, languageId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(administrativeAreaNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Update(countryId, areaCode, languageId, request, CancellationToken.None));

        administrativeAreaNameService.Verify(service => service.UpdateAsync(countryId, areaCode, languageId, request, CancellationToken.None), Times.Once);
    }

    #endregion

    #region Delete

    // Success case: verifies Delete returns no content when translation is deleted;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Delete_ReturnsNoContentWhenTranslationIsDeleted()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        var controller = CreateController(administrativeAreaNameService.Object);

        var result = await controller.Delete(countryId, areaCode, languageId, cancellation.Token);

        Assert.IsType<NoContentResult>(result);
        administrativeAreaNameService.Verify(service => service.RemoveAsync(countryId, areaCode, languageId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Delete propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Delete_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "SF";
        const string languageId = "en";
        var administrativeAreaNameService = new Mock<IAdministrativeAreaNameService>();
        administrativeAreaNameService
            .Setup(service => service.RemoveAsync(countryId, areaCode, languageId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(administrativeAreaNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Delete(countryId, areaCode, languageId, CancellationToken.None));

        administrativeAreaNameService.Verify(service => service.RemoveAsync(countryId, areaCode, languageId, CancellationToken.None), Times.Once);
    }

    #endregion

    #region TestHelpers

    private static AdministrativeAreaNamesController CreateController(IAdministrativeAreaNameService administrativeAreaNameService) => new(
        new ConfigurationBuilder().Build(),
        NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
        Mock.Of<IReferenceDataUnitOfWork>(),
        Mock.Of<IMapper>(),
        administrativeAreaNameService);

    #endregion
}
