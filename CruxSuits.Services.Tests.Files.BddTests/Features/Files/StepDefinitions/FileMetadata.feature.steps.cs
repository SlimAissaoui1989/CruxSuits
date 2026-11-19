using System.Text;
using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.Tests.Files.BddTests.Features.Files.StepDefinitions;

public sealed partial class FilesControllerSteps
{
    [Given("a (public|private) database-stored file with description \"(.*)\" exists")]
    public Task GivenDatabaseStoredFileWithDescriptionExists(string visibility, string description)
    {
        _description = description;
        return CreateFileAsync(true, visibility == "public", description: description);
    }

    [Given("a database-stored file exists")]
    public Task GivenDatabaseStoredFileExists() => CreateFileAsync(true, false);

    [Given("a disk-stored file exists")]
    public Task GivenDiskStoredFileExists() => CreateFileAsync(false, false);

    [When("an authorized service changes the description to \"(.*)\" and makes it private")]
    public Task WhenAuthorizedServiceChangesDescriptionAndPrivacy(string description)
    {
        _description = description;
        _isPublic = false;
        return UpdateFileAsync(description, _isPublic, _isSavedInDatabase);
    }

    [When("an authorized service changes its storage to disk")]
    public Task WhenAuthorizedServiceMovesFileToDisk()
    {
        _isSavedInDatabase = false;
        return UpdateFileAsync(_description, _isPublic, _isSavedInDatabase);
    }

    [When("an authorized service changes its storage to the database")]
    public Task WhenAuthorizedServiceMovesFileToDatabase()
    {
        _isSavedInDatabase = true;
        return UpdateFileAsync(_description, _isPublic, _isSavedInDatabase);
    }

    [When("an authorized service tries to replace its content during update")]
    public Task WhenAuthorizedServiceTriesToReplaceContent()
    {
        var payload = JsonSerializer.Serialize(new
        {
            Description = "Changed",
            IsPublic = _isPublic,
            IsSavedInDatabase = _isSavedInDatabase,
            Content = Convert.ToBase64String([0x01, 0x02, 0x03])
        });
        return SendAsync(HttpMethod.Put, $"/api/v1/Files/{_fileId}", new StringContent(payload, Encoding.UTF8, "application/json"));
    }

    [When("an authorized service updates the file")]
    public Task WhenAuthorizedServiceUpdatesMissingFile() => UpdateFileAsync("Missing", _isPublic, _isSavedInDatabase);

    [Then("the update response status is (\\d+)")]
    public void ThenUpdateResponseStatusIs(int statusCode) => AssertStatus(statusCode);

    [Then("the new metadata is stored")]
    public async Task ThenNewMetadataIsStored()
    {
        using var json = JsonDocument.Parse(await _response!.Content.ReadAsStringAsync());
        Assert.Equal(_description, ReadString(json.RootElement, "Description"));
        Assert.Equal(_isPublic, ReadBoolean(json.RootElement, "IsPublic"));
    }

    [Then("the original content remains downloadable")]
    public async Task ThenOriginalContentRemainsDownloadable()
    {
        await InternalDownloadAsync();
        Assert.Equal(System.Net.HttpStatusCode.OK, _response!.StatusCode);
        await AssertDownloadedBytesAsync();
    }
}
