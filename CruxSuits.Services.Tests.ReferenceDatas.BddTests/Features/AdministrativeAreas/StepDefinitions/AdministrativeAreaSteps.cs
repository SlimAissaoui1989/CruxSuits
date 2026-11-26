using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.AdministrativeAreas.StepDefinitions;

[Binding]
[Scope(Tag = "administrative_areas")]
public sealed class AdministrativeAreaSteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };

    [BeforeScenario("administrative_areas")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("administrative_areas")]
    public async Task StopApi()
    {
        _response?.Dispose();
        if (_fixture is not null)
            await _fixture.DisposeAsync();
    }

    [When("the client creates administrative area \"([^\"]*)\" in country \"([^\"]*)\"")]
    public async Task WhenClientCreatesArea(string code, string countryId) =>
        _response = await _fixture.Client.PostAsJsonAsync(AreasUrl(countryId), new { Code = code }, ApiJsonOptions);

    [When("the client lists administrative areas in country \"([^\"]*)\"")]
    public async Task WhenClientListsAreas(string countryId) =>
        _response = await _fixture.Client.GetAsync(AreasUrl(countryId));

    [When("the client requests administrative area \"([^\"]*)\" in country \"([^\"]*)\"")]
    public async Task WhenClientRequestsArea(string code, string countryId) =>
        _response = await _fixture.Client.GetAsync($"{AreasUrl(countryId)}/{code}");

    [When("the client deletes administrative area \"([^\"]*)\" in country \"([^\"]*)\"")]
    public async Task WhenClientDeletesArea(string code, string countryId) =>
        _response = await _fixture.Client.DeleteAsync($"{AreasUrl(countryId)}/{code}");

    [Then("the administrative area response status is (.*)")]
    public async Task ThenResponseStatusIs(int status)
    {
        var body = await _response!.Content.ReadAsStringAsync();
        Assert.True(status == (int)_response.StatusCode, body);
    }

    [Then("the administrative area response identifies \"([^\"]*)\" with code \"([^\"]*)\"")]
    public async Task ThenResponseIdentifiesArea(string id, string code)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(id, json.RootElement.GetProperty("Id").GetString());
        Assert.Equal(code, json.RootElement.GetProperty("Code").GetString());
    }

    [Then("the administrative area response contains (.*) areas")]
    public async Task ThenResponseContainsAreas(int count)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(count, json.RootElement.GetArrayLength());
    }

    [Then("administrative area \"([^\"]*)\" in country \"([^\"]*)\" can no longer be retrieved")]
    public async Task ThenAreaCannotBeRetrieved(string code, string countryId)
    {
        using var response = await _fixture.Client.GetAsync($"{AreasUrl(countryId)}/{code}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static string AreasUrl(string countryId) => $"/api/v1/Countries/{countryId}/AdministrativeAreas";
}
