using System.Net;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Languages.StepDefinitions;

public sealed partial class LanguagesControllerSteps
{
    [When("the client deletes language \"(.*)\"")]
    public Task WhenClientDeletesLanguage(string languageId)
    {
        _deletedLanguageId = languageId;
        return SendAsync(HttpMethod.Delete, $"/api/v1/Languages/{languageId}");
    }

    [Then("the deleted language can no longer be retrieved")]
    public async Task ThenDeletedLanguageCanNoLongerBeRetrieved()
    {
        using var response = await _fixture.Client.GetAsync($"/api/v1/Languages/{_deletedLanguageId}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
