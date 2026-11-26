using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

public sealed class LocalityRepositoryTests
{
    // Contract check: verifies autocomplete resolves a locality, governorate, and country query;
    // A pass confirms the search uses translated names and returns the preferred language;
    // Failure signals a regression in search matching or localization.
    [Fact]
    public async Task AutocompleteAsync_MatchesAllTermsAndReturnsLocalizedSuggestion()
    {
        var options = new DbContextOptionsBuilder<ReferenceDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        await using var context = new ReferenceDataContext(options);
        await context.Database.EnsureCreatedAsync();
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.Setup(work => work.Set<Locality>()).Returns(context.Localities);
        var repository = new LocalityRepository(unitOfWork.Object);

        var suggestion = Assert.Single(await repository.AutocompleteAsync("bardo, tunis tunisia", "ar"));
        Assert.Equal("TN-TU-BRD", suggestion.Id);
        Assert.Equal("باردو", suggestion.Name);
        Assert.Equal("ولاية تونس", suggestion.AdministrativeAreaName);
        Assert.Equal("TN", suggestion.CountryId);

        Assert.Empty(await repository.AutocompleteAsync("bardo sfax", "en"));
        Assert.Empty(await repository.AutocompleteAsync("ardo", "en"));
        Assert.Equal(10, (await repository.AutocompleteAsync("tunisia", "en")).Count);

        var ranked = await repository.AutocompleteAsync("sfax tun", "en");
        Assert.Equal("TN-SF-SFX", ranked[0].LocalityId);
        Assert.Equal("Sfax, Sfax Governorate, Tunisia", ranked[0].DisplayLabel);
        Assert.All(ranked, item => Assert.Equal("TN-SF", item.AdministrativeAreaId));
        Assert.Equal(ranked.Skip(1).Select(item => item.Id).Order(),
            ranked.Skip(1).Select(item => item.Id));
    }

    // Contract check: verifies the repository builds a area-scoped ID and reserves deleted identifiers;
    // A pass confirms that the same ID remains reserved after soft deletion;
    // Failure signals that the ID format or duplicate protection regressed.
    [Fact]
    public async Task AddAsync_BuildsIdAndReservesItAfterSoftDeletion()
    {
        var options = new DbContextOptionsBuilder<ReferenceDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        await using var context = new ReferenceDataContext(options);
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(work => work.DbContext).Returns(context);
        unitOfWork.As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(work => work.Trace)
            .Returns(Mock.Of<CruxIT.Library.DataToolkit.RepositoryPattern.Repositories.ITraceRepository>());
        unitOfWork.SetupGet(work => work.DbContext).Returns(context);
        unitOfWork.Setup(work => work.Set<Locality>()).Returns(context.Localities);
        var repository = new LocalityRepository(unitOfWork.Object);

        var locality = new Locality { AdministrativeAreaId = "TN-TU", Code = "BRD" };
        await repository.AddAsync(locality);
        await context.SaveChangesAsync();

        Assert.Equal("TN-TU-BRD", locality.Id);
        Assert.True(await context.Localities.AnyAsync(area => area.Id == locality.Id));

        locality.IsDeleted = true;
        await context.SaveChangesAsync();

        Assert.False(await context.Localities.AnyAsync(area => area.Id == locality.Id));
        Assert.True(await repository.ExistsIncludingDeletedAsync("TN-TU", "BRD"));
        Assert.False(await repository.ExistsIncludingDeletedAsync("TN-SF", "BRD"));
    }
}
