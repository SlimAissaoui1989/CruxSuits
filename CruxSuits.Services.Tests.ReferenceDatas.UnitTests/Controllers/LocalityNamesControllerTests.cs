using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class LocalityNamesControllerTests
{
    #region GetAll

    // Success case: verifies GetAll returns ok with all names for locality;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAll_ReturnsOkWithAllNamesForLocality()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        using var cancellation = new CancellationTokenSource();
        var names = new List<LocalityNameResponse>
        {
            new() { LanguageId = "en", Name = "Le Bardo" },
            new() { LanguageId = "fr", Name = "Le Bardo" },
            new() { LanguageId = "ar", Name = "باردو" }
        };
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.GetAllAsync(countryId, areaCode, localityCode, cancellation.Token))
            .ReturnsAsync(names);
        var controller = CreateController(localityNameService.Object);

        var result = await controller.GetAll(countryId, areaCode, localityCode, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(names, okResult.Value);
        localityNameService.Verify(service => service.GetAllAsync(countryId, areaCode, localityCode, cancellation.Token), Times.Once);
    }

    // Failure case: verifies GetAll propagates not found exception when locality does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetAll_WhenLocalityDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "ZZ";
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.GetAllAsync(countryId, areaCode, localityCode, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(localityNameService.Object);

        await Assert.ThrowsAsync<CxException>(() => controller.GetAll(countryId, areaCode, localityCode, CancellationToken.None));

        localityNameService.Verify(service => service.GetAllAsync(countryId, areaCode, localityCode, CancellationToken.None), Times.Once);
    }

    #endregion

    #region GetByLanguage

    // Success case: verifies GetByLanguage returns ok with the requested locality name;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByLanguage_ReturnsOkWithTheRequestedLocalityName()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        const string languageId = "fr";
        using var cancellation = new CancellationTokenSource();
        var expectedName = new LocalityNameResponse
        {
            LanguageId = languageId,
            LanguageName = "French",
            Name = "Le Bardo"
        };
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.GetByLanguageAsync(countryId, areaCode, localityCode, languageId, cancellation.Token))
            .ReturnsAsync(expectedName);
        var controller = CreateController(localityNameService.Object);

        var result = await controller.GetByLanguage(countryId, areaCode, localityCode, languageId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(expectedName, okResult.Value);
        localityNameService.Verify(
            service => service.GetByLanguageAsync(countryId, areaCode, localityCode, languageId, cancellation.Token),
            Times.Once);
    }

    // Failure case: verifies GetByLanguage propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByLanguage_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        const string languageId = "fr";
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.GetByLanguageAsync(countryId, areaCode, localityCode, languageId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(localityNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.GetByLanguage(countryId, areaCode, localityCode, languageId, CancellationToken.None));

        localityNameService.Verify(
            service => service.GetByLanguageAsync(countryId, areaCode, localityCode, languageId, CancellationToken.None),
            Times.Once);
    }

    #endregion

    #region Create

    // Success case: verifies Create returns created at locality name in language;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Create_ReturnsCreatedAtLocalityNameInLanguage()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        using var cancellation = new CancellationTokenSource();
        var request = new LocalityNameRequest
        {
            LanguageId = "fr",
            Name = "Le Bardo"
        };
        var response = new LocalityNameResponse
        {
            LanguageId = request.LanguageId,
            LanguageName = "French (Français)",
            Name = request.Name
        };
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.CreateAsync(countryId, areaCode, localityCode, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(localityNameService.Object);

        var result = await controller.Create(countryId, areaCode, localityCode, request, cancellation.Token);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(LocalityNamesController.GetByLanguage), createdResult.ActionName);
        Assert.Equal(countryId, createdResult.RouteValues!["countryId"]);
        Assert.Equal(areaCode, createdResult.RouteValues["areaCode"]);
        Assert.Equal(localityCode, createdResult.RouteValues["localityCode"]);
        Assert.Equal(request.LanguageId, createdResult.RouteValues["languageId"]);
        Assert.Same(response, createdResult.Value);
        localityNameService.Verify(service => service.CreateAsync(countryId, areaCode, localityCode, request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Create propagates conflict exception when translation already exists;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenTranslationAlreadyExists_PropagatesConflictException()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        var request = new LocalityNameRequest
        {
            LanguageId = "en",
            Name = "Le Bardo"
        };
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.CreateAsync(countryId, areaCode, localityCode, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.Conflict));
        var controller = CreateController(localityNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, areaCode, localityCode, request, CancellationToken.None));
        localityNameService.Verify(service => service.CreateAsync(countryId, areaCode, localityCode, request, CancellationToken.None), Times.Once);
    }

    // Failure case: verifies Create propagates not found exception when locality does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenLocalityDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "ZZ";
        var request = new LocalityNameRequest
        {
            LanguageId = "fr",
            Name = "Unknown locality"
        };
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.CreateAsync(countryId, areaCode, localityCode, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(localityNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, areaCode, localityCode, request, CancellationToken.None));

        localityNameService.Verify(
            service => service.CreateAsync(countryId, areaCode, localityCode, request, CancellationToken.None),
            Times.Once);
    }

    // Failure case: verifies Create propagates bad request exception when language does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenLanguageDoesNotExist_PropagatesBadRequestException()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        var request = new LocalityNameRequest
        {
            LanguageId = "zz",
            Name = "Unknown language"
        };
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.CreateAsync(countryId, areaCode, localityCode, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.BadRequest));
        var controller = CreateController(localityNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(countryId, areaCode, localityCode, request, CancellationToken.None));

        localityNameService.Verify(
            service => service.CreateAsync(countryId, areaCode, localityCode, request, CancellationToken.None),
            Times.Once);
    }

    #endregion

    #region Update

    // Success case: verifies Update returns ok with updated locality name;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Update_ReturnsOkWithUpdatedLocalityName()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var request = new LocalityNameUpdateRequest
        {
            Name = "Greater Bardo"
        };
        var response = new LocalityNameResponse
        {
            LanguageId = languageId,
            LanguageName = "English (English)",
            Name = request.Name
        };
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.UpdateAsync(countryId, areaCode, localityCode, languageId, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(localityNameService.Object);

        var result = await controller.Update(countryId, areaCode, localityCode, languageId, request, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(response, okResult.Value);
        localityNameService.Verify(service => service.UpdateAsync(countryId, areaCode, localityCode, languageId, request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Update propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Update_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        const string languageId = "en";
        var request = new LocalityNameUpdateRequest
        {
            Name = "Greater Bardo"
        };
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.UpdateAsync(countryId, areaCode, localityCode, languageId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(localityNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Update(countryId, areaCode, localityCode, languageId, request, CancellationToken.None));

        localityNameService.Verify(service => service.UpdateAsync(countryId, areaCode, localityCode, languageId, request, CancellationToken.None), Times.Once);
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
        const string areaCode = "TU";
        const string localityCode = "BRD";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var localityNameService = new Mock<ILocalityNameService>();
        var controller = CreateController(localityNameService.Object);

        var result = await controller.Delete(countryId, areaCode, localityCode, languageId, cancellation.Token);

        Assert.IsType<NoContentResult>(result);
        localityNameService.Verify(service => service.RemoveAsync(countryId, areaCode, localityCode, languageId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Delete propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Delete_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string countryId = "TN";
        const string areaCode = "TU";
        const string localityCode = "BRD";
        const string languageId = "en";
        var localityNameService = new Mock<ILocalityNameService>();
        localityNameService
            .Setup(service => service.RemoveAsync(countryId, areaCode, localityCode, languageId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(localityNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Delete(countryId, areaCode, localityCode, languageId, CancellationToken.None));

        localityNameService.Verify(service => service.RemoveAsync(countryId, areaCode, localityCode, languageId, CancellationToken.None), Times.Once);
    }

    #endregion

    #region TestHelpers

    private static LocalityNamesController CreateController(ILocalityNameService localityNameService) => new(
        new ConfigurationBuilder().Build(),
        NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
        Mock.Of<IReferenceDataUnitOfWork>(),
        Mock.Of<IMapper>(),
        localityNameService);

    #endregion
}
