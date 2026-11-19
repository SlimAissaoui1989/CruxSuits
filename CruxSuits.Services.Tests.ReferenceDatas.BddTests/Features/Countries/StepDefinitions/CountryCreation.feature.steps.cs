using System.Net;
using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Countries.StepDefinitions;

public sealed partial class CountriesControllerSteps
{
    [When("the client creates country \"(.*)\" with alpha-3 code \"(.*)\", numeric code \"(.*)\", phone code \"(.*)\" and currency \"(.*)\"")]
    public Task WhenClientCreatesCountry(string countryId, string alpha3Code, string numericCode, string phoneCode, string currencyId) =>
        SendAsync(HttpMethod.Post, "/api/v1/Countries", new
        {
            Id = countryId,
            Alpha3Code = alpha3Code,
            NumericCode = numericCode,
            PhoneCode = phoneCode,
            CurrencyId = currencyId
        });

    [Then("the created country can be retrieved with alpha-3 code \"(.*)\" and numeric code \"(.*)\"")]
    public async Task ThenCreatedCountryCanBeRetrieved(string alpha3Code, string numericCode)
    {
        using var createdJson = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var countryId = GetString(createdJson.RootElement, "Id");
        using var detailsResponse = await _fixture.Client.GetAsync($"/api/v1/Countries/{countryId}");
        Assert.Equal(HttpStatusCode.OK, detailsResponse.StatusCode);

        using var detailsJson = JsonDocument.Parse(await detailsResponse.Content.ReadAsStringAsync());
        Assert.Equal(alpha3Code, GetString(detailsJson.RootElement, "Alpha3Code"));
        Assert.Equal(numericCode, GetString(detailsJson.RootElement, "NumericCode"));
    }
}
