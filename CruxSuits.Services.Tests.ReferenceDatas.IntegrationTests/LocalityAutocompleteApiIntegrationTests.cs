using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class LocalityAutocompleteApiIntegrationTests
{
    private const string Url = "/api/v1/Localities/Autocomplete";
    private readonly ReferenceDatasIntegrationFixture _fixture;

    public LocalityAutocompleteApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Autocomplete_RanksLocalityMatchFirstAndReturnsDisplayLabel()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"{Url}?q=sfax%20tun");
        request.Headers.AcceptLanguage.ParseAdd("en");
        using var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("TN-SF-SFX", json.RootElement[0].GetProperty("LocalityId").GetString());
        Assert.Equal("Sfax, Sfax Governorate, Tunisia", json.RootElement[0].GetProperty("DisplayLabel").GetString());
    }

    [Fact]
    public async Task Autocomplete_MatchesPartialNamesAndReturnsLocalizedContext()
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var catalog = scope.ServiceProvider.GetRequiredService<ILanguageCatalogProvider>();
        await catalog.RefreshAsync();

        using var request = new HttpRequestMessage(HttpMethod.Get, $"{Url}?q=bar%2C%20tunis%20tunisia");
        request.Headers.AcceptLanguage.ParseAdd("ar");
        using var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var suggestions = await response.Content.ReadFromJsonAsync<List<LocalitySuggestionResponse>>();
        Assert.NotNull(suggestions);
        var bardo = Assert.Single(suggestions, item => item.Id == "TN-TU-BRD");
        Assert.Equal(("BRD", "باردو"), (bardo.Code, bardo.Name));
        Assert.Equal(("TN-TU", "TU", "ولاية تونس"), (bardo.AdministrativeAreaId, bardo.AdministrativeAreaCode, bardo.AdministrativeAreaName));
        Assert.Equal(("TN", "تونس"), (bardo.CountryId, bardo.CountryName));
    }

    [Theory]
    [InlineData("tunisia", 10)]
    [InlineData("bardo%20sfax", 0)]
    public async Task Autocomplete_ReturnsExpectedSuggestionCount(string query, int expectedCount)
    {
        using var response = await _fixture.Client.GetAsync($"{Url}?q={query}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var suggestions = await response.Content.ReadFromJsonAsync<List<LocalitySuggestionResponse>>();
        Assert.NotNull(suggestions);
        Assert.Equal(expectedCount, suggestions.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData("?q=b")]
    public async Task Autocomplete_RejectsMissingOrShortQuery(string queryString)
    {
        using var response = await _fixture.Client.GetAsync(Url + queryString);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
