using System.Net;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CountryNames.StepDefinitions;

public sealed partial class CountryNamesSteps
{
    private string _deletionCountryId = null!;
    private string _deletionLanguageId = null!;

    [Given("country \"(.*)\" has a translation in language \"(.*)\"")]
    public async Task GivenCountryHasTranslationInLanguage(string countryId, string languageId)
    {
        _deletionCountryId = countryId;
        _deletionLanguageId = languageId;
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.CountryNames.AnyAsync(name =>
            name.CountryId == countryId && name.LanguageId == languageId));
    }

    [Given("country \"(.*)\" has no translation in language \"(.*)\"")]
    public async Task GivenCountryHasNoTranslationInLanguage(string countryId, string languageId)
    {
        _deletionCountryId = countryId;
        _deletionLanguageId = languageId;
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var translations = await context.CountryNames
            .Where(name => name.CountryId == countryId && name.LanguageId == languageId)
            .ToListAsync();

        context.CountryNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }

    [When("the client deletes the translation for country \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientDeletesTranslation(string countryId, string languageId)
    {
        _deletionCountryId = countryId;
        _deletionLanguageId = languageId;
        _response = await _fixture.Client.DeleteAsync($"/api/v1/Countries/{countryId}/names/{languageId}");
    }

    [Then("the translation no longer exists")]
    public async Task ThenTranslationNoLongerExists()
    {
        using var response = await _fixture.Client.GetAsync(
            $"/api/v1/Countries/{_deletionCountryId}/names/{_deletionLanguageId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
