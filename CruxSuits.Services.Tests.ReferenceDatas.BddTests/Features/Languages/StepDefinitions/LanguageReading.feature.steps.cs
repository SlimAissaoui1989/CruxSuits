using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Languages.StepDefinitions;

public sealed partial class LanguagesControllerSteps
{
    [When("the client lists languages")]
    public Task WhenClientListsLanguages() => SendAsync(HttpMethod.Get, "/api/v1/Languages");

    [When("the client requests language \"(.*)\"")]
    public Task WhenClientRequestsLanguage(string languageId) =>
        SendAsync(HttpMethod.Get, $"/api/v1/Languages/{languageId}");

    [Then("the language list contains \"(.*)\", \"(.*)\" and \"(.*)\"")]
    public async Task ThenLanguageListContains(string firstId, string secondId, string thirdId)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var ids = json.RootElement.EnumerateArray().Select(item => GetString(item, "Id")).ToHashSet(StringComparer.OrdinalIgnoreCase);
        Assert.Contains(firstId, ids);
        Assert.Contains(secondId, ids);
        Assert.Contains(thirdId, ids);
    }

    [Then("the language details contain id \"(.*)\", English name \"(.*)\", native name \"(.*)\", and right-to-left set to (.*)")]
    public async Task ThenLanguageDetailsContainFields(string languageId, string englishName, string nativeName, bool isRightToLeft)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var language = json.RootElement;
        Assert.Equal(languageId, GetString(language, "Id"));
        Assert.Equal(englishName, GetString(language, "EnglishName"));
        Assert.Equal(nativeName, GetString(language, "NativeName"));
        Assert.Equal(isRightToLeft, GetBoolean(language, "IsRightToLeft"));
    }
}
