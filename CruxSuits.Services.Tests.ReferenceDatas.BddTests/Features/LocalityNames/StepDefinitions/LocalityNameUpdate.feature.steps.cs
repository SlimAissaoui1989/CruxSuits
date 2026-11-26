using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.LocalityNames.StepDefinitions;

public sealed partial class LocalityNamesSteps
{
    private string _updateLocalityId = null!;
    private string _updateLanguageId = null!;
    private string _expectedUpdatedName = null!;

    [Given("locality \"(.*)\" has the following translation:")]
    public async Task GivenLocalityHasTranslation(string localityId, Table translations)
    {
        var row = translations.Rows.Single();
        _updateLocalityId = localityId;
        _updateLanguageId = row["Language"];
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var translation = await context.LocalityNames.SingleOrDefaultAsync(name =>
            name.LocalityId == localityId && name.LanguageId == _updateLanguageId);

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
            $"{NamesUrl(_updateLocalityId)}/{_updateLanguageId}",
            new { Name = _expectedUpdatedName },
            ApiJsonOptions);
    }

    [When("the client updates the translation for locality \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientUpdatesMissingTranslation(string localityId, string languageId)
    {
        _updateLocalityId = localityId;
        _updateLanguageId = languageId;
        _response = await _fixture.Client.PutAsJsonAsync(
            $"{NamesUrl(localityId)}/{languageId}",
            new { Name = "Updated name" },
            ApiJsonOptions);
    }

    [Then("the updated locality name is returned")]
    public async Task ThenUpdatedLocalityNameIsReturned()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_expectedUpdatedName, json.RootElement.GetProperty("Name").GetString());
    }

    [Then("the locality and language remain unchanged")]
    public async Task ThenLocalityAndLanguageRemainUnchanged()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_updateLanguageId, json.RootElement.GetProperty("LanguageId").GetString());

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var updated = await context.LocalityNames.SingleAsync(name =>
            name.LocalityId == _updateLocalityId && name.LanguageId == _updateLanguageId);
        Assert.Equal(_updateLocalityId, updated.LocalityId);
        Assert.Equal(_updateLanguageId, updated.LanguageId);
    }
}
