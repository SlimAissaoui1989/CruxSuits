using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.CurrencyNames.StepDefinitions;

[Binding]
[Scope(Tag = "currency_names_listing")]
[Scope(Tag = "currency_name_details")]
[Scope(Tag = "currency_name_deletion")]
[Scope(Tag = "currency_name_creation")]
[Scope(Tag = "currency_name_update")]
public sealed partial class CurrencyNamesSteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private readonly List<ExpectedCurrencyName> _expectedNames = [];
    private static readonly JsonSerializerOptions ApiJsonOptions = new() { PropertyNamingPolicy = null };

    [BeforeScenario("currency_names_listing", "currency_name_details", "currency_name_deletion", "currency_name_creation", "currency_name_update")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("currency_names_listing", "currency_name_details", "currency_name_deletion", "currency_name_creation", "currency_name_update")]
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

    private sealed record ExpectedCurrencyName(string LanguageId, string Name);
}
