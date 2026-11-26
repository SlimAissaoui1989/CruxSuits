using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Localities.StepDefinitions;

[Binding]
[Scope(Tag = "localities")]
public sealed partial class LocalitySteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };

    [BeforeScenario("localities")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("localities")]
    public async Task StopApi()
    {
        _response?.Dispose();
        if (_fixture is not null)
            await _fixture.DisposeAsync();
    }

    [When("the client creates locality \"([^\"]*)\" in country \"([^\"]*)\" and area \"([^\"]*)\"")]
    public async Task WhenClientCreatesArea(string code, string countryId, string areaCode) =>
        _response = await _fixture.Client.PostAsJsonAsync(LocalitiesUrl(countryId, areaCode), new { Code = code }, ApiJsonOptions);

    [When("the client lists localities in country \"([^\"]*)\" and area \"([^\"]*)\"")]
    public async Task WhenClientListsAreas(string countryId, string areaCode) =>
        _response = await _fixture.Client.GetAsync(LocalitiesUrl(countryId, areaCode));

    [When("the client requests locality \"([^\"]*)\" in country \"([^\"]*)\" and area \"([^\"]*)\"")]
    public async Task WhenClientRequestsArea(string code, string countryId, string areaCode) =>
        _response = await _fixture.Client.GetAsync($"{LocalitiesUrl(countryId, areaCode)}/{code}");

    [When("the client deletes locality \"([^\"]*)\" in country \"([^\"]*)\" and area \"([^\"]*)\"")]
    public async Task WhenClientDeletesArea(string code, string countryId, string areaCode) =>
        _response = await _fixture.Client.DeleteAsync($"{LocalitiesUrl(countryId, areaCode)}/{code}");

    [Then("the locality response status is (.*)")]
    public async Task ThenResponseStatusIs(int status)
    {
        var body = await _response!.Content.ReadAsStringAsync();
        Assert.True(status == (int)_response.StatusCode, body);
    }

    [Then("the locality response identifies \"([^\"]*)\" with code \"([^\"]*)\"")]
    public async Task ThenResponseIdentifiesArea(string id, string code)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(id, json.RootElement.GetProperty("Id").GetString());
        Assert.Equal(code, json.RootElement.GetProperty("Code").GetString());
    }

    [Then("the locality response contains (.*) localities")]
    public async Task ThenResponseContainsAreas(int count)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(count, json.RootElement.GetArrayLength());
    }

    [Then("locality \"([^\"]*)\" in country \"([^\"]*)\" and area \"([^\"]*)\" can no longer be retrieved")]
    public async Task ThenAreaCannotBeRetrieved(string code, string countryId, string areaCode)
    {
        using var response = await _fixture.Client.GetAsync($"{LocalitiesUrl(countryId, areaCode)}/{code}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private static string LocalitiesUrl(string countryId, string areaCode) => $"/api/v1/Countries/{countryId}/AdministrativeAreas/{areaCode}/Localities";
}
