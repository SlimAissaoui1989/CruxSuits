using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class LocalityNamesApiIntegrationTests
{
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };
    private readonly ReferenceDatasIntegrationFixture _fixture;

    public LocalityNamesApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    private const string NamesUrl = "/api/v1/Countries/TN/AdministrativeAreas/TU/Localities/names";

    [Fact]
    public async Task GetAll_ReturnsThreeSeededNamesForBardo()
    {
        using var response = await _fixture.Client.GetAsync($"{NamesUrl}/BRD");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var names = await response.Content.ReadFromJsonAsync<List<LocalityNameResponse>>();
        Assert.NotNull(names);
        Assert.Equal(new[] { "ar", "en", "fr" }, names.Select(name => name.LanguageId).Order().ToArray());
    }

    [Theory]
    [InlineData("en", "Le Bardo")]
    [InlineData("ar", "باردو")]
    [InlineData("fr", "Le Bardo")]
    public async Task GetByLanguage_ReturnsSeededName(string language, string expected)
    {
        using var response = await _fixture.Client.GetAsync($"{NamesUrl}/BRD/{language}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var name = await response.Content.ReadFromJsonAsync<LocalityNameResponse>();
        Assert.NotNull(name);
        Assert.Equal((language, expected), (name.LanguageId, name.Name));
    }

    [Fact]
    public async Task CreateUpdateAndDelete_UseTheSameAreaAndLanguageRoute()
    {
        using var areaResponse = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Countries/TN/AdministrativeAreas/TU/Localities", new { Code = "ZQC" }, ApiJsonOptions);
        Assert.Equal(HttpStatusCode.Created, areaResponse.StatusCode);

        using var createResponse = await _fixture.Client.PostAsJsonAsync(
            $"{NamesUrl}/ZQC",
            new LocalityNameRequest { LanguageId = "fr", Name = "Zone de test" }, ApiJsonOptions);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        Assert.NotNull(createResponse.Headers.Location);
        Assert.EndsWith("/Localities/names/ZQC/fr", createResponse.Headers.Location.ToString());

        using var updateResponse = await _fixture.Client.PutAsJsonAsync(
            createResponse.Headers.Location,
            new LocalityNameUpdateRequest { Name = "Nouvelle zone" }, ApiJsonOptions);
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        using var detailResponse = await _fixture.Client.GetAsync(createResponse.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, detailResponse.StatusCode);
        var updated = await detailResponse.Content.ReadFromJsonAsync<LocalityNameResponse>();
        Assert.NotNull(updated);
        Assert.Equal(("fr", "Nouvelle zone"), (updated.LanguageId, updated.Name));

        using var deleteResponse = await _fixture.Client.DeleteAsync(createResponse.Headers.Location);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        using var missingResponse = await _fixture.Client.GetAsync(createResponse.Headers.Location);
        Assert.Equal(HttpStatusCode.NotFound, missingResponse.StatusCode);
    }

    [Theory]
    [InlineData("BRD", "en", "Test Name", HttpStatusCode.Conflict)]
    [InlineData("BRD", "zz", "Test Name", HttpStatusCode.BadRequest)]
    [InlineData("ZZZ", "en", "Test Name", HttpStatusCode.NotFound)]
    [InlineData("BRD", "en", "Le Bardo2", HttpStatusCode.BadRequest)]
    public async Task Create_RejectsInvalidRequests(string code, string language, string name, HttpStatusCode expected)
    {
        var request = new LocalityNameRequest { LanguageId = language, Name = name };
        using var response = await _fixture.Client.PostAsJsonAsync($"{NamesUrl}/{code}", request, ApiJsonOptions);

        Assert.Equal(expected, response.StatusCode);
    }
}
