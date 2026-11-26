using System.Net;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.LocalityNames.StepDefinitions;

public sealed partial class LocalityNamesSteps
{
    private string _deletionLocalityId = null!;
    private string _deletionLanguageId = null!;

    [Given("locality \"(.*)\" has a translation in language \"(.*)\"")]
    public async Task GivenLocalityHasTranslationInLanguage(string localityId, string languageId)
    {
        _deletionLocalityId = localityId;
        _deletionLanguageId = languageId;
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.LocalityNames.AnyAsync(name =>
            name.LocalityId == localityId && name.LanguageId == languageId));
    }

    [Given("locality \"(.*)\" has no translation in language \"(.*)\"")]
    public async Task GivenLocalityHasNoTranslationInLanguage(string localityId, string languageId)
    {
        _deletionLocalityId = localityId;
        _deletionLanguageId = languageId;
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var translations = await context.LocalityNames
            .Where(name => name.LocalityId == localityId && name.LanguageId == languageId)
            .ToListAsync();

        context.LocalityNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }

    [When("the client deletes the translation for locality \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientDeletesTranslation(string localityId, string languageId)
    {
        _deletionLocalityId = localityId;
        _deletionLanguageId = languageId;
        _response = await _fixture.Client.DeleteAsync($"{NamesUrl(localityId)}/{languageId}");
    }

    [Then("the translation no longer exists")]
    public async Task ThenTranslationNoLongerExists()
    {
        using var response = await _fixture.Client.GetAsync(
            $"{NamesUrl(_deletionLocalityId)}/{_deletionLanguageId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
