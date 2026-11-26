using System.Net;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CurrencyNames.StepDefinitions;

public sealed partial class CurrencyNamesSteps
{
    private string _deletionCurrencyId = null!;
    private string _deletionLanguageId = null!;

    [Given("currency \"(.*)\" has a translation in language \"(.*)\"")]
    public async Task GivenCurrencyHasTranslationInLanguage(string currencyId, string languageId)
    {
        _deletionCurrencyId = currencyId;
        _deletionLanguageId = languageId;
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.CurrencyNames.AnyAsync(name =>
            name.CurrencyId == currencyId && name.LanguageId == languageId));
    }

    [Given("currency \"(.*)\" has no translation in language \"(.*)\"")]
    public async Task GivenCurrencyHasNoTranslationInLanguage(string currencyId, string languageId)
    {
        _deletionCurrencyId = currencyId;
        _deletionLanguageId = languageId;
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var translations = await context.CurrencyNames
            .Where(name => name.CurrencyId == currencyId && name.LanguageId == languageId)
            .ToListAsync();

        context.CurrencyNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }

    [When("the client deletes the translation for currency \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientDeletesTranslation(string currencyId, string languageId)
    {
        _deletionCurrencyId = currencyId;
        _deletionLanguageId = languageId;
        _response = await _fixture.Client.DeleteAsync($"/api/v1/Currencies/{currencyId}/names/{languageId}");
    }

    [Then("the translation no longer exists")]
    public async Task ThenTranslationNoLongerExists()
    {
        using var response = await _fixture.Client.GetAsync(
            $"/api/v1/Currencies/{_deletionCurrencyId}/names/{_deletionLanguageId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
