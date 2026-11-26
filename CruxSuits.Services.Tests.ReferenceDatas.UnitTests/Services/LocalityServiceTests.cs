using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class LocalityServiceTests
{
    private readonly Mock<IReferenceDataUnitOfWork> _work = new();
    private readonly Mock<ILocalityRepository> _areas = new();
    private readonly Mock<IAdministrativeAreaRepository> _administrativeAreas = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly LocalityService _service;

    public LocalityServiceTests()
    {
        _work.SetupGet(work => work.Locality).Returns(_areas.Object);
        _work.SetupGet(work => work.AdministrativeArea).Returns(_administrativeAreas.Object);
        _areas.Setup(areas => areas.BuildId("TN-TU", "BRD")).Returns("TN-TU-BRD");
        _administrativeAreas.Setup(areas => areas.BuildId("TN", "TU")).Returns("TN-TU");
        _administrativeAreas.Setup(areas => areas.BuildId("TN", "ZZ")).Returns("TN-ZZ");
        _administrativeAreas.Setup(areas => areas.GetByIdOrDefaultAsync("TN-TU", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AdministrativeArea { Id = "TN-TU" });
        _service = new LocalityService(_work.Object, _mapper.Object);
    }

    // Contract check: verifies autocomplete trims the query before sending it to the repository;
    // A pass confirms the repository receives the expected text and language;
    // Failure signals a regression in query preparation.
    [Fact]
    public async Task AutocompleteAsync_TrimsQueryAndForwardsLanguage()
    {
        using var cancellation = new CancellationTokenSource();
        var suggestions = new List<LocalitySuggestionResponse> { new() { Id = "TN-TU-BRD" } };
        _areas.Setup(repository => repository.AutocompleteAsync("bardo", "fr", cancellation.Token))
            .ReturnsAsync(suggestions);

        Assert.Same(suggestions, await _service.AutocompleteAsync("  bardo  ", "fr", cancellation.Token));
        _areas.Verify(repository => repository.AutocompleteAsync("bardo", "fr", cancellation.Token), Times.Once);
    }

    // Contract check: verifies an insufficient query is handled without a database lookup;
    // A pass confirms the service returns an empty list without querying the repository;
    // Failure signals unnecessary work for a short query.
    [Theory]
    [InlineData("   ")]
    [InlineData(" b ")]
    public async Task AutocompleteAsync_WhenTrimmedQueryIsTooShort_ReturnsEmptyList(string query)
    {
        Assert.Empty(await _service.AutocompleteAsync(query, "en"));
        _areas.Verify(repository => repository.AutocompleteAsync(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies listing selects only areas belonging to the administrative area;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_FiltersByAdministrativeArea()
    {
        var expected = new LocalityDetailsResponse { Id = "TN-TU-BRD", Code = "BRD", AdministrativeAreaId = "TN-TU" };
        _areas.Setup(areas => areas.GetAllAsync<LocalityDetailsResponse>(
            It.IsAny<System.Linq.Expressions.Expression<Func<Locality, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([expected]);

        Assert.Same(expected, Assert.Single(await _service.GetAllAsync("TN", "TU")));
        _areas.Verify(areas => areas.GetAllAsync<LocalityDetailsResponse>(
            It.Is<System.Linq.Expressions.Expression<Func<Locality, bool>>>(filter =>
                filter.Compile()(new Locality { AdministrativeAreaId = "TN-TU" })
                && !filter.Compile()(new Locality { AdministrativeAreaId = "TN-SF" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Contract check: verifies listing rejects an unknown administrative area;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenAdministrativeAreaIsMissing_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<CxException>(() => _service.GetAllAsync("TN", "ZZ"));
    }

    // Contract check: verifies details use the repository-built area ID;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByCodeAsync_ReturnsMatchingArea()
    {
        var expected = new LocalityDetailsResponse { Id = "TN-TU-BRD", Code = "BRD" };
        _areas.Setup(areas => areas.GetByIdOrDefaultAsync<LocalityDetailsResponse>("TN-TU-BRD", It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        Assert.Same(expected, await _service.GetByCodeAsync("TN", "TU", "BRD"));
    }

    // Contract check: verifies details reject a missing area;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByCodeAsync_WhenAreaIsMissing_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<CxException>(() => _service.GetByCodeAsync("TN", "TU", "BRD"));
    }

    // Contract check: verifies creation passes the administrative area and short code to the repository;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_PersistsArea()
    {
        var request = new LocalityRequest { Code = "BRD" };
        var entity = new Locality { Code = "BRD" };
        var expected = new LocalityDetailsResponse { Id = "TN-TU-BRD", Code = "BRD", AdministrativeAreaId = "TN-TU" };
        _mapper.Setup(mapper => mapper.Map<Locality>(request)).Returns(entity);
        _areas.Setup(areas => areas.AddAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _mapper.Setup(mapper => mapper.Map<LocalityDetailsResponse>(entity)).Returns(expected);

        Assert.Same(expected, await _service.CreateAsync("TN", "TU", request));
        Assert.Equal("TN-TU", entity.AdministrativeAreaId);
        Assert.Equal("BRD", entity.Code);
        _areas.Verify(areas => areas.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Contract check: verifies creation rejects an ID that exceeds the database limit;
    // A pass confirms an incompatible area and locality code cannot reach persistence;
    // Failure signals a risk of a database truncation error.
    [Fact]
    public async Task CreateAsync_WhenCombinedIdExceedsLimit_ThrowsBadRequestWithoutSaving()
    {
        _administrativeAreas.Setup(areas => areas.BuildId("TN", "ABC")).Returns("TN-ABC");
        _administrativeAreas.Setup(areas => areas.GetByIdOrDefaultAsync("TN-ABC", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AdministrativeArea { Id = "TN-ABC" });

        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync("TN", "ABC", new LocalityRequest { Code = "ABCDE" }));

        _areas.Verify(areas => areas.AddAsync(It.IsAny<Locality>(), It.IsAny<CancellationToken>()), Times.Never);
        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies a three-letter area and a four-letter locality code fit together;
    // A pass confirms the service accepts an 11-character locality ID;
    // Failure signals a regression at the supported length boundary.
    [Fact]
    public async Task CreateAsync_WhenCombinedIdHasElevenCharacters_SavesLocality()
    {
        const string areaId = "TN-ABC";
        var request = new LocalityRequest { Code = "ABCD" };
        var locality = new Locality { Code = request.Code };
        var expected = new LocalityDetailsResponse { Id = "TN-ABC-ABCD", AdministrativeAreaId = areaId, Code = "ABCD" };
        _administrativeAreas.Setup(areas => areas.BuildId("TN", "ABC")).Returns(areaId);
        _administrativeAreas.Setup(areas => areas.GetByIdOrDefaultAsync(areaId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AdministrativeArea { Id = areaId });
        _mapper.Setup(mapper => mapper.Map<Locality>(request)).Returns(locality);
        _areas.Setup(areas => areas.AddAsync(locality, It.IsAny<CancellationToken>())).ReturnsAsync(locality);
        _mapper.Setup(mapper => mapper.Map<LocalityDetailsResponse>(locality)).Returns(expected);

        Assert.Same(expected, await _service.CreateAsync("TN", "ABC", request));
        Assert.Equal(areaId, locality.AdministrativeAreaId);
        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Contract check: verifies creation rejects a duplicate code without saving;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_WhenCodeIsDuplicate_ThrowsConflict()
    {
        _areas.Setup(areas => areas.ExistsIncludingDeletedAsync("TN-TU", "BRD", It.IsAny<CancellationToken>())).ReturnsAsync(true);

        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync("TN", "TU", new LocalityRequest { Code = "BRD" }));

        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies creation rejects a missing administrative area without saving;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_WhenAdministrativeAreaIsMissing_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync("TN", "ZZ", new LocalityRequest { Code = "BRD" }));

        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies deletion removes the requested area and saves;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_RemovesAndSavesArea()
    {
        var area = new Locality { Id = "TN-TU-BRD" };
        _areas.Setup(areas => areas.GetByIdOrDefaultAsync("TN-TU-BRD", It.IsAny<CancellationToken>())).ReturnsAsync(area);

        await _service.RemoveAsync("TN", "TU", "BRD");

        _areas.Verify(areas => areas.RemoveAsync(area, It.IsAny<CancellationToken>()), Times.Once);
        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Contract check: verifies removal by area stages only active localities in that area;
    // A pass confirms that the locality service leaves the shared commit to its caller;
    // Failure signals incorrect filtering or an early commit.
    [Fact]
    public async Task RemoveByAdministrativeAreaIdAsync_StagesMatchingLocalitiesWithoutSaving()
    {
        using var cancellation = new CancellationTokenSource();
        var active = new Locality { Id = "TN-TU-BRD", AdministrativeAreaId = "TN-TU" };
        var deleted = new Locality { Id = "TN-TU-MRS", AdministrativeAreaId = "TN-TU", IsDeleted = true };
        var other = new Locality { Id = "TN-SF-SFX", AdministrativeAreaId = "TN-SF" };
        Locality[] localities = [active, deleted, other];
        _areas.Setup(areas => areas.GetAllAsync(
            It.IsAny<Expression<Func<Locality, bool>>>(), cancellation.Token))
            .ReturnsAsync((Expression<Func<Locality, bool>> predicate, CancellationToken _) =>
                localities.Where(predicate.Compile()).ToList());

        await _service.RemoveByAdministrativeAreaIdAsync("TN-TU", cancellation.Token);

        _areas.Verify(areas => areas.RemoveAsync(active, cancellation.Token), Times.Once);
        _areas.Verify(areas => areas.RemoveAsync(deleted, cancellation.Token), Times.Never);
        _areas.Verify(areas => areas.RemoveAsync(other, cancellation.Token), Times.Never);
        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Contract check: verifies deletion rejects a missing area;
    // A pass confirms the expected behavior;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_WhenAreaIsMissing_ThrowsNotFound()
    {
        await Assert.ThrowsAsync<CxException>(() => _service.RemoveAsync("TN", "TU", "BRD"));

        _work.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
