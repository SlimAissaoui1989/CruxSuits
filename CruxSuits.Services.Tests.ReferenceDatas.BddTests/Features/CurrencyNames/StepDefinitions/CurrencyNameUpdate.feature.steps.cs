using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CurrencyNames.StepDefinitions;

public sealed partial class CurrencyNamesSteps
{
    private string _updateCurrencyId = null!;
    private string _updateLanguageId = null!;
    private string _expectedUpdatedName = null!;

    [Given("currency \"(.*)\" has the following translation:")]
    public async Task GivenCurrencyHasTranslation(string currencyId, Table translations)
    {
        var row = translations.Rows.Single();
        _updateCurrencyId = currencyId;
        _updateLanguageId = row["Language"];
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var translation = await context.CurrencyNames.SingleOrDefaultAsync(name =>
            name.CurrencyId == currencyId && name.LanguageId == _updateLanguageId);

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
            $"/api/v1/Currencies/{_updateCurrencyId}/names/{_updateLanguageId}",
            new { Name = _expectedUpdatedName },
            ApiJsonOptions);
    }

    [When("the client updates the translation for currency \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientUpdatesMissingTranslation(string currencyId, string languageId)
    {
        _updateCurrencyId = currencyId;
        _updateLanguageId = languageId;
        _response = await _fixture.Client.PutAsJsonAsync(
            $"/api/v1/Currencies/{currencyId}/names/{languageId}",
            new { Name = "Updated name" },
            ApiJsonOptions);
    }

    [Then("the updated currency name is returned")]
    public async Task ThenUpdatedCurrencyNameIsReturned()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_expectedUpdatedName, json.RootElement.GetProperty("Name").GetString());
    }

    [Then("the currency and language remain unchanged")]
    public async Task ThenCurrencyAndLanguageRemainUnchanged()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_updateLanguageId, json.RootElement.GetProperty("LanguageId").GetString());

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var updated = await context.CurrencyNames.SingleAsync(name =>
            name.CurrencyId == _updateCurrencyId && name.LanguageId == _updateLanguageId);
        Assert.Equal(_updateCurrencyId, updated.CurrencyId);
        Assert.Equal(_updateLanguageId, updated.LanguageId);
    }
}
