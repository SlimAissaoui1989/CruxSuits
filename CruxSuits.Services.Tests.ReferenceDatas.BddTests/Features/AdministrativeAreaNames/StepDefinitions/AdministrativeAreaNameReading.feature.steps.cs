using System.Net;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.AdministrativeAreaNames.StepDefinitions;

public sealed partial class AdministrativeAreaNamesSteps
{
    [Given("administrative area \"(.*)\" does not exist")]
    public async Task GivenAdministrativeAreaDoesNotExist(string administrativeAreaId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.False(await context.AdministrativeAreas.AnyAsync(area => area.Id == administrativeAreaId));
    }

    [Given("administrative area \"(.*)\" exists without translations")]
    public async Task GivenAdministrativeAreaExistsWithoutTranslations(string administrativeAreaId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var area = await context.AdministrativeAreas.SingleOrDefaultAsync(area => area.Id == administrativeAreaId);

        if (area is null)
        {
            context.AdministrativeAreas.Add(new AdministrativeArea
            {
                Id = administrativeAreaId,
                CountryId = administrativeAreaId[..2],
                Code = administrativeAreaId[3..]
            });
        }

        var translations = await context.AdministrativeAreaNames
            .Where(name => name.AdministrativeAreaId == administrativeAreaId)
            .ToListAsync();
        context.AdministrativeAreaNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }

    [Given("administrative area \"(.*)\" has the following translations:")]
    public async Task GivenAdministrativeAreaHasFollowingTranslations(string administrativeAreaId, Table translations)
    {
        _expectedNames.Clear();
        foreach (var row in translations.Rows)
            _expectedNames.Add(new ExpectedAdministrativeAreaName(row["Language"], row["Name"]));

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var languageIds = _expectedNames.Select(name => name.LanguageId).ToArray();
        var existingNames = await context.AdministrativeAreaNames
            .Where(name => name.AdministrativeAreaId == administrativeAreaId && languageIds.Contains(name.LanguageId))
            .ToListAsync();

        foreach (var expectedName in _expectedNames)
        {
            var existingName = existingNames.SingleOrDefault(name => name.LanguageId == expectedName.LanguageId);
            Assert.NotNull(existingName);
            existingName.Name = expectedName.Name;
        }

        await context.SaveChangesAsync();
    }

    [When("the client requests all names for administrative area \"(.*)\"")]
    public async Task WhenClientRequestsAllNamesForAdministrativeArea(string administrativeAreaId)
    {
        _response = await _fixture.Client.GetAsync(NamesUrl(administrativeAreaId));
    }

    [When("the client requests the name for administrative area \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientRequestsAdministrativeAreaName(string administrativeAreaId, string languageId)
    {
        _response = await _fixture.Client.GetAsync($"{NamesUrl(administrativeAreaId)}/{languageId}");
    }

    [Then("the response status is (.*)")]
    public async Task ThenResponseStatusIs(int expectedStatusCode)
    {
        var responseBody = await _response!.Content.ReadAsStringAsync();
        Assert.True((int)_response.StatusCode == expectedStatusCode,
            $"Expected {(HttpStatusCode)expectedStatusCode} but received {(int)_response.StatusCode}: {responseBody}");
    }

    [Then("the response contains (.*) administrative area names")]
    public async Task ThenResponseContainsAdministrativeAreaNames(int expectedCount)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedCount, json.RootElement.GetArrayLength());
    }

    [Then("each administrative area name contains its language and name")]
    public async Task ThenEachAdministrativeAreaNameContainsLanguageAndName()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var returnedNames = json.RootElement.EnumerateArray()
            .Select(name => new ExpectedAdministrativeAreaName(
                GetString(name, "LanguageId"),
                GetString(name, "Name")))
            .ToArray();

        foreach (var expectedName in _expectedNames)
            Assert.Contains(expectedName, returnedNames);
    }

    [Then("the response contains the expected name \"(.*)\"")]
    public async Task ThenResponseContainsExpectedAdministrativeAreaName(string expectedName)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedName, GetString(json.RootElement, "Name"));
    }
}
