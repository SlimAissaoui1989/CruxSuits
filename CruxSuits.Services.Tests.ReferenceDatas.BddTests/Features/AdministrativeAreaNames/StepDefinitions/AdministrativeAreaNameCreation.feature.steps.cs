using System.Net.Http.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.AdministrativeAreaNames.StepDefinitions;

public sealed partial class AdministrativeAreaNamesSteps
{
    [Given("administrative area \"(.*)\" exists")]
    public async Task GivenAdministrativeAreaExists(string administrativeAreaId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.AdministrativeAreas.AnyAsync(area => area.Id == administrativeAreaId));
    }

    [Given("no translation exists for administrative area \"(.*)\" in language \"(.*)\"")]
    public async Task GivenNoTranslationExists(string administrativeAreaId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        await context.AdministrativeAreaNames.IgnoreQueryFilters()
            .Where(name => name.AdministrativeAreaId == administrativeAreaId && name.LanguageId == languageId)
            .ExecuteDeleteAsync();
    }

    [Given("administrative area \"(.*)\" already has a translation in language \"(.*)\"")]
    public async Task GivenAdministrativeAreaAlreadyHasTranslation(string administrativeAreaId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.AdministrativeAreaNames.AnyAsync(name =>
            name.AdministrativeAreaId == administrativeAreaId && name.LanguageId == languageId));
    }

    [When("the client creates an administrative area name \"(.*)\" in language \"(.*)\"")]
    public async Task WhenClientCreatesAdministrativeAreaName(string name, string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            NamesUrl("TN-SF"),
            new { LanguageId = languageId, Name = name },
            ApiJsonOptions);
    }

    [When("the client creates another translation for administrative area \"(.*)\" in language \"(.*)\"")]
    public async Task WhenClientCreatesDuplicateTranslation(string administrativeAreaId, string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            NamesUrl(administrativeAreaId),
            new { LanguageId = languageId, Name = "Duplicate" },
            ApiJsonOptions);
    }

    [When("the client creates a translation for administrative area \"(.*)\"")]
    public async Task WhenClientCreatesTranslationForAdministrativeArea(string administrativeAreaId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            NamesUrl(administrativeAreaId),
            new { LanguageId = "en", Name = "Test name" },
            ApiJsonOptions);
    }

    [When("the client creates a translation for language \"(.*)\"")]
    public async Task WhenClientCreatesTranslationForLanguage(string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            NamesUrl("TN-SF"),
            new { LanguageId = languageId, Name = "Test name" },
            ApiJsonOptions);
    }

    [When("the client creates an administrative area name with a (.*) name")]
    public async Task WhenClientCreatesAdministrativeAreaNameWithInvalidName(string condition)
    {
        var invalidValue = condition == "blank" ? string.Empty : "   ";
        var request = new
        {
            LanguageId = "en",
            Name = invalidValue
        };
        _response = await _fixture.Client.PostAsJsonAsync(NamesUrl("TN-SF"), request, ApiJsonOptions);
    }

    private AdministrativeAreaName? _storedName;

    [Then("the administrative area name is stored for administrative area \"(.*)\" and language \"(.*)\"")]
    public async Task ThenAdministrativeAreaNameIsStored(string administrativeAreaId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        _storedName = await context.AdministrativeAreaNames.SingleOrDefaultAsync(name =>
            name.AdministrativeAreaId == administrativeAreaId && name.LanguageId == languageId);
        Assert.NotNull(_storedName);
    }

    [Then("the stored name is \"(.*)\"")]
    public void ThenStoredNameIs(string expectedName)
    {
        Assert.Equal(expectedName, _storedName!.Name);
    }
}
