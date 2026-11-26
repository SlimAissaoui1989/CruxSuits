using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.LocalityNames.StepDefinitions;

[Binding]
[Scope(Tag = "locality_names_listing")]
[Scope(Tag = "locality_name_details")]
[Scope(Tag = "locality_name_deletion")]
[Scope(Tag = "locality_name_creation")]
[Scope(Tag = "locality_name_update")]
public sealed partial class LocalityNamesSteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private readonly List<ExpectedLocalityName> _expectedNames = [];
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };

    [BeforeScenario("locality_names_listing", "locality_name_details", "locality_name_deletion", "locality_name_creation", "locality_name_update")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("locality_names_listing", "locality_name_details", "locality_name_deletion", "locality_name_creation", "locality_name_update")]
    public async Task StopApi()
    {
        _response?.Dispose();
        if (_fixture is not null)
            await _fixture.DisposeAsync();
    }

    [Given("the supported languages are \"(.*)\", \"(.*)\" and \"(.*)\"")]
    public async Task GivenSupportedLanguages(string firstLanguageId, string secondLanguageId, string thirdLanguageId)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var supportedLanguageIds = await context.Languages.Select(language => language.Id).ToListAsync();

        Assert.Contains(firstLanguageId, supportedLanguageIds);
        Assert.Contains(secondLanguageId, supportedLanguageIds);
        Assert.Contains(thirdLanguageId, supportedLanguageIds);
    }

    private static string NamesUrl(string localityId)
    {
        var parts = localityId.Split('-');
        return $"/api/v1/Countries/{parts[0]}/AdministrativeAreas/{parts[1]}/Localities/names/{parts[2]}";
    }

    private static string GetString(JsonElement element, string propertyName) =>
        element.EnumerateObject()
            .Single(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            .Value.GetString()!;

    private sealed record ExpectedLocalityName(string LanguageId, string Name);
}
