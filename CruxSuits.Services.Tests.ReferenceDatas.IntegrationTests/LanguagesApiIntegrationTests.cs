using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class LanguagesApiIntegrationTests
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = null };
    private readonly ReferenceDatasIntegrationFixture _fixture;

    public LanguagesApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task GetAll_ReturnsSeededLanguages()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Languages");

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK,
            $"Expected GetAll to return 200 OK but received {(int)response.StatusCode}: {responseBody}");

        using var json = JsonDocument.Parse(responseBody);
        var languageIds = json.RootElement.EnumerateArray()
            .Select(language => language.GetProperty("Id").GetString())
            .ToArray();

        Assert.Contains("en", languageIds);
        Assert.Contains("fr", languageIds);
        Assert.Contains("ar", languageIds);
    }

    [Fact]
    public async Task GetById_ReturnsSeededLanguageDetails()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Languages/ar");

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK,
            $"Expected GetById to return 200 OK but received {(int)response.StatusCode}: {responseBody}");

        using var json = JsonDocument.Parse(responseBody);
        Assert.Equal("ar", json.RootElement.GetProperty("Id").GetString());
        Assert.Equal("Arabic", json.RootElement.GetProperty("EnglishName").GetString());
        Assert.Equal("العربية", json.RootElement.GetProperty("NativeName").GetString());
        Assert.True(json.RootElement.GetProperty("IsRightToLeft").GetBoolean());
    }

    [Fact]
    public async Task GetById_ReturnsNotFoundWhenLanguageDoesNotExist()
    {
        using var response = await _fixture.Client.GetAsync("/api/v1/Languages/ZZ");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Create_CreatesLanguageAndReturnsCreatedAtGetById()
    {
        var request = new LanguageRequest
        {
            Id = "QX",
            EnglishName = "Test language",
            NativeName = "Test",
            IsRightToLeft = false
        };

        using var response = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Languages",
            request,
            JsonOptions);

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.Created,
            $"Expected Create to return 201 Created but received {(int)response.StatusCode}: {responseBody}");
        Assert.Contains("/api/v1/Languages/QX", response.Headers.Location?.ToString() ?? string.Empty, StringComparison.Ordinal);

        using var createdJson = JsonDocument.Parse(responseBody);
        Assert.Equal("QX", createdJson.RootElement.GetProperty("Id").GetString());
        Assert.Equal("Test language", createdJson.RootElement.GetProperty("EnglishName").GetString());

        using var getResponse = await _fixture.Client.GetAsync("/api/v1/Languages/QX");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        using var languageJson = JsonDocument.Parse(await getResponse.Content.ReadAsStringAsync());
        Assert.Equal("Test language", languageJson.RootElement.GetProperty("EnglishName").GetString());
        Assert.Equal("Test", languageJson.RootElement.GetProperty("NativeName").GetString());
        Assert.False(languageJson.RootElement.GetProperty("IsRightToLeft").GetBoolean());
    }

    [Fact]
    public async Task CreateUpdateAndRemove_RefreshSharedLanguageCatalogSnapshot()
    {
        const string languageId = "QW";
        var cache = _fixture.Factory.Services.GetRequiredService<ILanguageCatalogCache>();
        var createRequest = new LanguageRequest
        {
            Id = languageId,
            EnglishName = "Catalog test language",
            NativeName = "Catalog test",
            IsRightToLeft = false
        };

        using var createResponse = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Languages",
            createRequest,
            JsonOptions);

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var afterCreate = await cache.GetAsync();
        Assert.Contains(afterCreate!, language => language.Id == languageId
            && language.EnglishName == "Catalog test language");

        var updateRequest = new UpdateLanguageRequest
        {
            EnglishName = "Updated catalog language",
            NativeName = "Updated catalog",
            IsRightToLeft = true
        };
        using var updateResponse = await _fixture.Client.PutAsJsonAsync(
            $"/api/v1/Languages/{languageId}",
            updateRequest,
            JsonOptions);

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        var afterUpdate = await cache.GetAsync();
        Assert.Contains(afterUpdate!, language => language.Id == languageId
            && language.EnglishName == "Updated catalog language"
            && language.NativeName == "Updated catalog"
            && language.IsRightToLeft);

        using var removeResponse = await _fixture.Client.DeleteAsync($"/api/v1/Languages/{languageId}");

        Assert.Equal(HttpStatusCode.NoContent, removeResponse.StatusCode);
        var afterRemove = await cache.GetAsync();
        Assert.DoesNotContain(afterRemove!, language => language.Id == languageId);
    }

    [Fact]
    public async Task Create_ReturnsConflictWhenLanguageIdAlreadyExists()
    {
        var request = new LanguageRequest
        {
            Id = "en",
            EnglishName = "Duplicate English",
            NativeName = "Duplicate English",
            IsRightToLeft = false
        };

        using var response = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Languages",
            request,
            JsonOptions);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsOkAndPersistsLanguageChanges()
    {
        await EnsureLanguageAsync("QY", "Test language", "Test", isRightToLeft: false);
        var request = new UpdateLanguageRequest
        {
            EnglishName = "Updated language",
            NativeName = "Updated",
            IsRightToLeft = true
        };

        using var response = await _fixture.Client.PutAsJsonAsync(
            "/api/v1/Languages/QY",
            request,
            JsonOptions);

        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.OK,
            $"Expected Update to return 200 OK but received {(int)response.StatusCode}: {responseBody}");
        using var json = JsonDocument.Parse(responseBody);
        Assert.Equal("QY", json.RootElement.GetProperty("Id").GetString());
        Assert.Equal("Updated language", json.RootElement.GetProperty("EnglishName").GetString());
        Assert.Equal("Updated", json.RootElement.GetProperty("NativeName").GetString());
        Assert.True(json.RootElement.GetProperty("IsRightToLeft").GetBoolean());

        using var getResponse = await _fixture.Client.GetAsync("/api/v1/Languages/QY");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        using var languageJson = JsonDocument.Parse(await getResponse.Content.ReadAsStringAsync());
        Assert.Equal("Updated language", languageJson.RootElement.GetProperty("EnglishName").GetString());
        Assert.Equal("Updated", languageJson.RootElement.GetProperty("NativeName").GetString());
        Assert.True(languageJson.RootElement.GetProperty("IsRightToLeft").GetBoolean());
    }

    [Fact]
    public async Task Update_ReturnsNotFoundWhenLanguageDoesNotExist()
    {
        var request = new UpdateLanguageRequest
        {
            EnglishName = "Updated language",
            NativeName = "Updated",
            IsRightToLeft = true
        };

        using var response = await _fixture.Client.PutAsJsonAsync(
            "/api/v1/Languages/ZZ",
            request,
            JsonOptions);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Remove_ReturnsNoContentAndSoftDeletesLanguage()
    {
        await EnsureLanguageAsync("QZ", "Test language", "Test", isRightToLeft: false);

        using var response = await _fixture.Client.DeleteAsync("/api/v1/Languages/QZ");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        using var getResponse = await _fixture.Client.GetAsync("/api/v1/Languages/QZ");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var deletedLanguage = await context.Languages.IgnoreQueryFilters().SingleAsync(language => language.Id == "QZ");
        Assert.True(deletedLanguage.IsDeleted);
    }

    [Fact]
    public async Task Remove_ReturnsNotFoundWhenLanguageDoesNotExist()
    {
        using var response = await _fixture.Client.DeleteAsync("/api/v1/Languages/ZZ");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task EnsureLanguageAsync(string id, string englishName, string nativeName, bool isRightToLeft)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var language = await context.Languages.IgnoreQueryFilters().SingleOrDefaultAsync(item => item.Id == id);
        if (language is null)
        {
            language = new Language
            {
                Id = id,
                CreatedDate = DateTime.UtcNow
            };
            context.Languages.Add(language);
        }

        language.EnglishName = englishName;
        language.NativeName = nativeName;
        language.IsRightToLeft = isRightToLeft;
        language.IsDeleted = false;
        await context.SaveChangesAsync();
    }
}
