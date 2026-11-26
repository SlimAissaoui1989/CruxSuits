using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class CurrencyNamesControllerTests
{
    #region GetAll

    // Success case: verifies GetAll returns ok with all names for currency;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAll_ReturnsOkWithAllNamesForCurrency()
    {
        const string currencyId = "USD";
        using var cancellation = new CancellationTokenSource();
        var names = new List<CurrencyNameResponse>
        {
            new() { LanguageId = "en", Name = "US Dollar" },
            new() { LanguageId = "fr", Name = "dollar des États-Unis" },
            new() { LanguageId = "ar", Name = "دولار أمريكي" }
        };
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.GetAllAsync(currencyId, cancellation.Token))
            .ReturnsAsync(names);
        var controller = CreateController(currencyNameService.Object);

        var result = await controller.GetAll(currencyId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(names, okResult.Value);
        currencyNameService.Verify(service => service.GetAllAsync(currencyId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies GetAll propagates not found exception when currency does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetAll_WhenCurrencyDoesNotExist_PropagatesNotFoundException()
    {
        const string currencyId = "ZZZ";
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.GetAllAsync(currencyId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(currencyNameService.Object);

        await Assert.ThrowsAsync<CxException>(() => controller.GetAll(currencyId, CancellationToken.None));

        currencyNameService.Verify(service => service.GetAllAsync(currencyId, CancellationToken.None), Times.Once);
    }

    #endregion

    #region GetByLanguage

    // Success case: verifies GetByLanguage returns ok with the requested currency name;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByLanguage_ReturnsOkWithTheRequestedCurrencyName()
    {
        const string currencyId = "USD";
        const string languageId = "fr";
        using var cancellation = new CancellationTokenSource();
        var expectedName = new CurrencyNameResponse
        {
            LanguageId = languageId,
            LanguageName = "French",
            Name = "dollar des États-Unis"
        };
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.GetByLanguageAsync(currencyId, languageId, cancellation.Token))
            .ReturnsAsync(expectedName);
        var controller = CreateController(currencyNameService.Object);

        var result = await controller.GetByLanguage(currencyId, languageId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(expectedName, okResult.Value);
        currencyNameService.Verify(
            service => service.GetByLanguageAsync(currencyId, languageId, cancellation.Token),
            Times.Once);
    }

    // Failure case: verifies GetByLanguage propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByLanguage_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string currencyId = "USD";
        const string languageId = "fr";
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.GetByLanguageAsync(currencyId, languageId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(currencyNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.GetByLanguage(currencyId, languageId, CancellationToken.None));

        currencyNameService.Verify(
            service => service.GetByLanguageAsync(currencyId, languageId, CancellationToken.None),
            Times.Once);
    }

    #endregion

    #region Create

    // Success case: verifies Create returns created at currency name in language;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Create_ReturnsCreatedAtCurrencyNameInLanguage()
    {
        const string currencyId = "USD";
        using var cancellation = new CancellationTokenSource();
        var request = new CurrencyNameRequest
        {
            LanguageId = "fr",
            Name = "dollar des États-Unis"
        };
        var response = new CurrencyNameResponse
        {
            LanguageId = request.LanguageId,
            LanguageName = "French (Français)",
            Name = request.Name
        };
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.CreateAsync(currencyId, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(currencyNameService.Object);

        var result = await controller.Create(currencyId, request, cancellation.Token);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(CurrencyNamesController.GetByLanguage), createdResult.ActionName);
        Assert.Equal(currencyId, createdResult.RouteValues!["currencyId"]);
        Assert.Equal(request.LanguageId, createdResult.RouteValues["languageId"]);
        Assert.Same(response, createdResult.Value);
        currencyNameService.Verify(service => service.CreateAsync(currencyId, request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Create propagates conflict exception when translation already exists;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenTranslationAlreadyExists_PropagatesConflictException()
    {
        const string currencyId = "USD";
        var request = new CurrencyNameRequest
        {
            LanguageId = "en",
            Name = "US Dollar"
        };
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.CreateAsync(currencyId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.Conflict));
        var controller = CreateController(currencyNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(currencyId, request, CancellationToken.None));
        currencyNameService.Verify(service => service.CreateAsync(currencyId, request, CancellationToken.None), Times.Once);
    }

    // Failure case: verifies Create propagates not found exception when currency does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenCurrencyDoesNotExist_PropagatesNotFoundException()
    {
        const string currencyId = "ZZZ";
        var request = new CurrencyNameRequest
        {
            LanguageId = "fr",
            Name = "Unknown currency"
        };
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.CreateAsync(currencyId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(currencyNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(currencyId, request, CancellationToken.None));

        currencyNameService.Verify(
            service => service.CreateAsync(currencyId, request, CancellationToken.None),
            Times.Once);
    }

    // Failure case: verifies Create propagates bad request exception when language does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Create_WhenLanguageDoesNotExist_PropagatesBadRequestException()
    {
        const string currencyId = "USD";
        var request = new CurrencyNameRequest
        {
            LanguageId = "zz",
            Name = "Unknown language"
        };
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.CreateAsync(currencyId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.BadRequest));
        var controller = CreateController(currencyNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Create(currencyId, request, CancellationToken.None));

        currencyNameService.Verify(
            service => service.CreateAsync(currencyId, request, CancellationToken.None),
            Times.Once);
    }

    #endregion

    #region Update

    // Success case: verifies Update returns ok with updated currency name;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Update_ReturnsOkWithUpdatedCurrencyName()
    {
        const string currencyId = "USD";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var request = new CurrencyNameUpdateRequest
        {
            Name = "United States dollar"
        };
        var response = new CurrencyNameResponse
        {
            LanguageId = languageId,
            LanguageName = "English (English)",
            Name = request.Name
        };
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.UpdateAsync(currencyId, languageId, request, cancellation.Token))
            .ReturnsAsync(response);
        var controller = CreateController(currencyNameService.Object);

        var result = await controller.Update(currencyId, languageId, request, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(response, okResult.Value);
        currencyNameService.Verify(service => service.UpdateAsync(currencyId, languageId, request, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Update propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Update_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string currencyId = "USD";
        const string languageId = "en";
        var request = new CurrencyNameUpdateRequest
        {
            Name = "United States dollar"
        };
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.UpdateAsync(currencyId, languageId, request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(currencyNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Update(currencyId, languageId, request, CancellationToken.None));

        currencyNameService.Verify(service => service.UpdateAsync(currencyId, languageId, request, CancellationToken.None), Times.Once);
    }

    #endregion

    #region Delete

    // Success case: verifies Delete returns no content when translation is deleted;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Delete_ReturnsNoContentWhenTranslationIsDeleted()
    {
        const string currencyId = "USD";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var currencyNameService = new Mock<ICurrencyNameService>();
        var controller = CreateController(currencyNameService.Object);

        var result = await controller.Delete(currencyId, languageId, cancellation.Token);

        Assert.IsType<NoContentResult>(result);
        currencyNameService.Verify(service => service.RemoveAsync(currencyId, languageId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies Delete propagates not found exception when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task Delete_WhenTranslationDoesNotExist_PropagatesNotFoundException()
    {
        const string currencyId = "USD";
        const string languageId = "en";
        var currencyNameService = new Mock<ICurrencyNameService>();
        currencyNameService
            .Setup(service => service.RemoveAsync(currencyId, languageId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new CxException(CxHttpStatusCode.NotFound));
        var controller = CreateController(currencyNameService.Object);

        await Assert.ThrowsAsync<CxException>(() =>
            controller.Delete(currencyId, languageId, CancellationToken.None));

        currencyNameService.Verify(service => service.RemoveAsync(currencyId, languageId, CancellationToken.None), Times.Once);
    }

    #endregion

    #region TestHelpers

    private static CurrencyNamesController CreateController(ICurrencyNameService currencyNameService) => new(
        new ConfigurationBuilder().Build(),
        NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
        Mock.Of<IReferenceDataUnitOfWork>(),
        Mock.Of<IMapper>(),
        currencyNameService);

    #endregion
}
