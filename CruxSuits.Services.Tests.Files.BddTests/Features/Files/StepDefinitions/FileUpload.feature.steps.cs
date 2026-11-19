using System.Net;
using System.Text.Json;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.Tests.Files.BddTests.Features.Files.StepDefinitions;

public sealed partial class FilesControllerSteps
{
    [Given("the Files API is available")]
    public void GivenFilesApiIsAvailable() => Assert.NotNull(Client);

    [Given("I have a JPEG image named \"(.*)\"")]
    public void GivenIHaveJpegImage(string fileName)
    {
        _fileName = fileName;
        _fileBytes = TestImageBytes;
    }

    [When("I upload the image for database storage and public access")]
    public async Task WhenIUploadTheImage()
    {
        _isSavedInDatabase = true;
        _isPublic = true;
        _uploadResponse = await UploadRequestAsync(_fileName, _fileBytes, _isSavedInDatabase, _isPublic);
    }

    [Then("the upload response status is (\\d+)")]
    public void ThenUploadResponseStatusIs(int statusCode) =>
        Assert.Equal((HttpStatusCode)statusCode, _uploadResponse!.StatusCode);

    [Then("the upload response contains the original file name \"(.*)\"")]
    public async Task ThenUploadResponseContainsOriginalFileName(string expectedFileName)
    {
        using var json = JsonDocument.Parse(await _uploadResponse!.Content.ReadAsStringAsync());
        _fileId = ReadGuid(json.RootElement, "Id");
        Assert.Equal(expectedFileName, ReadString(json.RootElement, "OriginalFileName"));
    }

    [When("an anonymous client downloads the uploaded file")]
    public Task WhenAnonymousClientDownloadsUploadedFile() => PublicDownloadAsync(anonymous: true);

    [Then("the downloaded bytes match the uploaded image")]
    public Task ThenDownloadedBytesMatchUploadedImage() => AssertDownloadedBytesAsync();

    [When("I upload an empty file")]
    public async Task WhenIUploadAnEmptyFile()
    {
        using var multipart = new MultipartFormDataContent();
        multipart.Add(new ByteArrayContent([]), "File", "empty.jpg");
        multipart.Add(new StringContent("true"), "IsSavedInDatabase");
        multipart.Add(new StringContent("true"), "IsPublic");
        _uploadResponse = await Client.PostAsync("/api/v1/Files", multipart);
    }
}
