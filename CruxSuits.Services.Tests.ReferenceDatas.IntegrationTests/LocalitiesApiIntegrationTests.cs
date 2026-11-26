using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class LocalitiesApiIntegrationTests
{
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };
    private readonly ReferenceDatasIntegrationFixture _fixture;

    public LocalitiesApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    private const string LocalitiesUrl = "/api/v1/Countries/TN/AdministrativeAreas/TU/Localities";

    [Fact]
    public async Task GetAll_ReturnsSeededLocalitiesForTunis()
    {
        using var response = await _fixture.Client.GetAsync(LocalitiesUrl);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var areas = await response.Content.ReadFromJsonAsync<List<LocalityDetailsResponse>>();
        Assert.NotNull(areas);
        Assert.All(areas, area => Assert.Equal("TN-TU", area.AdministrativeAreaId));
        Assert.Contains(areas, area => area.Id == "TN-TU-BRD" && area.Code == "BRD");
    }

    [Theory]
    [InlineData("ZQA")]
    [InlineData("ZQAA")]
    public async Task Create_ReturnsLocationUsingShortCodeAndPersistsCombinedId(string code)
    {
        var request = new LocalityRequest { Code = code };
        using var response = await _fixture.Client.PostAsJsonAsync(LocalitiesUrl, request, ApiJsonOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        Assert.EndsWith($"/Localities/{code}", response.Headers.Location.ToString());

        using var detailsResponse = await _fixture.Client.GetAsync(response.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, detailsResponse.StatusCode);
        var area = await detailsResponse.Content.ReadFromJsonAsync<LocalityDetailsResponse>();
        Assert.NotNull(area);
        Assert.Equal(($"TN-TU-{code}", "TN-TU", code), (area.Id, area.AdministrativeAreaId, area.Code));
    }

    [Fact]
    public async Task Create_AcceptsAnElevenCharacterId()
    {
        const string areaCode = "QXB";
        using var areaResponse = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Countries/TN/AdministrativeAreas", new { Code = areaCode }, ApiJsonOptions);
        Assert.Equal(HttpStatusCode.Created, areaResponse.StatusCode);

        using var response = await _fixture.Client.PostAsJsonAsync(
            $"/api/v1/Countries/TN/AdministrativeAreas/{areaCode}/Localities",
            new LocalityRequest { Code = "ABCD" }, ApiJsonOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var locality = await response.Content.ReadFromJsonAsync<LocalityDetailsResponse>();
        Assert.NotNull(locality);
        Assert.Equal("TN-QXB-ABCD", locality.Id);
    }

    [Theory]
    [InlineData("TN", "BRD", HttpStatusCode.Conflict)]
    [InlineData("TN", "Q1A", HttpStatusCode.BadRequest)]
    [InlineData("ZZ", "ZQA", HttpStatusCode.NotFound)]
    public async Task Create_RejectsInvalidRequests(string countryId, string code, HttpStatusCode expected)
    {
        var request = new LocalityRequest { Code = code };
        using var response = await _fixture.Client.PostAsJsonAsync(
            $"/api/v1/Countries/{countryId}/AdministrativeAreas/TU/Localities", request, ApiJsonOptions);

        Assert.Equal(expected, response.StatusCode);
    }

    [Fact]
    public async Task Put_CannotChangeLocalityCode()
    {
        using var response = await _fixture.Client.PutAsJsonAsync(
            $"{LocalitiesUrl}/BRD", new LocalityRequest { Code = "ZZ" }, ApiJsonOptions);

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact]
    public async Task Delete_HidesLocalityAndItsLocalizedNames()
    {
        using var created = await _fixture.Client.PostAsJsonAsync(
            LocalitiesUrl, new LocalityRequest { Code = "ZQB" }, ApiJsonOptions);
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);

        using var createdName = await _fixture.Client.PostAsJsonAsync(
            $"{LocalitiesUrl}/names/ZQB", new { LanguageId = "en", Name = "Test Area" }, ApiJsonOptions);
        Assert.Equal(HttpStatusCode.Created, createdName.StatusCode);

        using var response = await _fixture.Client.DeleteAsync($"{LocalitiesUrl}/ZQB");
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var details = await _fixture.Client.GetAsync($"{LocalitiesUrl}/ZQB");
        using var names = await _fixture.Client.GetAsync($"{LocalitiesUrl}/names/ZQB");
        Assert.Equal(HttpStatusCode.NotFound, details.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, names.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundForMissingLocality()
    {
        using var response = await _fixture.Client.DeleteAsync($"{LocalitiesUrl}/ZZZ");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
