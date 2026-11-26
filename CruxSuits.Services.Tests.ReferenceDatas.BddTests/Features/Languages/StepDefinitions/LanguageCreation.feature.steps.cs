using System.Net;
using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Languages.StepDefinitions;

public sealed partial class LanguagesControllerSteps
{
    [When("the client creates language \"(.*)\" with English name \"(.*)\", native name \"(.*)\", and right-to-left set to (.*)")]
    public Task WhenClientCreatesLanguage(string languageId, string englishName, string nativeName, bool isRightToLeft) =>
        SendAsync(HttpMethod.Post, "/api/v1/Languages", new
        {
            Id = languageId,
            EnglishName = englishName,
            NativeName = nativeName,
            IsRightToLeft = isRightToLeft
        });

    [Then("the created language can be retrieved")]
    public async Task ThenCreatedLanguageCanBeRetrieved()
    {
        using var createdJson = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        var languageId = GetString(createdJson.RootElement, "Id");
        using var detailsResponse = await _fixture.Client.GetAsync($"/api/v1/Languages/{languageId}");
        Assert.Equal(HttpStatusCode.OK, detailsResponse.StatusCode);
    }
}
