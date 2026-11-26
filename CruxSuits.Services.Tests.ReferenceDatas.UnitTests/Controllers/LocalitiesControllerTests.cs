using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class LocalitiesControllerTests
{
    private readonly Mock<ILocalityService> _service = new();

    private LocalitiesController CreateController() => new(
        new ConfigurationBuilder().Build(),
        NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
        Mock.Of<IReferenceDataUnitOfWork>(),
        Mock.Of<IMapper>(),
        _service.Object);

    // Contract check: verifies autocomplete uses the resolved language and forwards cancellation;
    // A pass confirms the controller returns the matching suggestions;
    // Failure signals a regression in localization or request forwarding.
    [Fact]
    public async Task Autocomplete_UsesResolvedLanguageAndReturnsSuggestions()
    {
        using var cancellation = new CancellationTokenSource();
        var controller = CreateController();
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        var resolver = new Mock<ILanguageRequestResolver>();
        resolver.Setup(service => service.ResolveAsync(controller.Request, cancellation.Token)).ReturnsAsync("ar");
        var suggestions = new List<LocalitySuggestionResponse> { new() { Id = "TN-TU-BRD", Name = "باردو" } };
        _service.Setup(service => service.AutocompleteAsync("bardo", "ar", cancellation.Token)).ReturnsAsync(suggestions);

        var result = await controller.Autocomplete("bardo", resolver.Object, cancellation.Token);

        Assert.Same(suggestions, Assert.IsType<OkObjectResult>(result).Value);
        resolver.Verify(service => service.ResolveAsync(controller.Request, cancellation.Token), Times.Once);
        _service.Verify(service => service.AutocompleteAsync("bardo", "ar", cancellation.Token), Times.Once);
    }

    // Contract check: verifies listing returns the service results;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var areas = new List<LocalityDetailsResponse>();
        _service.Setup(service => service.GetAllAsync("TN", "TU", CancellationToken.None)).ReturnsAsync(areas);

        var result = await CreateController().GetAll("TN", "TU", CancellationToken.None);

        Assert.Same(areas, Assert.IsType<OkObjectResult>(result).Value);
    }

    // Contract check: verifies details forward the country and short area codes;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByCode_ReturnsOk()
    {
        var area = new LocalityDetailsResponse { Id = "TN-TU-BRD", Code = "BRD" };
        _service.Setup(service => service.GetByCodeAsync("TN", "TU", "BRD", CancellationToken.None)).ReturnsAsync(area);

        var result = await CreateController().GetByCode("TN", "TU", "BRD", CancellationToken.None);

        Assert.Same(area, Assert.IsType<OkObjectResult>(result).Value);
    }

    // Contract check: verifies creation returns a location using the short code;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        var request = new LocalityRequest { Code = "BRD" };
        var area = new LocalityDetailsResponse { Id = "TN-TU-BRD", Code = "BRD", AdministrativeAreaId = "TN-TU" };
        _service.Setup(service => service.CreateAsync("TN", "TU", request, CancellationToken.None)).ReturnsAsync(area);

        var result = Assert.IsType<CreatedAtActionResult>(await CreateController().Create("TN", "TU", request, CancellationToken.None));

        Assert.Equal(nameof(LocalitiesController.GetByCode), result.ActionName);
        Assert.Equal("TN", result.RouteValues!["countryId"]);
        Assert.Equal("TU", result.RouteValues["areaCode"]);
        Assert.Equal("BRD", result.RouteValues["localityCode"]);
        Assert.Same(area, result.Value);
    }

    // Contract check: verifies deletion returns no content and forwards cancellation;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        using var cancellation = new CancellationTokenSource();

        Assert.IsType<NoContentResult>(await CreateController().Delete("TN", "TU", "BRD", cancellation.Token));

        _service.Verify(service => service.RemoveAsync("TN", "TU", "BRD", cancellation.Token), Times.Once);
    }
}
