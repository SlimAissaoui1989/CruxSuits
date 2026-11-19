using System.Net;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CountryNames.StepDefinitions;

public sealed partial class CountryNamesSteps
{
    [Given("country \"(.*)\" has the following translations:")]
    public async Task GivenCountryHasFollowingTranslations(string countryId, Table translations)
    {
        _expectedNames.Clear();
        foreach (var row in translations.Rows)
            _expectedNames.Add(new ExpectedCountryName(row["Language"], row["Name"], row["Nationality"]));

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var languageIds = _expectedNames.Select(name => name.LanguageId).ToArray();
        var existingNames = await context.CountryNames
            .Where(name => name.CountryId == countryId && languageIds.Contains(name.LanguageId))
            .ToListAsync();

        foreach (var expectedName in _expectedNames)
        {
            var existingName = existingNames.SingleOrDefault(name => name.LanguageId == expectedName.LanguageId);
            Assert.NotNull(existingName);
            existingName.Name = expectedName.Name;
            existingName.Nationality = expectedName.Nationality;
        }

        await context.SaveChangesAsync();
    }

    [When("the client requests all names for country \"(.*)\"")]
    public async Task WhenClientRequestsAllNamesForCountry(string countryId)
    {
        _response = await _fixture.Client.GetAsync($"/api/v1/Countries/{countryId}/names");
    }

    [Then("the response status is (.*)")]
    public async Task ThenResponseStatusIs(int expectedStatusCode)
    {
        var responseBody = await _response!.Content.ReadAsStringAsync();
        Assert.True((int)_response.StatusCode == expectedStatusCode,
            $"Expected {(HttpStatusCode)expectedStatusCode} but received {(int)_response.StatusCode}: {responseBody}");
    }

    [Then("the response contains (.*) country names")]
    public async Task ThenResponseContainsCountryNames(int expectedCount)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedCount, json.RootElement.GetArrayLength());
    }

    [Then("each country name contains its language, name and nationality")]
    public async Task ThenEachCountryNameContainsLanguageNameAndNationality()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var returnedNames = json.RootElement.EnumerateArray()
            .Select(name => new ExpectedCountryName(
                GetString(name, "LanguageId"),
                GetString(name, "Name"),
                GetString(name, "Nationality")))
            .ToArray();

        foreach (var expectedName in _expectedNames)
            Assert.Contains(expectedName, returnedNames);
    }
}
