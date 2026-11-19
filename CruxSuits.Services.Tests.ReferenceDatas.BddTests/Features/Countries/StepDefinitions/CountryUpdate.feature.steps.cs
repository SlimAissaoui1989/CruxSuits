using Reqnroll;
using System.Text.Json;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Countries.StepDefinitions;

public sealed partial class CountriesControllerSteps
{
    [When("the client updates country \"(.*)\" with phone code \"(.*)\"")]
    public Task WhenClientUpdatesCountry(string countryId, string phoneCode) =>
        SendAsync(HttpMethod.Put, $"/api/v1/Countries/{countryId}", new
        {
            Alpha3Code = countryId == "US" ? "USA" : "ZZZ",
            NumericCode = countryId == "US" ? "840" : "998",
            PhoneCode = phoneCode,
            CurrencyId = "USD"
        });

    [Then("the updated country has phone code \"(.*)\"")]
    public async Task ThenUpdatedCountryHasPhoneCode(string phoneCode)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(phoneCode, GetString(json.RootElement, "PhoneCode"));
    }
}
