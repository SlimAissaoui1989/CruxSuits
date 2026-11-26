using System.Net;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CurrencyNames.StepDefinitions;

public sealed partial class CurrencyNamesSteps
{
    [Given("currency \"(.*)\" does not exist")]
    public async Task GivenCurrencyDoesNotExist(string currencyId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.False(await context.Currencies.AnyAsync(currency => currency.Id == currencyId));
    }

    [Given("currency \"(.*)\" exists without translations")]
    public async Task GivenCurrencyExistsWithoutTranslations(string currencyId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var currency = await context.Currencies.SingleOrDefaultAsync(currency => currency.Id == currencyId);

        if (currency is null)
        {
            context.Currencies.Add(new Currency
            {
                Id = currencyId,
                NumericCode = "993"
            });
        }

        var translations = await context.CurrencyNames
            .Where(name => name.CurrencyId == currencyId)
            .ToListAsync();
        context.CurrencyNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }

    [Given("currency \"(.*)\" has the following translations:")]
    public async Task GivenCurrencyHasFollowingTranslations(string currencyId, Table translations)
    {
        _expectedNames.Clear();
        foreach (var row in translations.Rows)
            _expectedNames.Add(new ExpectedCurrencyName(row["Language"], row["Name"]));

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var languageIds = _expectedNames.Select(name => name.LanguageId).ToArray();
        var existingNames = await context.CurrencyNames
            .Where(name => name.CurrencyId == currencyId && languageIds.Contains(name.LanguageId))
            .ToListAsync();

        foreach (var expectedName in _expectedNames)
        {
            var existingName = existingNames.SingleOrDefault(name => name.LanguageId == expectedName.LanguageId);
            Assert.NotNull(existingName);
            existingName.Name = expectedName.Name;
        }

        await context.SaveChangesAsync();
    }

    [When("the client requests all names for currency \"(.*)\"")]
    public async Task WhenClientRequestsAllNamesForCurrency(string currencyId)
    {
        _response = await _fixture.Client.GetAsync($"/api/v1/Currencies/{currencyId}/names");
    }

    [When("the client requests the name for currency \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientRequestsCurrencyName(string currencyId, string languageId)
    {
        _response = await _fixture.Client.GetAsync($"/api/v1/Currencies/{currencyId}/names/{languageId}");
    }

    [Then("the response status is (.*)")]
    public async Task ThenResponseStatusIs(int expectedStatusCode)
    {
        var responseBody = await _response!.Content.ReadAsStringAsync();
        Assert.True((int)_response.StatusCode == expectedStatusCode,
            $"Expected {(HttpStatusCode)expectedStatusCode} but received {(int)_response.StatusCode}: {responseBody}");
    }

    [Then("the response contains (.*) currency names")]
    public async Task ThenResponseContainsCurrencyNames(int expectedCount)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedCount, json.RootElement.GetArrayLength());
    }

    [Then("each currency name contains its language and name")]
    public async Task ThenEachCurrencyNameContainsLanguageAndName()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var returnedNames = json.RootElement.EnumerateArray()
            .Select(name => new ExpectedCurrencyName(
                GetString(name, "LanguageId"),
                GetString(name, "Name")))
            .ToArray();

        foreach (var expectedName in _expectedNames)
            Assert.Contains(expectedName, returnedNames);
    }

    [Then("the response contains the expected name \"(.*)\"")]
    public async Task ThenResponseContainsExpectedCurrencyName(string expectedName)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedName, GetString(json.RootElement, "Name"));
    }
}
