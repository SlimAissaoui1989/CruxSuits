using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Languages.StepDefinitions;

public sealed partial class LanguagesControllerSteps
{
    [When("the client updates language \"(.*)\" with English name \"(.*)\", native name \"(.*)\", and right-to-left set to (.*)")]
    public Task WhenClientUpdatesLanguage(string languageId, string englishName, string nativeName, bool isRightToLeft) =>
        SendAsync(HttpMethod.Put, $"/api/v1/Languages/{languageId}", new
        {
            EnglishName = englishName,
            NativeName = nativeName,
            IsRightToLeft = isRightToLeft
        });

    [Then("the updated language details contain id \"(.*)\", English name \"(.*)\", native name \"(.*)\", and right-to-left set to (.*)")]
    public async Task ThenUpdatedLanguageDetailsContainFields(string languageId, string englishName, string nativeName, bool isRightToLeft)
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var language = json.RootElement;
        Assert.Equal(languageId, GetString(language, "Id"));
        Assert.Equal(englishName, GetString(language, "EnglishName"));
        Assert.Equal(nativeName, GetString(language, "NativeName"));
        Assert.Equal(isRightToLeft, GetBoolean(language, "IsRightToLeft"));
    }
}
