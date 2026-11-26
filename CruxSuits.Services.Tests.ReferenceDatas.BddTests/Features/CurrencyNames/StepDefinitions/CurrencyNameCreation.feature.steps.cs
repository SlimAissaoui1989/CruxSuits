using System.Net.Http.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CurrencyNames.StepDefinitions;

public sealed partial class CurrencyNamesSteps
{
    [Given("currency \"(.*)\" exists")]
    public async Task GivenCurrencyExists(string currencyId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.Currencies.AnyAsync(currency => currency.Id == currencyId));
    }

    [Given("no translation exists for currency \"(.*)\" in language \"(.*)\"")]
    public async Task GivenNoTranslationExists(string currencyId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        await context.CurrencyNames.IgnoreQueryFilters()
            .Where(name => name.CurrencyId == currencyId && name.LanguageId == languageId)
            .ExecuteDeleteAsync();
    }

    [Given("currency \"(.*)\" already has a translation in language \"(.*)\"")]
    public async Task GivenCurrencyAlreadyHasTranslation(string currencyId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.CurrencyNames.AnyAsync(name =>
            name.CurrencyId == currencyId && name.LanguageId == languageId));
    }

    [When("the client creates a currency name \"(.*)\" in language \"(.*)\"")]
    public async Task WhenClientCreatesCurrencyName(string name, string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Currencies/USD/names",
            new { LanguageId = languageId, Name = name },
            ApiJsonOptions);
    }

    [When("the client creates another translation for currency \"(.*)\" in language \"(.*)\"")]
    public async Task WhenClientCreatesDuplicateTranslation(string currencyId, string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            $"/api/v1/Currencies/{currencyId}/names",
            new { LanguageId = languageId, Name = "Duplicate" },
            ApiJsonOptions);
    }

    [When("the client creates a translation for currency \"(.*)\"")]
    public async Task WhenClientCreatesTranslationForCurrency(string currencyId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            $"/api/v1/Currencies/{currencyId}/names",
            new { LanguageId = "en", Name = "Test name" },
            ApiJsonOptions);
    }

    [When("the client creates a translation for language \"(.*)\"")]
    public async Task WhenClientCreatesTranslationForLanguage(string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Currencies/USD/names",
            new { LanguageId = languageId, Name = "Test name" },
            ApiJsonOptions);
    }

    [When("the client creates a currency name with a (.*) name")]
    public async Task WhenClientCreatesCurrencyNameWithInvalidName(string condition)
    {
        var invalidValue = condition == "blank" ? string.Empty : "   ";
        var request = new
        {
            LanguageId = "en",
            Name = invalidValue
        };
        _response = await _fixture.Client.PostAsJsonAsync("/api/v1/Currencies/USD/names", request, ApiJsonOptions);
    }

    private CurrencyName? _storedName;

    [Then("the currency name is stored for currency \"(.*)\" and language \"(.*)\"")]
    public async Task ThenCurrencyNameIsStored(string currencyId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        _storedName = await context.CurrencyNames.SingleOrDefaultAsync(name =>
            name.CurrencyId == currencyId && name.LanguageId == languageId);
        Assert.NotNull(_storedName);
    }

    [Then("the stored name is \"(.*)\"")]
    public void ThenStoredNameIs(string expectedName)
    {
        Assert.Equal(expectedName, _storedName!.Name);
    }
}
