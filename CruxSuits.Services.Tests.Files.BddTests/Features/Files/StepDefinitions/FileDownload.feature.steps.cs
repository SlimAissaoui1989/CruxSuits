using System.Net;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.Tests.Files.BddTests.Features.Files.StepDefinitions;

public sealed partial class FilesControllerSteps
{
    [Given("a public database-stored file exists")]
    public Task GivenPublicDatabaseStoredFileExists() => CreateFileAsync(true, true);

    [Given("a private database-stored file exists")]
    public Task GivenPrivateDatabaseStoredFileExists() => CreateFileAsync(true, false);

    [Given("a private file exists")]
    public Task GivenPrivateFileExists() => CreateFileAsync(true, false);

    [Given("a public file exists")]
    public Task GivenPublicFileExists() => CreateFileAsync(true, true);

    [Given("no file exists for the requested identifier")]
    public void GivenNoFileExists() => _fileId = Guid.NewGuid();

    [When("an anonymous client downloads the file")]
    [When("an anonymous client downloads it through the public route")]
    [When("an anonymous client downloads the file through the public route")]
    public Task WhenAnonymousClientDownloadsPublicFile() => PublicDownloadAsync(anonymous: true);

    [When("an authorized service downloads it through the internal route")]
    [When("an authorized service downloads the file through the internal route")]
    public Task WhenAuthorizedServiceDownloadsPrivateFile() => InternalDownloadAsync();

    [When("a client requests its download")]
    public Task WhenClientRequestsMissingDownload() => PublicDownloadAsync(anonymous: true);

    [Then("the download response status is (\\d+)")]
    public void ThenDownloadResponseStatusIs(int statusCode) => AssertStatus(statusCode);

    [Then("the response contains the original bytes and file name")]
    public async Task ThenResponseContainsOriginalBytesAndFileName()
    {
        Assert.Equal(_fileName, _response!.Content.Headers.ContentDisposition?.FileName?.Trim('"'));
        await AssertDownloadedBytesAsync();
    }
}
