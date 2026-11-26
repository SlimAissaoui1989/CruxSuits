using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Currencies.StepDefinitions;

public sealed partial class CurrenciesControllerSteps
{
    [Given("the test currency \"(.*)\" has only an English name \"(.*)\"")]
    public async Task GivenTestCurrencyHasOnlyAnEnglishName(string currencyId, string name)
    {
        await EnsureCurrencyAsync(currencyId, "998");

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        if (!await context.CurrencyNames.AnyAsync(currencyName =>
                currencyName.CurrencyId == currencyId && currencyName.LanguageId == "en"))
        {
            context.CurrencyNames.Add(new CurrencyName
            {
                CurrencyId = currencyId,
                LanguageId = "en",
                Name = name
            });
            await context.SaveChangesAsync();
        }
    }

    [When("the client lists currencies in language \"(.*)\"")]
    public Task WhenClientListsCurrenciesInLanguage(string languageId) =>
        SendAsync(HttpMethod.Get, "/api/v1/Currencies", acceptLanguage: languageId);

    [When("the client requests currency \"(.*)\"")]
    public Task WhenClientRequestsCurrency(string currencyId) =>
        SendAsync(HttpMethod.Get, $"/api/v1/Currencies/{currencyId}");

    [Then("the currency list contains currency \"([^\"]*)\" named \"([^\"]*)\" with numeric code \"([^\"]*)\"")]
    public async Task ThenCurrencyListContainsLocalizedCurrency(string currencyId, string name, string numericCode)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var currency = json.RootElement.EnumerateArray()
            .Single(item => GetString(item, "Id") == currencyId);
        Assert.Equal(name, GetString(currency, "Name"));
        Assert.Equal(numericCode, GetString(currency, "NumericCode"));
    }

    [Then("the currency details contain id \"(.*)\" and numeric code \"(.*)\"")]
    public async Task ThenCurrencyDetailsContainIdAndNumericCode(string currencyId, string numericCode)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(currencyId, GetString(json.RootElement, "Id"));
        Assert.Equal(numericCode, GetString(json.RootElement, "NumericCode"));
    }
}
