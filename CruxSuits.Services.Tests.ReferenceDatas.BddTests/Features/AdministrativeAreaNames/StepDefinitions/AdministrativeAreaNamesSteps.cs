using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.AdministrativeAreaNames.StepDefinitions;

[Binding]
[Scope(Tag = "administrative_area_names_listing")]
[Scope(Tag = "administrative_area_name_details")]
[Scope(Tag = "administrative_area_name_deletion")]
[Scope(Tag = "administrative_area_name_creation")]
[Scope(Tag = "administrative_area_name_update")]
public sealed partial class AdministrativeAreaNamesSteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private readonly List<ExpectedAdministrativeAreaName> _expectedNames = [];
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };

    [BeforeScenario("administrative_area_names_listing", "administrative_area_name_details", "administrative_area_name_deletion", "administrative_area_name_creation", "administrative_area_name_update")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("administrative_area_names_listing", "administrative_area_name_details", "administrative_area_name_deletion", "administrative_area_name_creation", "administrative_area_name_update")]
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

    private static string NamesUrl(string administrativeAreaId) =>
        $"/api/v1/Countries/{administrativeAreaId[..2]}/AdministrativeAreas/names/{administrativeAreaId[3..]}";

    private static string GetString(JsonElement element, string propertyName) =>
        element.EnumerateObject()
            .Single(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            .Value.GetString()!;

    private sealed record ExpectedAdministrativeAreaName(string LanguageId, string Name);
}
