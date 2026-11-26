using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.Tests.UnitTests.Controllers;

public sealed class LanguagesControllerTests
{
    #region GetAll

    // Success case: verifies GetAll returns the languages provided by the service;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAll_ReturnsOkWithLanguages()
    {
        using var cancellation = new CancellationTokenSource();
        var languages = new List<LanguageResponse>
        {
            CreateLanguageResponse("en", "English", "English", isRightToLeft: false),
            CreateLanguageResponse("ar", "Arabic", "العربية", isRightToLeft: true)
        };
        var languageService = new Mock<ILanguageService>();
        languageService.Setup(service => service.GetAllAsync(cancellation.Token)).ReturnsAsync(languages);
        var controller = CreateController(languageService.Object);

        var result = await controller.GetAll(cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(languages, okResult.Value);
        languageService.Verify(service => service.GetAllAsync(cancellation.Token), Times.Once);
    }

    // Contract check: verifies GetAll declares its successful response;
    // A pass confirms API metadata describes the response;
    // Failure means generated API documentation omits the 200 response.
    [Fact]
    public void GetAll_DeclaresOkResponse()
    {
        AssertDeclaresResponse(nameof(LanguagesController.GetAll), StatusCodes.Status200OK, typeof(List<LanguageResponse>));
    }

    #endregion

    #region GetById

    // Success case: verifies GetById returns language details when found;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetById_ReturnsOkWithLanguageDetails()
    {
        const string languageId = "ar";
        using var cancellation = new CancellationTokenSource();
        var language = CreateLanguageDetailsResponse(languageId, "Arabic", "العربية", isRightToLeft: true);
        var languageService = new Mock<ILanguageService>();
        languageService.Setup(service => service.GetByIdAsync(languageId, cancellation.Token)).ReturnsAsync(language);
        var controller = CreateController(languageService.Object);

        var result = await controller.GetById(languageId, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(language, okResult.Value);
        languageService.Verify(service => service.GetByIdAsync(languageId, cancellation.Token), Times.Once);
    }

    // Contract check: verifies GetById declares success and not found responses;
    // A pass confirms API metadata describes both outcomes;
    // Failure means a response is missing from the API contract.
    [Fact]
    public void GetById_DeclaresOkAndNotFoundResponses()
    {
        AssertDeclaresResponse(nameof(LanguagesController.GetById), StatusCodes.Status200OK, typeof(LanguageDetailsResponse));
        AssertDeclaresResponse(nameof(LanguagesController.GetById), StatusCodes.Status404NotFound);
    }

    #endregion

    #region Create

    // Success case: verifies Create returns a created response pointing to language details;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Create_ReturnsCreatedAtLanguageDetails()
    {
        using var cancellation = new CancellationTokenSource();
        var request = new LanguageRequest
        {
            Id = "QX",
            EnglishName = "Test language",
            NativeName = "Test",
            IsRightToLeft = false
        };
        var response = CreateLanguageResponse(request.Id, request.EnglishName, request.NativeName, request.IsRightToLeft);
        var languageService = new Mock<ILanguageService>();
        languageService.Setup(service => service.CreateAsync(request, cancellation.Token)).ReturnsAsync(response);
        var controller = CreateController(languageService.Object);

        var result = await controller.Create(request, cancellation.Token);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(LanguagesController.GetById), createdResult.ActionName);
        Assert.Same(response, createdResult.Value);
        Assert.Equal(request.Id, createdResult.RouteValues!["id"]);
        languageService.Verify(service => service.CreateAsync(request, cancellation.Token), Times.Once);
    }

    // Contract check: verifies Create declares created and conflict responses;
    // A pass confirms API metadata describes both outcomes;
    // Failure means a response is missing from the API contract.
    [Fact]
    public void Create_DeclaresCreatedAndConflictResponses()
    {
        AssertDeclaresResponse(nameof(LanguagesController.Create), StatusCodes.Status201Created, typeof(LanguageResponse));
        AssertDeclaresResponse(nameof(LanguagesController.Create), StatusCodes.Status409Conflict);
    }

    #endregion

    #region Update

    // Success case: verifies Update returns the updated language;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Update_ReturnsOkWithUpdatedLanguage()
    {
        const string languageId = "QX";
        using var cancellation = new CancellationTokenSource();
        var request = new UpdateLanguageRequest
        {
            EnglishName = "Updated language",
            NativeName = "Updated",
            IsRightToLeft = true
        };
        var response = CreateLanguageResponse(languageId, request.EnglishName, request.NativeName, request.IsRightToLeft);
        var languageService = new Mock<ILanguageService>();
        languageService.Setup(service => service.UpdateAsync(languageId, request, cancellation.Token)).ReturnsAsync(response);
        var controller = CreateController(languageService.Object);

        var result = await controller.Update(languageId, request, cancellation.Token);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Same(response, okResult.Value);
        languageService.Verify(service => service.UpdateAsync(languageId, request, cancellation.Token), Times.Once);
    }

    // Contract check: verifies Update declares success and not found responses;
    // A pass confirms API metadata describes both outcomes;
    // Failure means a response is missing from the API contract.
    [Fact]
    public void Update_DeclaresOkAndNotFoundResponses()
    {
        AssertDeclaresResponse(nameof(LanguagesController.Update), StatusCodes.Status200OK, typeof(LanguageResponse));
        AssertDeclaresResponse(nameof(LanguagesController.Update), StatusCodes.Status404NotFound);
    }

    #endregion

    #region Remove

    // Success case: verifies Remove returns no content after removing a language;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Remove_ReturnsNoContentAfterRemovingLanguage()
    {
        const string languageId = "QX";
        using var cancellation = new CancellationTokenSource();
        var languageService = new Mock<ILanguageService>();
        languageService.Setup(service => service.RemoveAsync(languageId, cancellation.Token)).Returns(Task.CompletedTask);
        var controller = CreateController(languageService.Object);

        var result = await controller.Remove(languageId, cancellation.Token);

        Assert.IsType<NoContentResult>(result);
        languageService.Verify(service => service.RemoveAsync(languageId, cancellation.Token), Times.Once);
    }

    // Contract check: verifies Remove declares no content and not found responses;
    // A pass confirms API metadata describes both outcomes;
    // Failure means a response is missing from the API contract.
    [Fact]
    public void Remove_DeclaresNoContentAndNotFoundResponses()
    {
        AssertDeclaresResponse(nameof(LanguagesController.Remove), StatusCodes.Status204NoContent);
        AssertDeclaresResponse(nameof(LanguagesController.Remove), StatusCodes.Status404NotFound);
    }

    #endregion

    #region TestHelpers

    private static LanguageResponse CreateLanguageResponse(string id, string englishName, string nativeName, bool isRightToLeft) =>
        new()
        {
            Id = id,
            EnglishName = englishName,
            NativeName = nativeName,
            IsRightToLeft = isRightToLeft
        };

    private static LanguageDetailsResponse CreateLanguageDetailsResponse(string id, string englishName, string nativeName, bool isRightToLeft) =>
        new()
        {
            Id = id,
            EnglishName = englishName,
            NativeName = nativeName,
            IsRightToLeft = isRightToLeft
        };

    private static void AssertDeclaresResponse(string actionName, int statusCode, Type? responseType = null)
    {
        var method = typeof(LanguagesController).GetMethod(actionName);
        Assert.NotNull(method);

        Assert.Contains(method.GetCustomAttributes(typeof(ProducesResponseTypeAttribute), inherit: true)
                .Cast<ProducesResponseTypeAttribute>(),
            attribute => attribute.StatusCode == statusCode
                && (responseType is null || attribute.Type == responseType));
    }

    private static LanguagesController CreateController(ILanguageService languageService)
    {
        var controller = new LanguagesController(
            new ConfigurationBuilder().Build(),
            NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
            Mock.Of<IReferenceDataUnitOfWork>(),
            Mock.Of<IMapper>(),
            languageService);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        return controller;
    }

    #endregion
}
