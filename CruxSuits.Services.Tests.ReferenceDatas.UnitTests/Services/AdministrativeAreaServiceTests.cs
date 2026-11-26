using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class AdministrativeAreaServiceTests
{
    private readonly Mock<IReferenceDataUnitOfWork> _work = new();
    private readonly Mock<IAdministrativeAreaRepository> _areas = new();
    private readonly Mock<ICountryRepository> _countries = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<ILocalityService> _localityService = new();
    private readonly AdministrativeAreaService _service;

    public AdministrativeAreaServiceTests()
    {
        _work.SetupGet(work => work.AdministrativeArea).Returns(_areas.Object);
        _work.SetupGet(work => work.Country).Returns(_countries.Object);
        _areas.Setup(areas => areas.BuildId("TN", "SF")).Returns("TN-SF");
        _countries.Setup(countries => countries.GetByIdOrDefaultAsync("TN", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Country { Id = "TN" });
        _service = new AdministrativeAreaService(_work.Object, _mapper.Object, _localityService.Object);
    }

    // Contract check: verifies listing selects only areas belonging to the country;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_FiltersByCountry()
    {
        var expected = new AdministrativeAreaDetailsResponse { Id = "TN-SF", Code = "SF", CountryId = "TN" };
        _areas.Setup(areas => areas.GetAllAsync<AdministrativeAreaDetailsResponse>(
            It.IsAny<System.Linq.Expressions.Expression<Func<AdministrativeArea, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([expected]);

        Assert.Same(expected, Assert.Single(await _service.GetAllAsync("TN")));
        _areas.Verify(areas => areas.GetAllAsync<AdministrativeAreaDetailsResponse>(
            It.Is<System.Linq.Expressions.Expression<Func<AdministrativeArea, bool>>>(filter =>
                filter.Compile()(new AdministrativeArea { CountryId = "TN" })
                && !filter.Compile()(new AdministrativeArea { CountryId = "FR" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Contract check: verifies listing rejects an unknown country;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenCountryIsMissing_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<CxException>(() => _service.GetAllAsync("ZZ"));
    }

    // Contract check: verifies details use the repository-built area ID;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByCodeAsync_ReturnsMatchingArea()
    {
        var expected = new AdministrativeAreaDetailsResponse { Id = "TN-SF", Code = "SF" };
        _areas.Setup(areas => areas.GetByIdOrDefaultAsync<AdministrativeAreaDetailsResponse>("TN-SF", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        Assert.Same(expected, await _service.GetByCodeAsync("TN", "SF"));
    }

    // Contract check: verifies details reject a missing area;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByCodeAsync_WhenAreaIsMissing_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<CxException>(() => _service.GetByCodeAsync("TN", "SF"));
    }

    // Contract check: verifies creation passes the country and short code to the repository;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_PersistsArea()
    {
        var request = new AdministrativeAreaRequest { Code = "SF" };
        var entity = new AdministrativeArea { Code = "SF" };
        var expected = new AdministrativeAreaDetailsResponse { Id = "TN-SF", Code = "SF", CountryId = "TN" };
        _mapper.Setup(mapper => mapper.Map<AdministrativeArea>(request)).Returns(entity);
        _areas.Setup(areas => areas.AddAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _mapper.Setup(mapper => mapper.Map<AdministrativeAreaDetailsResponse>(entity)).Returns(expected);

        Assert.Same(expected, await _service.CreateAsync("TN", request));
        Assert.Equal("TN", entity.CountryId);
        Assert.Equal("SF", entity.Code);
        _areas.Verify(areas => areas.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Contract check: verifies creation rejects a duplicate code without saving;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_WhenCodeIsDuplicate_ThrowsConflict()
    {
        _areas.Setup(areas => areas.ExistsIncludingDeletedAsync("TN", "SF", It.IsAny<CancellationToken>())).ReturnsAsync(true);

        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync("TN", new AdministrativeAreaRequest { Code = "SF" }));

        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies creation rejects a missing country without saving;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_WhenCountryIsMissing_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync("ZZ", new AdministrativeAreaRequest { Code = "SF" }));

        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies deletion removes the requested area and saves;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_RemovesAndSavesArea()
    {
        var area = new AdministrativeArea { Id = "TN-SF" };
        _areas.Setup(areas => areas.GetByIdOrDefaultAsync("TN-SF", It.IsAny<CancellationToken>())).ReturnsAsync(area);

        await _service.RemoveAsync("TN", "SF");

        _localityService.Verify(service => service.RemoveByAdministrativeAreaIdAsync("TN-SF", It.IsAny<CancellationToken>()), Times.Once);
        _areas.Verify(areas => areas.RemoveAsync(area, It.IsAny<CancellationToken>()), Times.Once);
        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Contract check: verifies country removal skips already-deleted areas;
    // A pass confirms the area service stages only active areas and delegates locality removal;
    // Failure signals an incorrect area filter or an early commit.
    [Fact]
    public async Task RemoveByCountryIdAsync_StagesOnlyActiveAreasWithoutSaving()
    {
        using var cancellation = new CancellationTokenSource();
        var active = new AdministrativeArea { Id = "TN-SF", CountryId = "TN" };
        var deleted = new AdministrativeArea { Id = "TN-TU", CountryId = "TN", IsDeleted = true };
        var other = new AdministrativeArea { Id = "US-NY", CountryId = "US" };
        AdministrativeArea[] candidates = [active, deleted, other];
        _areas.Setup(areas => areas.GetAllAsync(
            It.IsAny<System.Linq.Expressions.Expression<Func<AdministrativeArea, bool>>>(), cancellation.Token))
            .ReturnsAsync((System.Linq.Expressions.Expression<Func<AdministrativeArea, bool>> predicate, CancellationToken _) =>
                candidates.Where(predicate.Compile()).ToList());

        await _service.RemoveByCountryIdAsync("TN", cancellation.Token);

        _localityService.Verify(service => service.RemoveByAdministrativeAreaIdAsync(active.Id, cancellation.Token), Times.Once);
        _localityService.Verify(service => service.RemoveByAdministrativeAreaIdAsync(deleted.Id, cancellation.Token), Times.Never);
        _localityService.Verify(service => service.RemoveByAdministrativeAreaIdAsync(other.Id, cancellation.Token), Times.Never);
        _areas.Verify(areas => areas.RemoveAsync(active, cancellation.Token), Times.Once);
        _areas.Verify(areas => areas.RemoveAsync(deleted, cancellation.Token), Times.Never);
        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies deletion rejects a missing area;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_WhenAreaIsMissing_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<CxException>(() => _service.RemoveAsync("TN", "SF"));

        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
