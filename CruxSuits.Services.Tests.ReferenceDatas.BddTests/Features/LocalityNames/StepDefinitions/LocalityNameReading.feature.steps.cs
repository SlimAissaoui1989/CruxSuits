using System.Net;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.LocalityNames.StepDefinitions;

public sealed partial class LocalityNamesSteps
{
    [Given("locality \"(.*)\" does not exist")]
    public async Task GivenLocalityDoesNotExist(string localityId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();

        Assert.False(await context.Localities.AnyAsync(area => area.Id == localityId));
    }

    [Given("locality \"(.*)\" exists without translations")]
    public async Task GivenLocalityExistsWithoutTranslations(string localityId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var area = await context.Localities.SingleOrDefaultAsync(area => area.Id == localityId);

        if (area is null)
        {
            var lastSeparator = localityId.LastIndexOf('-');
            context.Localities.Add(new Locality
            {
                Id = localityId,
                AdministrativeAreaId = localityId[..lastSeparator],
                Code = localityId[(lastSeparator + 1)..]
            });
        }

        var translations = await context.LocalityNames
            .Where(name => name.LocalityId == localityId)
            .ToListAsync();
        context.LocalityNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }

    [Given("locality \"(.*)\" has the following translations:")]
    public async Task GivenLocalityHasFollowingTranslations(string localityId, Table translations)
    {
        _expectedNames.Clear();
        foreach (var row in translations.Rows)
            _expectedNames.Add(new ExpectedLocalityName(row["Language"], row["Name"]));

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var languageIds = _expectedNames.Select(name => name.LanguageId).ToArray();
        var existingNames = await context.LocalityNames
            .Where(name => name.LocalityId == localityId && languageIds.Contains(name.LanguageId))
            .ToListAsync();

        foreach (var expectedName in _expectedNames)
        {
            var existingName = existingNames.SingleOrDefault(name => name.LanguageId == expectedName.LanguageId);
            Assert.NotNull(existingName);
            existingName.Name = expectedName.Name;
        }

        await context.SaveChangesAsync();
    }

    [When("the client requests all names for locality \"(.*)\"")]
    public async Task WhenClientRequestsAllNamesForLocality(string localityId)
    {
        _response = await _fixture.Client.GetAsync(NamesUrl(localityId));
    }

    [When("the client requests the name for locality \"(.*)\" and language \"(.*)\"")]
    public async Task WhenClientRequestsLocalityName(string localityId, string languageId)
    {
        _response = await _fixture.Client.GetAsync($"{NamesUrl(localityId)}/{languageId}");
    }

    [Then("the response status is (.*)")]
    public async Task ThenResponseStatusIs(int expectedStatusCode)
    {
        var responseBody = await _response!.Content.ReadAsStringAsync();
        Assert.True((int)_response.StatusCode == expectedStatusCode,
            $"Expected {(HttpStatusCode)expectedStatusCode} but received {(int)_response.StatusCode}: {responseBody}");
    }

    [Then("the response contains (.*) locality names")]
    public async Task ThenResponseContainsLocalityNames(int expectedCount)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedCount, json.RootElement.GetArrayLength());
    }

    [Then("each locality name contains its language and name")]
    public async Task ThenEachLocalityNameContainsLanguageAndName()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var returnedNames = json.RootElement.EnumerateArray()
            .Select(name => new ExpectedLocalityName(
                GetString(name, "LanguageId"),
                GetString(name, "Name")))
            .ToArray();

        foreach (var expectedName in _expectedNames)
            Assert.Contains(expectedName, returnedNames);
    }

    [Then("the response contains the expected name \"(.*)\"")]
    public async Task ThenResponseContainsExpectedLocalityName(string expectedName)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(expectedName, GetString(json.RootElement, "Name"));
    }
}
