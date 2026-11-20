using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CountryNames.StepDefinitions;

public sealed partial class CountryNamesSteps
{
    [Given("country \"(.*)\" exists")]
    public async Task GivenCountryExists(string countryId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.Countries.AnyAsync(country => country.Id == countryId));
    }

    [Given("no translation exists for country \"(.*)\" in language \"(.*)\"")]
    public async Task GivenNoTranslationExists(string countryId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        await context.CountryNames.IgnoreQueryFilters()
            .Where(name => name.CountryId == countryId && name.LanguageId == languageId)
            .ExecuteDeleteAsync();
    }

    [Given("country \"(.*)\" already has a translation in language \"(.*)\"")]
    public async Task GivenCountryAlreadyHasTranslation(string countryId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.CountryNames.AnyAsync(name =>
            name.CountryId == countryId && name.LanguageId == languageId));
    }

    [When("the client creates a country name \"(.*)\" with nationality \"(.*)\" in language \"(.*)\"")]
    public async Task WhenClientCreatesCountryName(string name, string nationality, string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Countries/US/names",
            new CountryNameRequest { LanguageId = languageId, Name = name, Nationality = nationality },
            ApiJsonOptions);
    }

    [When("the client creates another translation for country \"(.*)\" in language \"(.*)\"")]
    public async Task WhenClientCreatesDuplicateTranslation(string countryId, string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            $"/api/v1/Countries/{countryId}/names",
            new CountryNameRequest { LanguageId = languageId, Name = "Duplicate", Nationality = "Duplicate" },
            ApiJsonOptions);
    }

    [When("the client creates a translation for country \"(.*)\"")]
    public async Task WhenClientCreatesTranslationForCountry(string countryId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            $"/api/v1/Countries/{countryId}/names",
            new CountryNameRequest { LanguageId = "en", Name = "Test name", Nationality = "Test nationality" },
            ApiJsonOptions);
    }

    [When("the client creates a translation for language \"(.*)\"")]
    public async Task WhenClientCreatesTranslationForLanguage(string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Countries/US/names",
            new CountryNameRequest { LanguageId = languageId, Name = "Test name", Nationality = "Test nationality" },
            ApiJsonOptions);
    }

    [When("the client creates a country name with a (.*) \"(.*)\"")]
    public async Task WhenClientCreatesCountryNameWithInvalidDesignation(string condition, string field)
    {
        var invalidValue = condition == "blank" ? string.Empty : "   ";
        var request = new CountryNameRequest
        {
            LanguageId = "en",
            Name = field == "name" ? invalidValue : "Valid name",
            Nationality = field == "nationality" ? invalidValue : "Valid nationality"
        };
        _response = await _fixture.Client.PostAsJsonAsync("/api/v1/Countries/US/names", request, ApiJsonOptions);
    }

    [Then("the country name is stored for country \"(.*)\" and language \"(.*)\"")]
    public async Task ThenCountryNameIsStored(string countryId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        Assert.NotNull(await context.CountryNames.SingleOrDefaultAsync(name =>
            name.CountryId == countryId && name.LanguageId == languageId));
    }

    [Then("the stored name is \"(.*)\"")]
    public async Task ThenStoredNameIs(string expectedName)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedName, json.RootElement.GetProperty("Name").GetString());
    }

    [Then("the stored nationality is \"(.*)\"")]
    public async Task ThenStoredNationalityIs(string expectedNationality)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedNationality, json.RootElement.GetProperty("Nationality").GetString());
    }
}
