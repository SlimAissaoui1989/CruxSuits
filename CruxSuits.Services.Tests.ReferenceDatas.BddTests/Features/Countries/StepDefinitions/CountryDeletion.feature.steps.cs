using System.Net;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Countries.StepDefinitions;

public sealed partial class CountriesControllerSteps
{
    [Given("country \"([^\"]*)\" has an unrelated administrative area and locality")]
    public async Task GivenUnrelatedHierarchy(string countryId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var areaId = $"{countryId}-QA";
        context.AddRange(
            new AdministrativeArea { Id = areaId, CountryId = countryId, Code = "QA" },
            new Locality { Id = $"{areaId}-QAA", AdministrativeAreaId = areaId, Code = "QAA" });
        await context.SaveChangesAsync();
    }

    [Then("country \"([^\"]*)\" and its (.*) areas and (.*) localities retain their rows with deletion audit metadata")]
    public async Task ThenHierarchyHasDeletionAudit(string countryId, int areaCount, int localityCount)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var country = await context.Countries.IgnoreQueryFilters().SingleAsync(item => item.Id == countryId);
        var areas = await context.AdministrativeAreas.IgnoreQueryFilters().Where(item => item.CountryId == countryId).ToListAsync();
        var areaIds = areas.Select(area => area.Id).ToList();
        var localities = await context.Localities.IgnoreQueryFilters().Where(item => areaIds.Contains(item.AdministrativeAreaId)).ToListAsync();

        Assert.True(country.IsDeleted);
        Assert.NotNull(country.DeletedDate);
        Assert.Equal(areaCount, areas.Count);
        Assert.Equal(localityCount, localities.Count);
        Assert.All(areas.Cast<CruxIT.Library.DataToolkit.EntityFramework.BaseTypes.SoftDeleteEntity<string>>().Concat(localities), item =>
        {
            Assert.True(item.IsDeleted);
            Assert.NotNull(item.DeletedDate);
            Assert.Equal(country.DeletedUserId, item.DeletedUserId);
            Assert.Equal(country.DeletedUserName, item.DeletedUserName);
        });
    }

    [Then("country \"([^\"]*)\" and its area and locality remain active")]
    public async Task ThenUnrelatedHierarchyRemainsActive(string countryId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var areaId = $"{countryId}-QA";
        Assert.True(await context.Countries.AnyAsync(item => item.Id == countryId));
        Assert.True(await context.AdministrativeAreas.AnyAsync(item => item.Id == areaId));
        Assert.True(await context.Localities.AnyAsync(item => item.Id == $"{areaId}-QAA"));
    }

    [Given("the test-only country \"(.*)\" exists")]
    public async Task GivenTestOnlyCountryExists(string countryId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        await context.Countries.AddAsync(new Country
        {
            Id = countryId,
            Alpha3Code = "QXZ",
            NumericCode = "997",
            PhoneCode = "+999",
            CurrencyId = "USD",
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        });
        await context.SaveChangesAsync();
    }

    [When("the client deletes country \"(.*)\"")]
    public Task WhenClientDeletesCountry(string countryId)
    {
        _deletedCountryId = countryId;
        return SendAsync(HttpMethod.Delete, $"/api/v1/Countries/{countryId}");
    }

    [Then("the deleted country can no longer be retrieved")]
    public async Task ThenDeletedCountryCanNoLongerBeRetrieved()
    {
        using var detailsResponse = await _fixture.Client.GetAsync($"/api/v1/Countries/{_deletedCountryId}");
        Assert.Equal(HttpStatusCode.NotFound, detailsResponse.StatusCode);
    }
}
