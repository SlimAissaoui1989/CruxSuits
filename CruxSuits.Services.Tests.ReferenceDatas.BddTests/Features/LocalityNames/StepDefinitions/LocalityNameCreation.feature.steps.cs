using System.Net.Http.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.LocalityNames.StepDefinitions;

public sealed partial class LocalityNamesSteps
{
    [Given("locality \"(.*)\" exists")]
    public async Task GivenLocalityExists(string localityId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.Localities.AnyAsync(area => area.Id == localityId));
    }

    [Given("no translation exists for locality \"(.*)\" in language \"(.*)\"")]
    public async Task GivenNoTranslationExists(string localityId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        await context.LocalityNames.IgnoreQueryFilters()
            .Where(name => name.LocalityId == localityId && name.LanguageId == languageId)
            .ExecuteDeleteAsync();
    }

    [Given("locality \"(.*)\" already has a translation in language \"(.*)\"")]
    public async Task GivenLocalityAlreadyHasTranslation(string localityId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.True(await context.LocalityNames.AnyAsync(name =>
            name.LocalityId == localityId && name.LanguageId == languageId));
    }

    [When("the client creates a locality name \"(.*)\" in language \"(.*)\"")]
    public async Task WhenClientCreatesLocalityName(string name, string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            NamesUrl("TN-TU-BRD"),
            new { LanguageId = languageId, Name = name },
            ApiJsonOptions);
    }

    [When("the client creates another translation for locality \"(.*)\" in language \"(.*)\"")]
    public async Task WhenClientCreatesDuplicateTranslation(string localityId, string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            NamesUrl(localityId),
            new { LanguageId = languageId, Name = "Duplicate" },
            ApiJsonOptions);
    }

    [When("the client creates a translation for locality \"(.*)\"")]
    public async Task WhenClientCreatesTranslationForLocality(string localityId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            NamesUrl(localityId),
            new { LanguageId = "en", Name = "Test name" },
            ApiJsonOptions);
    }

    [When("the client creates a translation for language \"(.*)\"")]
    public async Task WhenClientCreatesTranslationForLanguage(string languageId)
    {
        _response = await _fixture.Client.PostAsJsonAsync(
            NamesUrl("TN-TU-BRD"),
            new { LanguageId = languageId, Name = "Test name" },
            ApiJsonOptions);
    }

    [When("the client creates a locality name with a (.*) name")]
    public async Task WhenClientCreatesLocalityNameWithInvalidName(string condition)
    {
        var invalidValue = condition == "blank" ? string.Empty : "   ";
        var request = new
        {
            LanguageId = "en",
            Name = invalidValue
        };
        _response = await _fixture.Client.PostAsJsonAsync(NamesUrl("TN-TU-BRD"), request, ApiJsonOptions);
    }

    private LocalityName? _storedName;

    [Then("the locality name is stored for locality \"(.*)\" and language \"(.*)\"")]
    public async Task ThenLocalityNameIsStored(string localityId, string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        _storedName = await context.LocalityNames.SingleOrDefaultAsync(name =>
            name.LocalityId == localityId && name.LanguageId == languageId);
        Assert.NotNull(_storedName);
    }

    [Then("the stored name is \"(.*)\"")]
    public void ThenStoredNameIs(string expectedName)
    {
        Assert.Equal(expectedName, _storedName!.Name);
    }
}
