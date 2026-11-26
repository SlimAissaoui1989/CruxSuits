using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Controllers;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class AdministrativeAreasControllerTests
{
    private readonly Mock<IAdministrativeAreaService> _service = new();

    private AdministrativeAreasController CreateController() => new(
        new ConfigurationBuilder().Build(),
        NullLogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>>.Instance,
        Mock.Of<IReferenceDataUnitOfWork>(),
        Mock.Of<IMapper>(),
        _service.Object);

    // Contract check: verifies listing returns the service results;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var areas = new List<AdministrativeAreaDetailsResponse>();
        _service.Setup(service => service.GetAllAsync("TN", CancellationToken.None)).ReturnsAsync(areas);

        var result = await CreateController().GetAll("TN", CancellationToken.None);

        Assert.Same(areas, Assert.IsType<OkObjectResult>(result).Value);
    }

    // Contract check: verifies details forward the country and short area codes;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByCode_ReturnsOk()
    {
        var area = new AdministrativeAreaDetailsResponse { Id = "TN-SF", Code = "SF" };
        _service.Setup(service => service.GetByCodeAsync("TN", "SF", CancellationToken.None)).ReturnsAsync(area);

        var result = await CreateController().GetByCode("TN", "SF", CancellationToken.None);

        Assert.Same(area, Assert.IsType<OkObjectResult>(result).Value);
    }

    // Contract check: verifies creation returns a location using the short code;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        var request = new AdministrativeAreaRequest { Code = "SF" };
        var area = new AdministrativeAreaDetailsResponse { Id = "TN-SF", Code = "SF", CountryId = "TN" };
        _service.Setup(service => service.CreateAsync("TN", request, CancellationToken.None)).ReturnsAsync(area);

        var result = Assert.IsType<CreatedAtActionResult>(await CreateController().Create("TN", request, CancellationToken.None));

        Assert.Equal(nameof(AdministrativeAreasController.GetByCode), result.ActionName);
        Assert.Equal("TN", result.RouteValues!["countryId"]);
        Assert.Equal("SF", result.RouteValues["areaCode"]);
        Assert.Same(area, result.Value);
    }

    // Contract check: verifies deletion returns no content and forwards cancellation;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        using var cancellation = new CancellationTokenSource();

        Assert.IsType<NoContentResult>(await CreateController().Delete("TN", "SF", cancellation.Token));

        _service.Verify(service => service.RemoveAsync("TN", "SF", cancellation.Token), Times.Once);
    }
}
