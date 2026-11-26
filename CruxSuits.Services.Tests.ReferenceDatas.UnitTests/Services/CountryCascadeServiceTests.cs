using AutoMapper;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class CountryCascadeServiceTests
{
    // Contract check: verifies the service cascade uses the shared audit actor and preserves existing deletion metadata;
    // A pass confirms that only active members of the selected hierarchy are soft-deleted;
    // Failure signals a regression in persistence, auditing, or isolation.
    [Fact]
    public async Task RemoveAsync_PreservesRowsAndAuditsOnlyActiveDescendants()
    {
        var options = new DbContextOptionsBuilder<ReferenceDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N")).Options;
        await using var context = new ReferenceDataContext(options);
        var work = new Mock<IReferenceDataUnitOfWork>();
        var baseWork = work.As<IUnitOfWork>();
        var actorId = Guid.NewGuid();
        const string actorName = "cascade-test";
        var previousDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var previousActorId = Guid.NewGuid();
        baseWork.SetupGet(unit => unit.DbContext).Returns(context);
        baseWork.SetupGet(unit => unit.Trace).Returns(Mock.Of<ITraceRepository>());
        baseWork.SetupGet(unit => unit.CurrentUserId).Returns(actorId);
        baseWork.SetupGet(unit => unit.CurrentUserName).Returns(actorName);
        work.SetupGet(unit => unit.DbContext).Returns(context);
        work.Setup(unit => unit.Set<Country>()).Returns(context.Countries);
        work.Setup(unit => unit.Set<AdministrativeArea>()).Returns(context.AdministrativeAreas);
        work.Setup(unit => unit.Set<Locality>()).Returns(context.Localities);
        work.SetupGet(unit => unit.Country).Returns(new CountryRepository(work.Object));
        work.SetupGet(unit => unit.AdministrativeArea).Returns(new AdministrativeAreaRepository(work.Object));
        work.SetupGet(unit => unit.Locality).Returns(new LocalityRepository(work.Object));
        work.Setup(unit => unit.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns((CancellationToken token) => context.SaveChangesAsync(token));
        var country = new Country { Id = "TN", Alpha3Code = "TUN", NumericCode = "788", CurrencyId = "TND" };
        var otherCountry = new Country { Id = "US", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "USD" };
        var area = new AdministrativeArea { Id = "TN-TU", CountryId = "TN", Code = "TU" };
        var oldArea = new AdministrativeArea
        {
            Id = "TN-SF", CountryId = "TN", Code = "SF", IsDeleted = true,
            DeletedDate = previousDate, DeletedUserId = previousActorId, DeletedUserName = "previous"
        };
        var otherArea = new AdministrativeArea { Id = "US-NY", CountryId = "US", Code = "NY" };
        var locality = new Locality { Id = "TN-TU-BRD", AdministrativeAreaId = "TN-TU", Code = "BRD" };
        var underOldArea = new Locality { Id = "TN-SF-SFX", AdministrativeAreaId = "TN-SF", Code = "SFX" };
        var oldLocality = new Locality
        {
            Id = "TN-TU-MRS", AdministrativeAreaId = "TN-TU", Code = "MRS", IsDeleted = true,
            DeletedDate = previousDate, DeletedUserId = previousActorId, DeletedUserName = "previous"
        };
        var otherLocality = new Locality { Id = "US-NY-NYC", AdministrativeAreaId = "US-NY", Code = "NYC" };
        context.AddRange(country, otherCountry, area, oldArea, otherArea, locality, underOldArea, oldLocality, otherLocality);
        await context.SaveChangesAsync();

        var mapper = Mock.Of<IMapper>();
        var localityService = new LocalityService(work.Object, mapper);
        var areaService = new AdministrativeAreaService(work.Object, mapper, localityService);
        var countryService = new CountryService(work.Object, mapper, areaService);
        await countryService.RemoveAsync(country.Id);
        context.ChangeTracker.Clear();

        var countries = await context.Countries.IgnoreQueryFilters().ToListAsync();
        var areas = await context.AdministrativeAreas.IgnoreQueryFilters().ToListAsync();
        var localities = await context.Localities.IgnoreQueryFilters().ToListAsync();
        foreach (var deleted in new SoftDeleteEntity<string>[]
        {
            countries.Single(item => item.Id == "TN"), areas.Single(item => item.Id == "TN-TU"),
            localities.Single(item => item.Id == "TN-TU-BRD")
        })
        {
            Assert.True(deleted.IsDeleted);
            Assert.NotNull(deleted.DeletedDate);
            Assert.Equal(actorId, deleted.DeletedUserId);
            Assert.Equal(actorName, deleted.DeletedUserName);
        }

        foreach (var previouslyDeleted in new SoftDeleteEntity<string>[]
        {
            areas.Single(item => item.Id == "TN-SF"), localities.Single(item => item.Id == "TN-TU-MRS")
        })
        {
            Assert.True(previouslyDeleted.IsDeleted);
            Assert.Equal(previousDate, previouslyDeleted.DeletedDate);
            Assert.Equal(previousActorId, previouslyDeleted.DeletedUserId);
            Assert.Equal("previous", previouslyDeleted.DeletedUserName);
        }

        Assert.False(countries.Single(item => item.Id == "US").IsDeleted);
        Assert.False(areas.Single(item => item.Id == "US-NY").IsDeleted);
        Assert.False(localities.Single(item => item.Id == "US-NY-NYC").IsDeleted);
        Assert.False(localities.Single(item => item.Id == "TN-SF-SFX").IsDeleted);
        Assert.Equal("TN-SF", localities.Single(item => item.Id == "TN-SF-SFX").AdministrativeAreaId);
    }
}
