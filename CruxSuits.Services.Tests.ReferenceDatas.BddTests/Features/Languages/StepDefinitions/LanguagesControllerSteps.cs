using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Tests.BddTests;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Languages.StepDefinitions;

[Binding]
public sealed partial class LanguagesControllerSteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private string? _deletedLanguageId;

    [BeforeScenario("languages_controller")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("languages_controller")]
    public async Task StopApi()
    {
        _response?.Dispose();
        if (_fixture is not null)
            await _fixture.DisposeAsync();
    }

    [Given("the test language \"(.*)\" exists")]
    public Task GivenTestLanguageExists(string languageId) => EnsureLanguageAsync(languageId);

    [Then("the language API response status is (.*)")]
    public async Task ThenLanguageApiResponseStatusIs(int statusCode)
    {
        var responseBody = _response is null ? string.Empty : await _response.Content.ReadAsStringAsync();
        Assert.True(_response is not null && (int)_response.StatusCode == statusCode,
            $"Expected {(HttpStatusCode)statusCode} but received {(int?)_response?.StatusCode}: {responseBody}");
    }

    private async Task EnsureLanguageAsync(string languageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        if (await context.Languages.FindAsync(languageId) is not null)
            return;

        context.Languages.Add(new Language
        {
            Id = languageId,
            EnglishName = "Test language",
            NativeName = "Test",
            IsRightToLeft = false,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        });
        await context.SaveChangesAsync();
    }

    private async Task SendAsync(HttpMethod method, string uri, object? body = null)
    {
        _response?.Dispose();
        using var request = new HttpRequestMessage(method, uri);
        if (body is not null)
            request.Content = JsonContent.Create(body, options: new JsonSerializerOptions { PropertyNamingPolicy = null });
        _response = await _fixture.Client.SendAsync(request);
    }

    private static string GetString(JsonElement element, string propertyName) =>
        element.EnumerateObject()
            .Single(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            .Value.GetString()!;

    private static bool GetBoolean(JsonElement element, string propertyName) =>
        element.EnumerateObject()
            .Single(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            .Value.GetBoolean();
}
