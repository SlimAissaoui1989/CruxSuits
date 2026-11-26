using System.Net;
using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Currencies.StepDefinitions;

public sealed partial class CurrenciesControllerSteps
{
    [When("the client creates currency \"(.*)\" with numeric code \"(.*)\", symbol \"(.*)\", decimal separator \"(.)\" and minor unit (.*)")]
    public Task WhenClientCreatesCurrency(string currencyId, string numericCode, string symbol, char decimalSeparator, byte minorUnit) =>
        SendAsync(HttpMethod.Post, "/api/v1/Currencies", new
        {
            Id = currencyId,
            NumericCode = numericCode,
            Symbol = symbol,
            DecimalSeparator = decimalSeparator,
            MinorUnit = minorUnit
        });

    [Then("the created currency can be retrieved with numeric code \"(.*)\"")]
    public async Task ThenCreatedCurrencyCanBeRetrieved(string numericCode)
    {
        using var createdJson = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var currencyId = GetString(createdJson.RootElement, "Id");
        using var detailsResponse = await _fixture.Client.GetAsync($"/api/v1/Currencies/{currencyId}");
        Assert.Equal(HttpStatusCode.OK, detailsResponse.StatusCode);
        using var detailsJson = JsonDocument.Parse(await detailsResponse.Content.ReadAsStringAsync());
        Assert.Equal(numericCode, GetString(detailsJson.RootElement, "NumericCode"));
    }
}
