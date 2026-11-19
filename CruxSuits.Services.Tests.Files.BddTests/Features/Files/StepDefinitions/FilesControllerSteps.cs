using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CruxSuits.Services.Files.Tests.IntegrationTests;
using Reqnroll;
using Xunit;

using CruxSuits.Services.Tests.Files.BddTests;

namespace CruxSuits.Services.Tests.Files.BddTests.Features.Files.StepDefinitions;

[Binding]
public sealed partial class FilesControllerSteps
{
    private static readonly byte[] TestImageBytes =
    [
        0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46,
        0x49, 0x46, 0x00, 0x01, 0x01, 0x00, 0x00, 0x01
    ];
    private static readonly byte[] TestArchiveBytes = [0x01, 0x02, 0x03];

    private string _fileName = "bdd-file.jpg";
    private byte[] _fileBytes = TestImageBytes;
    private Guid _fileId = Guid.NewGuid();
    private bool _isPublic;
    private bool _isSavedInDatabase = true;
    private string? _description;
    private HttpResponseMessage? _response;
    private HttpResponseMessage? _uploadResponse;

    private static HttpClient Client => FilesBddTestRunHooks.Fixture.Client;

    [AfterScenario]
    public void DisposeResponses()
    {
        _response?.Dispose();
        _uploadResponse?.Dispose();
    }

    private async Task CreateFileAsync(
        bool isSavedInDatabase,
        bool isPublic,
        string? fileName = null,
        byte[]? bytes = null,
        string? description = null)
    {
        _fileName = fileName ?? _fileName;
        _fileBytes = bytes ?? _fileBytes;
        _isSavedInDatabase = isSavedInDatabase;
        _isPublic = isPublic;
        _description = description;

        using var response = await UploadRequestAsync(_fileName, _fileBytes, isSavedInDatabase, isPublic, description);
        var body = await response.Content.ReadAsStringAsync();
        Assert.True(response.StatusCode == HttpStatusCode.Created,
            $"Test setup upload expected 201 but received {(int)response.StatusCode}: {body}");
        using var json = JsonDocument.Parse(body);
        _fileId = ReadGuid(json.RootElement, "Id");
    }

    private async Task<HttpResponseMessage> UploadRequestAsync(
        string fileName,
        byte[] bytes,
        bool isSavedInDatabase,
        bool isPublic,
        string? description = null)
    {
        using var multipart = CreateUploadContent(fileName, bytes, isSavedInDatabase, isPublic, description);
        return await Client.PostAsync("/api/v1/Files", multipart);
    }

    private static MultipartFormDataContent CreateUploadContent(
        string fileName,
        byte[] bytes,
        bool isSavedInDatabase,
        bool isPublic,
        string? description = null)
    {
        var multipart = new MultipartFormDataContent();
        var file = new ByteArrayContent(bytes);
        file.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(fileName));
        multipart.Add(file, "File", fileName);
        multipart.Add(new StringContent(isSavedInDatabase.ToString()), "IsSavedInDatabase");
        multipart.Add(new StringContent(isPublic.ToString()), "IsPublic");
        if (description is not null)
            multipart.Add(new StringContent(description), "Description");
        return multipart;
    }

    private static StringContent CreateUpdateContent(string? description, bool isPublic, bool isSavedInDatabase)
    {
        var payload = JsonSerializer.Serialize(new
        {
            Description = description,
            IsPublic = isPublic,
            IsSavedInDatabase = isSavedInDatabase
        });
        return new StringContent(payload, Encoding.UTF8, "application/json");
    }

    private Task PublicDownloadAsync(bool anonymous) =>
        SendAsync(HttpMethod.Get, $"/api/v1/Files/{_fileId}/download", authenticationMode: anonymous ? "anonymous" : null);

    private Task InternalDownloadAsync() => SendAsync(HttpMethod.Get, $"/internal/files/{_fileId}/download");

    private Task PreviewAsync(bool isInternal, bool anonymous)
    {
        var route = isInternal
            ? $"/internal/files/{_fileId}/preview"
            : $"/api/v1/Files/{_fileId}/preview";
        return SendAsync(HttpMethod.Get, route, authenticationMode: anonymous ? "anonymous" : null);
    }

    private Task UpdateFileAsync(string? description, bool isPublic, bool isSavedInDatabase) =>
        SendAsync(HttpMethod.Put, $"/api/v1/Files/{_fileId}", CreateUpdateContent(description, isPublic, isSavedInDatabase));

    private async Task SendAsync(
        HttpMethod method,
        string uri,
        HttpContent? content = null,
        string? authenticationMode = null)
    {
        _response?.Dispose();
        var request = CreateRequest(method, uri, authenticationMode, content);
        _response = await Client.SendAsync(request);
        request.Dispose();
    }

    private Task SendWithAuthenticationAsync(HttpMethod method, string uri, string mode, HttpContent? content)
    {
        var request = CreateRequest(method, uri, mode, content);
        return SendRequestAsync(request);
    }

    private async Task SendRequestAsync(HttpRequestMessage request)
    {
        _response?.Dispose();
        _response = await Client.SendAsync(request);
        request.Dispose();
    }

    private static HttpRequestMessage CreateRequest(HttpMethod method, string uri, string? mode, HttpContent? content)
    {
        var request = new HttpRequestMessage(method, uri) { Content = content };
        if (mode is not null)
            request.Headers.Add("X-Integration-Test-Auth", mode);
        return request;
    }

    private async Task AssertDownloadedBytesAsync() =>
        Assert.Equal(_fileBytes, await _response!.Content.ReadAsByteArrayAsync());

    private void AssertInlinePreview()
    {
        Assert.Equal("image/jpeg", _response!.Content.Headers.ContentType?.MediaType);
        Assert.Equal("inline", _response.Content.Headers.ContentDisposition?.DispositionType);
    }

    private void AssertStatus(int statusCode) =>
        Assert.Equal((HttpStatusCode)statusCode, _response!.StatusCode);

    private static string GetContentType(string fileName) =>
        Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".pdf" => "application/pdf",
            ".zip" => "application/zip",
            _ => "application/octet-stream"
        };

    private static JsonElement FindProperty(JsonElement element, string propertyName) =>
        element.EnumerateObject()
            .Single(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            .Value;

    private static string ReadString(JsonElement element, string propertyName) => FindProperty(element, propertyName).GetString()!;
    private static bool ReadBoolean(JsonElement element, string propertyName) => FindProperty(element, propertyName).GetBoolean();
    private static Guid ReadGuid(JsonElement element, string propertyName) => FindProperty(element, propertyName).GetGuid();
}
