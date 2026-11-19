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
