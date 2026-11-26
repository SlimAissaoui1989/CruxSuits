using System.Net;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class CountryCascadeApiIntegrationTests
{
    private readonly ReferenceDatasIntegrationFixture _fixture;

    public CountryCascadeApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Delete_SoftDeletesOnlyTheSelectedHierarchyAndPreservesPriorAudit()
    {
        const string countryId = "QK";
        const string areaId = "QK-SF";
        const string activeLocalityId = "QK-SF-SFX";
        const string deletedLocalityId = "QK-SF-BRD";
        var previousDate = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var previousActor = Guid.NewGuid();

        await using (var scope = _fixture.Factory.Services.CreateAsyncScope())
        {
            var setup = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
            setup.AddRange(
                new Country { Id = countryId, Alpha3Code = "QKZ", NumericCode = "993", CurrencyId = "USD" },
                new AdministrativeArea { Id = areaId, CountryId = countryId, Code = "SF" },
                new Locality { Id = activeLocalityId, AdministrativeAreaId = areaId, Code = "SFX" },
                new Locality
                {
                    Id = deletedLocalityId, AdministrativeAreaId = areaId, Code = "BRD", IsDeleted = true,
                    DeletedDate = previousDate, DeletedUserId = previousActor, DeletedUserName = "previous"
                });
            await setup.SaveChangesAsync();
        }

        using var response = await _fixture.Client.DeleteAsync($"/api/v1/Countries/{countryId}");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var details = await _fixture.Client.GetAsync($"/api/v1/Countries/{countryId}");
        Assert.Equal(HttpStatusCode.NotFound, details.StatusCode);

        await using var verification = _fixture.Factory.Services.CreateAsyncScope();
        var context = verification.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var country = await context.Countries.IgnoreQueryFilters().SingleAsync(item => item.Id == countryId);
        var area = await context.AdministrativeAreas.IgnoreQueryFilters().SingleAsync(item => item.Id == areaId);
        var locality = await context.Localities.IgnoreQueryFilters().SingleAsync(item => item.Id == activeLocalityId);
        foreach (var deleted in new SoftDeleteEntity<string>[] { country, area, locality })
        {
            Assert.True(deleted.IsDeleted);
            Assert.NotNull(deleted.DeletedDate);
            Assert.Equal(country.DeletedUserId, deleted.DeletedUserId);
            Assert.Equal(country.DeletedUserName, deleted.DeletedUserName);
        }

        var previouslyDeleted = await context.Localities.IgnoreQueryFilters().SingleAsync(item => item.Id == deletedLocalityId);
        Assert.Equal(previousDate, previouslyDeleted.DeletedDate);
        Assert.Equal(previousActor, previouslyDeleted.DeletedUserId);
        Assert.Equal("previous", previouslyDeleted.DeletedUserName);
        Assert.Equal(countryId, area.CountryId);
        Assert.Equal(areaId, locality.AdministrativeAreaId);

        Assert.True(await context.Countries.AnyAsync(item => item.Id == "TN"));
        Assert.True(await context.AdministrativeAreas.AnyAsync(item => item.Id == "TN-SF"));
        Assert.True(await context.Localities.AnyAsync(item => item.Id == "TN-SF-SFX"));
    }
}
