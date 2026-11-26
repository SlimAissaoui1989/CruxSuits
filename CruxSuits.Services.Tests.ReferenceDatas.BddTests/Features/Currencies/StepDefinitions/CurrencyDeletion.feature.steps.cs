using System.Net;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Currencies.StepDefinitions;

public sealed partial class CurrenciesControllerSteps
{
    [When("the client deletes currency \"(.*)\"")]
    public Task WhenClientDeletesCurrency(string currencyId)
    {
        _deletedCurrencyId = currencyId;
        return SendAsync(HttpMethod.Delete, $"/api/v1/Currencies/{currencyId}");
    }

    [Then("the deleted currency can no longer be retrieved")]
    public async Task ThenDeletedCurrencyCanNoLongerBeRetrieved()
    {
        using var response = await _fixture.Client.GetAsync($"/api/v1/Currencies/{_deletedCurrencyId}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
