using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Xunit;

namespace CruxSuits.Services.Files.Tests.IntegrationTests;

internal static class FilesApiTestHelpers
{
    public static readonly byte[] TestImageBytes =
    [
        0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46,
        0x49, 0x46, 0x00, 0x01, 0x01, 0x00, 0x00, 0x01
    ];

    public static async Task<Guid> UploadAsync(
        HttpClient client,
        byte[] content,
        string fileName,
        bool isSavedInDatabase,
        bool isPublic,
        string? description = null)
    {
        using var multipart = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(content);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(fileName));
        multipart.Add(fileContent, "File", fileName);
        multipart.Add(new StringContent(isSavedInDatabase.ToString()), "IsSavedInDatabase");
        multipart.Add(new StringContent(isPublic.ToString()), "IsPublic");
        if (description is not null)
            multipart.Add(new StringContent(description), "Description");

        using var response = await client.PostAsync("/api/v1/Files", multipart);
        var responseBody = await response.Content.ReadAsStringAsync();
        Assert.True(
            response.StatusCode == HttpStatusCode.Created,
            $"Expected upload to return 201 Created but received {(int)response.StatusCode}: {responseBody}");

        using var json = JsonDocument.Parse(responseBody);
        return json.RootElement.GetProperty("Id").GetGuid();
    }

    public static HttpRequestMessage CreateRequest(
        HttpMethod method,
        string uri,
        string? authenticationMode = null,
        HttpContent? content = null)
    {
        var request = new HttpRequestMessage(method, uri) { Content = content };
        if (authenticationMode is not null)
        {
            request.Headers.Add(
                IntegrationTestAuthenticationHandler.AuthenticationModeHeader,
                authenticationMode);
        }

        return request;
    }

    public static StringContent CreateUpdateContent(
        string? description,
        bool isPublic,
        bool isSavedInDatabase)
    {
        var payload = JsonSerializer.Serialize(new
        {
            Description = description,
            IsPublic = isPublic,
            IsSavedInDatabase = isSavedInDatabase
        });

        return new StringContent(payload, Encoding.UTF8, "application/json");
    }

    private static string GetContentType(string fileName) =>
        Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".pdf" => "application/pdf",
            ".zip" => "application/zip",
            _ => "application/octet-stream"
        };
}
