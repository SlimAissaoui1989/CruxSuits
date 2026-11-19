using System.Net.Http.Json;
using System.Text.Json;
using Reqnroll;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Countries.StepDefinitions;

[Binding]
public sealed partial class CountriesControllerSteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private string? _deletedCountryId;

    [BeforeScenario("countries_controller")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("countries_controller")]
    public async Task StopApi()
    {
        _response?.Dispose();
        if (_fixture is not null)
            await _fixture.DisposeAsync();
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
}
