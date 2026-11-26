using System.Net.Http.Json;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Localities.StepDefinitions;

public sealed partial class LocalitySteps
{
    [Then("the locality suggestions are ranked in this order: \"([^\"]*)\"")]
    public async Task ThenSuggestionsAreRankedInOrder(string ids)
    {
        var suggestions = await _response!.Content.ReadFromJsonAsync<List<LocalitySuggestionResponse>>();
        Assert.NotNull(suggestions);
        var expectedIds = ids.Split(',', StringSplitOptions.TrimEntries);
        Assert.Equal(expectedIds, suggestions.Select(suggestion => suggestion.LocalityId));
    }

    [Then("the first locality suggestion is \"([^\"]*)\" with label \"([^\"]*)\"")]
    public async Task ThenFirstLocalitySuggestionMatches(string id, string label)
    {
        var suggestions = await _response!.Content.ReadFromJsonAsync<List<LocalitySuggestionResponse>>();
        Assert.NotNull(suggestions);
        Assert.NotEmpty(suggestions);
        Assert.Equal((id, label), (suggestions[0].LocalityId, suggestions[0].DisplayLabel));
    }

    [When("the client searches localities for \"([^\"]*)\" in language \"([^\"]*)\"")]
    public async Task WhenClientSearchesLocalities(string query, string languageId)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get,
            $"/api/v1/Localities/Autocomplete?q={Uri.EscapeDataString(query)}");
        request.Headers.AcceptLanguage.ParseAdd(languageId);
        _response = await _fixture.Client.SendAsync(request);
    }

    [Then("the locality suggestions contain \"([^\"]*)\" named \"([^\"]*)\" in area \"([^\"]*)\" and country \"([^\"]*)\"")]
    public async Task ThenSuggestionsContainLocality(string id, string name, string areaId, string countryId)
    {
        var suggestions = await _response!.Content.ReadFromJsonAsync<List<LocalitySuggestionResponse>>();
        Assert.NotNull(suggestions);
        var suggestion = Assert.Single(suggestions, item => item.Id == id);
        Assert.Equal((name, areaId, countryId), (suggestion.Name, suggestion.AdministrativeAreaId, suggestion.CountryId));
    }
}
