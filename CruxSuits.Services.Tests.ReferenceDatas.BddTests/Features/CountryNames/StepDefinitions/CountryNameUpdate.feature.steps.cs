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
    private string _updateCountryId = null!;
    private string _updateLanguageId = null!;
    private string _expectedUpdatedName = null!;
    private string _expectedUpdatedNationality = null!;

    [Given("country \"(.*)\" has the following translation:")]
    public async Task GivenCountryHasTranslation(string countryId, Table translations)
    {
        var row = translations.Rows.Single();
        _updateCountryId = countryId;
        _updateLanguageId = row["Language"];
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var translation = await context.CountryNames.SingleOrDefaultAsync(name =>
            name.CountryId == countryId && name.LanguageId == _updateLanguageId);

        Assert.NotNull(translation);
        translation.Name = row["Name"];
        translation.Nationality = row["Nationality"];
        await context.SaveChangesAsync();
    }

    [When("the client updates the translation with:")]
    public async Task WhenClientUpdatesTranslation(Table updates)
    {
        var row = updates.Rows.Single();
        _expectedUpdatedName = row["Name"];
        _expectedUpdatedNationality = row["Nationality"];
        _response = await _fixture.Client.PutAsJsonAsync(
            $"/api/v1/Countries/{_updateCountryId}/names/{_updateLanguageId}",
            new CountryNameUpdateRequest { Name = _expectedUpdatedName, Nationality = _expectedUpdatedNationality },
            ApiJsonOptions);
    }

    [When("the client updates the translation for country \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientUpdatesMissingTranslation(string countryId, string languageId)
    {
        _updateCountryId = countryId;
        _updateLanguageId = languageId;
        _response = await _fixture.Client.PutAsJsonAsync(
            $"/api/v1/Countries/{countryId}/names/{languageId}",
            new CountryNameUpdateRequest { Name = "Updated name", Nationality = "Updated nationality" },
            ApiJsonOptions);
    }

    [Then("the updated country name and nationality are returned")]
    public async Task ThenUpdatedCountryNameAndNationalityAreReturned()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_expectedUpdatedName, json.RootElement.GetProperty("Name").GetString());
        Assert.Equal(_expectedUpdatedNationality, json.RootElement.GetProperty("Nationality").GetString());
    }

    [Then("the country and language remain unchanged")]
    public async Task ThenCountryAndLanguageRemainUnchanged()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_updateLanguageId, json.RootElement.GetProperty("LanguageId").GetString());

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var updated = await context.CountryNames.SingleAsync(name =>
            name.CountryId == _updateCountryId && name.LanguageId == _updateLanguageId);
        Assert.Equal(_updateCountryId, updated.CountryId);
        Assert.Equal(_updateLanguageId, updated.LanguageId);
    }
}
