using Reqnroll;
using Xunit;

namespace CruxSuits.Services.Tests.Files.BddTests.Features.Files.StepDefinitions;

public sealed partial class FilesControllerSteps
{
    [Given("a file exists")]
    public Task GivenAFileExistsForAuthorization() => CreateFileAsync(true, false);

    [When("a client with \"(.*)\" credentials performs the protected operation \"(.*)\"")]
    public Task WhenClientWithCredentialsPerformsOperation(string credential, string operation)
    {
        var method = operation switch
        {
            "upload" => HttpMethod.Post,
            "update" => HttpMethod.Put,
            "delete" => HttpMethod.Delete,
            "private download" or "private preview" => HttpMethod.Get,
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "Unknown protected operation.")
        };
        var uri = operation switch
        {
            "upload" => "/api/v1/Files",
            "update" or "delete" => $"/api/v1/Files/{_fileId}",
            "private download" => $"/internal/files/{_fileId}/download",
            "private preview" => $"/internal/files/{_fileId}/preview",
            _ => throw new ArgumentOutOfRangeException(nameof(operation), operation, "Unknown protected operation.")
        };
        HttpContent? content = operation switch
        {
            "upload" => CreateUploadContent("auth-check.jpg", TestImageBytes, true, true),
            "update" => CreateUpdateContent(_description, _isPublic, _isSavedInDatabase),
            _ => null
        };
        return SendWithAuthenticationAsync(method, uri, credential, content);
    }

    [Then("the response status is (\\d+)")]
    public void ThenResponseStatusIs(int statusCode) => AssertStatus(statusCode);
}
