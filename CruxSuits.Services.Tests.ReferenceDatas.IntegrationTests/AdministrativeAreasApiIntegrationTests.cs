using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class AdministrativeAreasApiIntegrationTests
{
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };
    private readonly ReferenceDatasIntegrationFixture _fixture;

    public AdministrativeAreasApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    private const string AreasUrl = "/api/v1/Countries/TN/AdministrativeAreas";

    [Fact]
    public async Task GetAll_ReturnsSeededAreasForTunisia()
    {
        using var response = await _fixture.Client.GetAsync(AreasUrl);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var areas = await response.Content.ReadFromJsonAsync<List<AdministrativeAreaDetailsResponse>>();
        Assert.NotNull(areas);
        Assert.All(areas, area => Assert.Equal("TN", area.CountryId));
        Assert.Contains(areas, area => area.Id == "TN-SF" && area.Code == "SF");
    }

    [Theory]
    [InlineData("QA")]
    [InlineData("QAA")]
    public async Task Create_ReturnsLocationUsingShortCodeAndPersistsCombinedId(string code)
    {
        var request = new AdministrativeAreaRequest { Code = code };
        using var response = await _fixture.Client.PostAsJsonAsync(AreasUrl, request, ApiJsonOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        Assert.EndsWith($"/AdministrativeAreas/{code}", response.Headers.Location.ToString());

        using var detailsResponse = await _fixture.Client.GetAsync(response.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, detailsResponse.StatusCode);
        var area = await detailsResponse.Content.ReadFromJsonAsync<AdministrativeAreaDetailsResponse>();
        Assert.NotNull(area);
        Assert.Equal(($"TN-{code}", "TN", code), (area.Id, area.CountryId, area.Code));
    }

    [Theory]
    [InlineData("TN", "SF", HttpStatusCode.Conflict)]
    [InlineData("TN", "Q1", HttpStatusCode.BadRequest)]
    [InlineData("ZZ", "QA", HttpStatusCode.NotFound)]
    public async Task Create_RejectsInvalidRequests(string countryId, string code, HttpStatusCode expected)
    {
        var request = new AdministrativeAreaRequest { Code = code };
        using var response = await _fixture.Client.PostAsJsonAsync(
            $"/api/v1/Countries/{countryId}/AdministrativeAreas", request, ApiJsonOptions);

        Assert.Equal(expected, response.StatusCode);
    }

    [Fact]
    public async Task Put_CannotChangeAreaCode()
    {
        using var response = await _fixture.Client.PutAsJsonAsync(
            $"{AreasUrl}/SF", new AdministrativeAreaRequest { Code = "ZZ" }, ApiJsonOptions);

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task Delete_HidesAreaAndItsLocalizedNames()
    {
        using var created = await _fixture.Client.PostAsJsonAsync(
            AreasUrl, new AdministrativeAreaRequest { Code = "QB" }, ApiJsonOptions);
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);

        using var createdName = await _fixture.Client.PostAsJsonAsync(
            $"{AreasUrl}/names/QB", new { LanguageId = "en", Name = "Test Area" }, ApiJsonOptions);
        Assert.Equal(HttpStatusCode.Created, createdName.StatusCode);

        using var response = await _fixture.Client.DeleteAsync($"{AreasUrl}/QB");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var details = await _fixture.Client.GetAsync($"{AreasUrl}/QB");
        using var names = await _fixture.Client.GetAsync($"{AreasUrl}/names/QB");
        Assert.Equal(HttpStatusCode.NotFound, details.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, names.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundForMissingArea()
    {
        using var response = await _fixture.Client.DeleteAsync($"{AreasUrl}/ZZ");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
