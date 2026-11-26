using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class CountryNamesApiIntegrationTests
{
    private readonly ReferenceDatasIntegrationFixture _fixture;

    public CountryNamesApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task GetAll_ReturnsAllSeededTranslationsForCountry()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Countries/US/names");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var names = json.RootElement.EnumerateArray().ToArray();
        Assert.Equal(3, names.Length);
        Assert.Contains(names, name =>
            name.GetProperty("LanguageId").GetString() == "en"
            && name.GetProperty("LanguageName").GetString() == "English (English)"
            && name.GetProperty("Name").GetString() == "United States"
            && name.GetProperty("Nationality").GetString() == "American");
        Assert.Contains(names, name =>
            name.GetProperty("LanguageId").GetString() == "fr"
            && name.GetProperty("LanguageName").GetString() == "French (Français)"
            && name.GetProperty("Name").GetString() == "États-Unis"
            && name.GetProperty("Nationality").GetString() == "Américain");
        Assert.Contains(names, name =>
            name.GetProperty("LanguageId").GetString() == "ar"
            && name.GetProperty("LanguageName").GetString() == "Arabic (العربية)"
            && name.GetProperty("Name").GetString() == "الولايات المتحدة"
            && name.GetProperty("Nationality").GetString() == "أمريكي");
    }

    [Theory]
    [InlineData("en", "English (English)", "United States", "American")]
    [InlineData("fr", "French (Français)", "États-Unis", "Américain")]
    [InlineData("ar", "Arabic (العربية)", "الولايات المتحدة", "أمريكي")]
    public async Task GetByLanguage_ReturnsSeededTranslation(
        string languageId,
        string languageName,
        string name,
        string nationality)
    {
        using var response = await _fixture.Client.GetAsync($"/api/v1/Countries/US/names/{languageId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(languageId, json.RootElement.GetProperty("LanguageId").GetString());
        Assert.Equal(languageName, json.RootElement.GetProperty("LanguageName").GetString());
        Assert.Equal(name, json.RootElement.GetProperty("Name").GetString());
        Assert.Equal(nationality, json.RootElement.GetProperty("Nationality").GetString());
    }

    [Fact]
    public async Task GetAll_ReturnsNotFoundWhenCountryDoesNotExist()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Countries/XX/names");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyListWhenCountryHasNoTranslations()
    {
        await EnsureCountryWithoutTranslationsExistsAsync();

        using var response = await _fixture.Client.GetAsync("/api/v1/Countries/QJ/names");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Empty(json.RootElement.EnumerateArray());
    }

    [Fact]
    public async Task GetByLanguage_ReturnsNotFoundWhenTranslationDoesNotExist()
    {
        await EnsureCountryWithoutTranslationsExistsAsync();

        using var response = await _fixture.Client.GetAsync("/api/v1/Countries/QJ/names/fr");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Update_UpdatesNameAndNationalityAndPreservesCountryAndLanguage()
    {
        await EnsureCountryTranslationExistsAsync();
        var request = new CountryNameUpdateRequest
        {
            Name = "Testland Updated",
            Nationality = "Testlander"
        };

        using var response = await _fixture.Client.PutAsJsonAsync(
            "/api/v1/Countries/QJ/names/en",
            request,
            new JsonSerializerOptions { PropertyNamingPolicy = null });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("en", json.RootElement.GetProperty("LanguageId").GetString());
        Assert.Equal("Testland Updated", json.RootElement.GetProperty("Name").GetString());
        Assert.Equal("Testlander", json.RootElement.GetProperty("Nationality").GetString());

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var updated = await context.CountryNames.SingleAsync(name => name.CountryId == "QJ" && name.LanguageId == "en");
        Assert.Equal("QJ", updated.CountryId);
        Assert.Equal("en", updated.LanguageId);
        Assert.Equal(request.Name, updated.Name);
        Assert.Equal(request.Nationality, updated.Nationality);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundWhenTranslationDoesNotExist()
    {
        await EnsureCountryWithoutTranslationsExistsAsync();
        var request = new CountryNameUpdateRequest
        {
            Name = "Testland Updated",
            Nationality = "Testlander"
        };

        using var response = await _fixture.Client.PutAsJsonAsync(
            "/api/v1/Countries/QJ/names/en",
            request,
            new JsonSerializerOptions { PropertyNamingPolicy = null });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_RemovesTranslationAndReturnsNoContent()
    {
        await EnsureCountryTranslationExistsAsync();

        using var response = await _fixture.Client.DeleteAsync("/api/v1/Countries/QJ/names/en");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var getResponse = await _fixture.Client.GetAsync("/api/v1/Countries/QJ/names/en");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundWhenTranslationDoesNotExist()
    {
        await EnsureCountryWithoutTranslationsExistsAsync();

        using var response = await _fixture.Client.DeleteAsync("/api/v1/Countries/QJ/names/en");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task EnsureCountryTranslationExistsAsync()
    {
        await EnsureCountryWithoutTranslationsExistsAsync();
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        context.CountryNames.Add(new CountryName
        {
            CountryId = "QJ",
            LanguageId = "en",
            Name = "Testland",
            Nationality = "Tester"
        });
        await context.SaveChangesAsync();
    }

    private async Task EnsureCountryWithoutTranslationsExistsAsync()
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        if (!await context.Countries.AnyAsync(country => country.Id == "QJ"))
        {
            context.Countries.Add(new Country
            {
                Id = "QJ",
                Alpha3Code = "QJZ",
                NumericCode = "995",
                CurrencyId = "USD"
            });
        }

        var translations = await context.CountryNames.Where(name => name.CountryId == "QJ").ToListAsync();
        context.CountryNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }
}
