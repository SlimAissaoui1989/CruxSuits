using System.Net;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.AdministrativeAreaNames.StepDefinitions;

public sealed partial class AdministrativeAreaNamesSteps
{
    private string _deletionAdministrativeAreaId = null!;
    private string _deletionLanguageId = null!;

    [Given("administrative area \"(.*)\" has a translation in language \"(.*)\"")]
    public async Task GivenAdministrativeAreaHasTranslationInLanguage(string administrativeAreaId, string languageId)
    {
        _deletionAdministrativeAreaId = administrativeAreaId;
        _deletionLanguageId = languageId;
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.AdministrativeAreaNames.AnyAsync(name =>
            name.AdministrativeAreaId == administrativeAreaId && name.LanguageId == languageId));
    }

    [Given("administrative area \"(.*)\" has no translation in language \"(.*)\"")]
    public async Task GivenAdministrativeAreaHasNoTranslationInLanguage(string administrativeAreaId, string languageId)
    {
        _deletionAdministrativeAreaId = administrativeAreaId;
        _deletionLanguageId = languageId;
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var translations = await context.AdministrativeAreaNames
            .Where(name => name.AdministrativeAreaId == administrativeAreaId && name.LanguageId == languageId)
            .ToListAsync();

        context.AdministrativeAreaNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }

    [When("the client deletes the translation for administrative area \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientDeletesTranslation(string administrativeAreaId, string languageId)
    {
        _deletionAdministrativeAreaId = administrativeAreaId;
        _deletionLanguageId = languageId;
        _response = await _fixture.Client.DeleteAsync($"{NamesUrl(administrativeAreaId)}/{languageId}");
    }

    [Then("the translation no longer exists")]
    public async Task ThenTranslationNoLongerExists()
    {
        using var response = await _fixture.Client.GetAsync(
            $"{NamesUrl(_deletionAdministrativeAreaId)}/{_deletionLanguageId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
