using System.Net;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.Tests.Files.BddTests.Features.Files.StepDefinitions;

public sealed partial class FilesControllerSteps
{
    [When("an authorized service deletes the file")]
    public Task WhenAuthorizedServiceDeletesFile() => SendAsync(HttpMethod.Delete, $"/api/v1/Files/{_fileId}");

    [Then("the delete response status is (\\d+)")]
    public void ThenDeleteResponseStatusIs(int statusCode) => AssertStatus(statusCode);

    [Then("the file can no longer be downloaded")]
    public async Task ThenFileCanNoLongerBeDownloaded()
    {
        if (_isPublic)
            await PublicDownloadAsync(anonymous: true);
        else
            await InternalDownloadAsync();
        Assert.Equal(HttpStatusCode.NotFound, _response!.StatusCode);
    }
}
