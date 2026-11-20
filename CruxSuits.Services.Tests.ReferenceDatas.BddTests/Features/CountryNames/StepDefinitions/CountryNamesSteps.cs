using System.Net;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CountryNames.StepDefinitions;

[Binding]
public sealed partial class CountryNamesSteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private readonly List<ExpectedCountryName> _expectedNames = [];
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };

    [BeforeScenario("country_names_listing", "country_name_details", "country_name_deletion", "country_name_creation", "country_name_update")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("country_names_listing", "country_name_details", "country_name_deletion", "country_name_creation", "country_name_update")]
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

    private static string GetString(JsonElement element, string propertyName) =>
        element.EnumerateObject()
            .Single(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            .Value.GetString()!;

    private sealed record ExpectedCountryName(string LanguageId, string Name, string Nationality);
}
