using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.AdministrativeAreaNames.StepDefinitions;

public sealed partial class AdministrativeAreaNamesSteps
{
    private string _updateAdministrativeAreaId = null!;
    private string _updateLanguageId = null!;
    private string _expectedUpdatedName = null!;

    [Given("administrative area \"(.*)\" has the following translation:")]
    public async Task GivenAdministrativeAreaHasTranslation(string administrativeAreaId, Table translations)
    {
        var row = translations.Rows.Single();
        _updateAdministrativeAreaId = administrativeAreaId;
        _updateLanguageId = row["Language"];
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var translation = await context.AdministrativeAreaNames.SingleOrDefaultAsync(name =>
            name.AdministrativeAreaId == administrativeAreaId && name.LanguageId == _updateLanguageId);

        Assert.NotNull(translation);
        translation.Name = row["Name"];
        await context.SaveChangesAsync();
    }

    [When("the client updates the translation with:")]
    public async Task WhenClientUpdatesTranslation(Table updates)
    {
        var row = updates.Rows.Single();
        _expectedUpdatedName = row["Name"];
        _response = await _fixture.Client.PutAsJsonAsync(
            $"{NamesUrl(_updateAdministrativeAreaId)}/{_updateLanguageId}",
            new { Name = _expectedUpdatedName },
            ApiJsonOptions);
    }

    [When("the client updates the translation for administrative area \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientUpdatesMissingTranslation(string administrativeAreaId, string languageId)
    {
        _updateAdministrativeAreaId = administrativeAreaId;
        _updateLanguageId = languageId;
        _response = await _fixture.Client.PutAsJsonAsync(
            $"{NamesUrl(administrativeAreaId)}/{languageId}",
            new { Name = "Updated name" },
            ApiJsonOptions);
    }

    [Then("the updated administrative area name is returned")]
    public async Task ThenUpdatedAdministrativeAreaNameIsReturned()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_expectedUpdatedName, json.RootElement.GetProperty("Name").GetString());
    }

    [Then("the administrative area and language remain unchanged")]
    public async Task ThenAdministrativeAreaAndLanguageRemainUnchanged()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_updateLanguageId, json.RootElement.GetProperty("LanguageId").GetString());

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var updated = await context.AdministrativeAreaNames.SingleAsync(name =>
            name.AdministrativeAreaId == _updateAdministrativeAreaId && name.LanguageId == _updateLanguageId);
        Assert.Equal(_updateAdministrativeAreaId, updated.AdministrativeAreaId);
        Assert.Equal(_updateLanguageId, updated.LanguageId);
    }
}
