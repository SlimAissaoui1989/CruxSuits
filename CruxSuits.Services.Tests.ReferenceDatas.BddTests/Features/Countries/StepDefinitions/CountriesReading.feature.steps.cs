using System.Net;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Countries.StepDefinitions;

public sealed partial class CountriesControllerSteps
{
    [Given("a test country \"(.*)\" has only an English name \"(.*)\"")]
    public async Task GivenTestCountryHasOnlyAnEnglishName(string countryId, string name)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        context.Countries.Add(new Country
        {
            Id = countryId,
            Alpha3Code = "QXZ",
            NumericCode = "997",
            PhoneCode = "+999",
            CurrencyId = "USD",
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        });
        context.CountryNames.Add(new CountryName
        {
            CountryId = countryId,
            LanguageId = "en",
            Name = name,
            Nationality = "Testlander"
        });
        await context.SaveChangesAsync();
    }

    [When("the client lists countries in language \"(.*)\"")]
    public Task WhenClientListsCountries(string languageId) =>
        SendAsync(HttpMethod.Get, $"/api/v1/{languageId}/Countries");

    [When("the client lists phone-code options in language \"(.*)\"")]
    public Task WhenClientListsPhoneCodeOptions(string languageId) =>
        SendAsync(HttpMethod.Get, $"/api/v1/Countries/{languageId}/phone-code-options");

    [When("the client requests country \"(.*)\"")]
    public Task WhenClientRequestsCountry(string countryId) =>
        SendAsync(HttpMethod.Get, $"/api/v1/Countries/{countryId}");

    [Then("the country API response status is (.*)")]
    public async Task ThenCountryApiResponseStatusIs(int statusCode)
    {
        var responseBody = await _response!.Content.ReadAsStringAsync();
        Assert.True((int)_response.StatusCode == statusCode,
            $"Expected {(HttpStatusCode)statusCode} but received {(int)_response.StatusCode}: {responseBody}");
    }

    [Then("the country list contains country \"(.*)\" named \"(.*)\" with alpha-3 code \"(.*)\" and numeric code \"(.*)\"")]
    public async Task ThenCountryListContainsCountry(string countryId, string name, string alpha3Code, string numericCode)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var country = json.RootElement.EnumerateArray()
            .Single(item => GetString(item, "Id") == countryId);

        Assert.Equal(name, GetString(country, "Name"));
        Assert.Equal(alpha3Code, GetString(country, "Alpha3Code"));
        Assert.Equal(numericCode, GetString(country, "NumericCode"));
    }

    [Then("the phone-code options contain country \"(.*)\" named \"(.*)\" with phone code \"(.*)\"")]
    public async Task ThenPhoneCodeOptionsContainCountry(string countryId, string name, string phoneCode)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var country = json.RootElement.EnumerateArray()
            .Single(item => GetString(item, "Id") == countryId);

        Assert.Equal(name, GetString(country, "Name"));
        Assert.Equal(phoneCode, GetString(country, "PhoneCode"));
    }

    [Then("the country details contain id \"(.*)\", alpha-3 code \"(.*)\", numeric code \"(.*)\" and currency \"(.*)\"")]
    public async Task ThenCountryDetailsContainExpectedFields(string countryId, string alpha3Code, string numericCode, string currencyId)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(countryId, GetString(json.RootElement, "Id"));
        Assert.Equal(alpha3Code, GetString(json.RootElement, "Alpha3Code"));
        Assert.Equal(numericCode, GetString(json.RootElement, "NumericCode"));
        Assert.Equal(currencyId, GetString(json.RootElement, "CurrencyId"));
    }
}
