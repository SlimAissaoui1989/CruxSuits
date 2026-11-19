using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class CountriesApiIntegrationTests
{
    private readonly ReferenceDatasIntegrationFixture _fixture;

    public CountriesApiIntegrationTests(ReferenceDatasIntegrationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetAll_ReturnsSeededCountriesForRequestedLanguage()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/en/Countries");

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK,
            $"Expected GetAll to return 200 OK but received {(int)response.StatusCode}: {responseBody}");
        using var json = JsonDocument.Parse(responseBody);
        var countries = json.RootElement.EnumerateArray().ToArray();
        var unitedStates = Assert.Single(countries, country => country.GetProperty("Id").GetString() == "US");
        Assert.Equal("United States", unitedStates.GetProperty("Name").GetString());
        Assert.Equal("USA", unitedStates.GetProperty("Alpha3Code").GetString());
        Assert.Equal("840", unitedStates.GetProperty("NumericCode").GetString());
    }

    [Fact]
    public async Task GetPhoneCodeOptions_ReturnsSeededOptionsForRequestedLanguage()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Countries/en/phone-code-options");

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK,
            $"Expected GetPhoneCodeOptions to return 200 OK but received {(int)response.StatusCode}: {responseBody}");
        using var json = JsonDocument.Parse(responseBody);
        var options = json.RootElement.EnumerateArray().ToArray();
        var unitedStates = Assert.Single(options, option => option.GetProperty("Id").GetString() == "US");
        Assert.Equal("United States", unitedStates.GetProperty("Name").GetString());
        Assert.Equal("+1", unitedStates.GetProperty("PhoneCode").GetString());
    }

    [Fact]
    public async Task Create_CreatesCountryAndReturnsCreatedAtGetById()
    {
        var request = new
        {
            Id = "QX",
            Alpha3Code = "QXZ",
            NumericCode = "997",
            PhoneCode = "+999",
            CurrencyId = "USD"
        };

        using var response = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Countries",
            request,
            new JsonSerializerOptions { PropertyNamingPolicy = null });

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.Created,
            $"Expected Create to return 201 Created but received {(int)response.StatusCode}: {responseBody}");
        Assert.Contains("/api/v1/Countries/QX", response.Headers.Location?.ToString() ?? string.Empty, StringComparison.Ordinal);
        using var createdJson = JsonDocument.Parse(responseBody);
        Assert.Equal("QX", createdJson.RootElement.GetProperty("Id").GetString());

        using var getResponse = await _fixture.Client.GetAsync("/api/v1/Countries/QX");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        using var countryJson = JsonDocument.Parse(await getResponse.Content.ReadAsStringAsync());
        Assert.Equal("QXZ", countryJson.RootElement.GetProperty("Alpha3Code").GetString());
        Assert.Equal("997", countryJson.RootElement.GetProperty("NumericCode").GetString());
    }

    [Fact]
    public async Task Update_ReturnsOkAndPersistsCountryChanges()
    {
        var request = new
        {
            Alpha3Code = "USA",
            NumericCode = "840",
            PhoneCode = "+1999",
            CurrencyId = "USD"
        };
        using var response = await _fixture.Client.PutAsJsonAsync(
            "/api/v1/Countries/US",
            request,
            new JsonSerializerOptions { PropertyNamingPolicy = null });

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK,
            $"Expected Update to return 200 OK but received {(int)response.StatusCode}: {responseBody}");
        using var json = JsonDocument.Parse(responseBody);
        Assert.Equal("US", json.RootElement.GetProperty("Id").GetString());

        using var getResponse = await _fixture.Client.GetAsync("/api/v1/Countries/US");
        using var countryJson = JsonDocument.Parse(await getResponse.Content.ReadAsStringAsync());
        Assert.Equal("+1999", countryJson.RootElement.GetProperty("PhoneCode").GetString());
    }

    [Fact]
    public async Task Update_ReturnsNotFoundWhenCountryDoesNotExist()
    {
        var request = new
        {
            Alpha3Code = "ZZZ",
            NumericCode = "998",
            PhoneCode = "+998",
            CurrencyId = "USD"
        };
        using var response = await _fixture.Client.PutAsJsonAsync(
            "/api/v1/Countries/ZZ",
            request,
            new JsonSerializerOptions { PropertyNamingPolicy = null });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Remove_ReturnsNoContentAndDeletesCountry()
    {
        var createRequest = new
        {
            Id = "QY",
            Alpha3Code = "QYY",
            NumericCode = "996",
            PhoneCode = "+996",
            CurrencyId = "USD"
        };
        using var createResponse = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Countries",
            createRequest,
            new JsonSerializerOptions { PropertyNamingPolicy = null });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        using var response = await _fixture.Client.DeleteAsync("/api/v1/Countries/QY");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var deletedCountry = await context.Countries.IgnoreQueryFilters().SingleAsync(country => country.Id == "QY");
        Assert.True(deletedCountry.IsDeleted);
    }

    [Fact]
    public async Task Remove_ReturnsNotFoundWhenCountryDoesNotExist()
    {
        using var response = await _fixture.Client.DeleteAsync("/api/v1/Countries/ZZ");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsSeededCountryDetails()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Countries/US");

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK,
            $"Expected GetById to return 200 OK but received {(int)response.StatusCode}: {responseBody}");
        using var json = JsonDocument.Parse(responseBody);
        Assert.Equal("US", json.RootElement.GetProperty("Id").GetString());
        Assert.Equal("USA", json.RootElement.GetProperty("Alpha3Code").GetString());
        Assert.Equal("840", json.RootElement.GetProperty("NumericCode").GetString());
        Assert.Equal("USD", json.RootElement.GetProperty("CurrencyId").GetString());
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundWhenCountryDoesNotExist()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Countries/ZZ");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
