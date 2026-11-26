using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class CurrencyNamesApiIntegrationTests
{
    private readonly ReferenceDatasIntegrationFixture _fixture;
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };

    public CurrencyNamesApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task GetAll_ReturnsAllSeededTranslationsForCurrency()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Currencies/USD/names");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var names = json.RootElement.EnumerateArray().ToArray();
        Assert.Equal(3, names.Length);
        Assert.Contains(names, name =>
            name.GetProperty("LanguageId").GetString() == "en"
            && name.GetProperty("LanguageName").GetString() == "English (English)"
            && name.GetProperty("Name").GetString() == "US Dollar");
        Assert.Contains(names, name =>
            name.GetProperty("LanguageId").GetString() == "fr"
            && name.GetProperty("LanguageName").GetString() == "French (Français)"
            && name.GetProperty("Name").GetString() == "dollar des États-Unis");
        Assert.Contains(names, name =>
            name.GetProperty("LanguageId").GetString() == "ar"
            && name.GetProperty("LanguageName").GetString() == "Arabic (العربية)"
            && name.GetProperty("Name").GetString() == "دولار أمريكي");
    }

    [Theory]
    [InlineData("en", "English (English)", "US Dollar")]
    [InlineData("fr", "French (Français)", "dollar des États-Unis")]
    [InlineData("ar", "Arabic (العربية)", "دولار أمريكي")]
    public async Task GetByLanguage_ReturnsSeededTranslation(
        string languageId,
        string languageName,
        string name)
    {
        using var response = await _fixture.Client.GetAsync($"/api/v1/Currencies/USD/names/{languageId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal(languageId, json.RootElement.GetProperty("LanguageId").GetString());
        Assert.Equal(languageName, json.RootElement.GetProperty("LanguageName").GetString());
        Assert.Equal(name, json.RootElement.GetProperty("Name").GetString());
    }

    [Fact]
    public async Task GetAll_ReturnsNotFoundWhenCurrencyDoesNotExist()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Currencies/ZZZ/names");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyListWhenCurrencyHasNoTranslations()
    {
        await EnsureCurrencyWithoutTranslationsExistsAsync();

        using var response = await _fixture.Client.GetAsync("/api/v1/Currencies/QXZ/names");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Empty(json.RootElement.EnumerateArray());
    }

    [Fact]
    public async Task GetByLanguage_ReturnsNotFoundWhenTranslationDoesNotExist()
    {
        await EnsureCurrencyWithoutTranslationsExistsAsync();

        using var response = await _fixture.Client.GetAsync("/api/v1/Currencies/QXZ/names/fr");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

[Fact]
    public async Task Create_ReturnsCreatedAndPersistsTranslation()
    {
        await EnsureCurrencyWithoutTranslationsExistsAsync();
        var request = new CurrencyNameRequest { LanguageId = "fr", Name = "Devise de test" };

        using var response = await _fixture.Client.PostAsJsonAsync("/api/v1/Currencies/QXZ/names", request, ApiJsonOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        using var getResponse = await _fixture.Client.GetAsync(response.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        using var json = JsonDocument.Parse(await getResponse.Content.ReadAsStringAsync());
        Assert.Equal(request.LanguageId, json.RootElement.GetProperty("LanguageId").GetString());
        Assert.Equal(request.Name, json.RootElement.GetProperty("Name").GetString());
    }

    [Fact]
    public async Task Create_ReturnsConflictWhenTranslationAlreadyExists()
    {
        await EnsureCurrencyTranslationExistsAsync();
        var request = new CurrencyNameRequest { LanguageId = "en", Name = "Duplicate Name" };

        using var response = await _fixture.Client.PostAsJsonAsync("/api/v1/Currencies/QXZ/names", request, ApiJsonOptions);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Theory]
    [InlineData("ZZZ", "en", HttpStatusCode.NotFound)]
    [InlineData("USD", "zz", HttpStatusCode.BadRequest)]
    public async Task Create_RejectsMissingCurrencyOrLanguage(string currencyId, string languageId, HttpStatusCode expectedStatus)
    {
        var request = new CurrencyNameRequest { LanguageId = languageId, Name = "Test Currency" };

        using var response = await _fixture.Client.PostAsJsonAsync($"/api/v1/Currencies/{currencyId}/names", request, ApiJsonOptions);

        Assert.Equal(expectedStatus, response.StatusCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("Dollar2")]
    [InlineData("US-Dollar")]
    public async Task CreateAndUpdate_RejectInvalidNames(string name)
    {
        await EnsureCurrencyTranslationExistsAsync();
        var createRequest = new CurrencyNameRequest { LanguageId = "fr", Name = name };
        var updateRequest = new CurrencyNameUpdateRequest { Name = name };

        using var createResponse = await _fixture.Client.PostAsJsonAsync("/api/v1/Currencies/QXZ/names", createRequest, ApiJsonOptions);
        using var updateResponse = await _fixture.Client.PutAsJsonAsync("/api/v1/Currencies/QXZ/names/en", updateRequest, ApiJsonOptions);

        Assert.Equal(HttpStatusCode.BadRequest, createResponse.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var names = await context.CurrencyNames.Where(translation => translation.CurrencyId == "QXZ").ToListAsync();
        var original = Assert.Single(names);
        Assert.Equal("en", original.LanguageId);
        Assert.Equal("Test Currency", original.Name);
    }

    [Fact]
    public async Task Update_UpdatesNameAndPreservesCurrencyAndLanguage()
    {
        await EnsureCurrencyTranslationExistsAsync();
        var request = new CurrencyNameUpdateRequest
        {
            Name = "Test Currency Updated"
        };

        using var response = await _fixture.Client.PutAsJsonAsync(
            "/api/v1/Currencies/QXZ/names/en",
            request,
            ApiJsonOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.Equal("en", json.RootElement.GetProperty("LanguageId").GetString());
        Assert.Equal("Test Currency Updated", json.RootElement.GetProperty("Name").GetString());

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var updated = await context.CurrencyNames.SingleAsync(name => name.CurrencyId == "QXZ" && name.LanguageId == "en");
        Assert.Equal("QXZ", updated.CurrencyId);
        Assert.Equal("en", updated.LanguageId);
        Assert.Equal(request.Name, updated.Name);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundWhenTranslationDoesNotExist()
    {
        await EnsureCurrencyWithoutTranslationsExistsAsync();
        var request = new CurrencyNameUpdateRequest
        {
            Name = "Test Currency Updated"
        };

        using var response = await _fixture.Client.PutAsJsonAsync(
            "/api/v1/Currencies/QXZ/names/en",
            request,
            ApiJsonOptions);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_RemovesTranslationAndReturnsNoContent()
    {
        await EnsureCurrencyTranslationExistsAsync();

        using var response = await _fixture.Client.DeleteAsync("/api/v1/Currencies/QXZ/names/en");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var getResponse = await _fixture.Client.GetAsync("/api/v1/Currencies/QXZ/names/en");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundWhenTranslationDoesNotExist()
    {
        await EnsureCurrencyWithoutTranslationsExistsAsync();

        using var response = await _fixture.Client.DeleteAsync("/api/v1/Currencies/QXZ/names/en");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task EnsureCurrencyTranslationExistsAsync()
    {
        await EnsureCurrencyWithoutTranslationsExistsAsync();
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        context.CurrencyNames.Add(new CurrencyName
        {
            CurrencyId = "QXZ",
            LanguageId = "en",
            Name = "Test Currency"
        });
        await context.SaveChangesAsync();
    }

    private async Task EnsureCurrencyWithoutTranslationsExistsAsync()
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        if (!await context.Currencies.AnyAsync(currency => currency.Id == "QXZ"))
        {
            context.Currencies.Add(new Currency
            {
                Id = "QXZ",
                NumericCode = "993"
            });
        }

        var translations = await context.CurrencyNames.Where(name => name.CurrencyId == "QXZ").ToListAsync();
        context.CurrencyNames.RemoveRange(translations);
        await context.SaveChangesAsync();
    }
}
