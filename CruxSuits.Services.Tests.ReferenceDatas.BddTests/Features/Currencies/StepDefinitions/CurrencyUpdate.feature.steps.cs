using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Currencies.StepDefinitions;

public sealed partial class CurrenciesControllerSteps
{
    [When("the client updates currency \"(.*)\" with numeric code \"(.*)\", symbol \"(.*)\", decimal separator \"(.)\" and minor unit (.*)")]
    public Task WhenClientUpdatesCurrency(string currencyId, string numericCode, string symbol, char decimalSeparator, byte minorUnit) =>
        SendAsync(HttpMethod.Put, $"/api/v1/Currencies/{currencyId}", new
        {
            NumericCode = numericCode,
            Symbol = symbol,
            DecimalSeparator = decimalSeparator,
            MinorUnit = minorUnit
        });

    [Then("the updated currency details contain id \"(.*)\", numeric code \"(.*)\", symbol \"(.*)\", decimal separator \"(.)\" and minor unit (.*)")]
    public async Task ThenUpdatedCurrencyDetailsContainFields(string currencyId, string numericCode, string symbol, char decimalSeparator, byte minorUnit)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var currency = json.RootElement;
        Assert.Equal(currencyId, GetString(currency, "Id"));
        Assert.Equal(numericCode, GetString(currency, "NumericCode"));
        Assert.Equal(symbol, GetString(currency, "Symbol"));
        Assert.Equal(decimalSeparator.ToString(), GetString(currency, "DecimalSeparator"));
        Assert.Equal(minorUnit, GetByte(currency, "MinorUnit"));
    }
}
