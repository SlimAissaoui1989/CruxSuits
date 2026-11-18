using System.Net;
using System.Text;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.Files.Tests.IntegrationTests;

[Collection(FilesServiceIntegrationCollection.Name)]
public sealed class FilesApiIntegrationTests
{
    private readonly FilesServiceIntegrationFixture _fixture;

    public FilesApiIntegrationTests(FilesServiceIntegrationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task UploadDatabaseStoredPublicImage_ThenPreview_ReturnsSavedImageInline()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "integration-photo.jpg",
            isSavedInDatabase: true,
            isPublic: true,
            description: "Integration test image");

        using var request = FilesApiTestHelpers.CreateRequest(
            HttpMethod.Get,
            $"/api/v1/Files/{fileId}/preview",
            authenticationMode: "anonymous");
        using var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("image/jpeg", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal("inline", response.Content.Headers.ContentDisposition?.DispositionType);
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, await response.Content.ReadAsByteArrayAsync());
        Assert.Equal("nosniff", response.Headers.GetValues("X-Content-Type-Options").Single());

        var savedFile = await GetFileAsync(fileId);
        Assert.True(savedFile.IsPublic);
        Assert.True(savedFile.IsSavedInDatabase);
        Assert.Equal("Integration test image", savedFile.Description);
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, savedFile.Content);
        Assert.Null(savedFile.AbsolutePath);
    }

    [Fact]
    public async Task UploadDatabaseStoredFile_ThenPublicDownload_ReturnsOriginalBytesAndName()
    {
        const string fileName = "download-check.jpg";
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            fileName,
            isSavedInDatabase: true,
            isPublic: true);

        using var request = FilesApiTestHelpers.CreateRequest(
            HttpMethod.Get,
            $"/api/v1/Files/{fileId}/download",
            authenticationMode: "anonymous");
        using var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("image/jpeg", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(fileName, response.Content.Headers.ContentDisposition?.FileName?.Trim('"'));
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, await response.Content.ReadAsByteArrayAsync());
    }

    [Fact]
    public async Task UploadDiskStoredFile_SavesBytesOnDiskAndServesPrivateDownload()
    {
        const string fileName = "disk-private.jpg";
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            fileName,
            isSavedInDatabase: false,
            isPublic: false);
        var savedFile = await GetFileAsync(fileId);

        Assert.False(savedFile.IsSavedInDatabase);
        Assert.Null(savedFile.Content);
        Assert.NotNull(savedFile.AbsolutePath);
        Assert.True(File.Exists(savedFile.AbsolutePath));
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, await File.ReadAllBytesAsync(savedFile.AbsolutePath!));
        Assert.Contains("integration-test-user_", Path.GetDirectoryName(savedFile.AbsolutePath)!);

        using var response = await _fixture.Client.GetAsync($"/internal/files/{fileId}/download");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, await response.Content.ReadAsByteArrayAsync());
    }

    [Fact]
    public async Task UploadDiskStoredPublicFile_ThenAnonymousDownload_ReturnsBytes()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "public-disk.jpg",
            isSavedInDatabase: false,
            isPublic: true);

        using var request = FilesApiTestHelpers.CreateRequest(
            HttpMethod.Get,
            $"/api/v1/Files/{fileId}/download",
            authenticationMode: "anonymous");
        using var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, await response.Content.ReadAsByteArrayAsync());
    }

    [Fact]
    public async Task PublicDownload_DoesNotExposePrivateFile()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "private.jpg",
            isSavedInDatabase: true,
            isPublic: false);

        using var response = await _fixture.Client.GetAsync($"/api/v1/Files/{fileId}/download");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PrivateDownload_DoesNotReturnPublicFile()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "public.jpg",
            isSavedInDatabase: true,
            isPublic: true);

        using var response = await _fixture.Client.GetAsync($"/internal/files/{fileId}/download");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Preview_RejectsFileTypeThatCannotBeDisplayedInBrowser()
    {
        var fileId = await UploadAsync(
            [0x01, 0x02, 0x03],
            "archive.zip",
            isSavedInDatabase: true,
            isPublic: true);

        using var request = FilesApiTestHelpers.CreateRequest(
            HttpMethod.Get,
            $"/api/v1/Files/{fileId}/preview",
            authenticationMode: "anonymous");
        using var response = await _fixture.Client.SendAsync(request);

        Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
    }

    [Fact]
    public async Task PrivatePreview_ReturnsNonPublicImageInline()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "private-preview.jpg",
            isSavedInDatabase: true,
            isPublic: false);

        using var response = await _fixture.Client.GetAsync($"/internal/files/{fileId}/preview");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("image/jpeg", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal("inline", response.Content.Headers.ContentDisposition?.DispositionType);
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, await response.Content.ReadAsByteArrayAsync());
    }

    [Fact]
    public async Task PrivatePreview_RejectsFileTypeThatCannotBeDisplayedInBrowser()
    {
        var fileId = await UploadAsync(
            [0x01, 0x02],
            "private-archive.zip",
            isSavedInDatabase: true,
            isPublic: false);

        using var response = await _fixture.Client.GetAsync($"/internal/files/{fileId}/preview");

        Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
    }

    [Fact]
    public async Task Upload_RejectsEmptyFile()
    {
        using var multipart = new MultipartFormDataContent();
        multipart.Add(new ByteArrayContent([]), "File", "empty.jpg");
        multipart.Add(new StringContent("true"), "IsSavedInDatabase");
        multipart.Add(new StringContent("true"), "IsPublic");

        using var response = await _fixture.Client.PostAsync("/api/v1/Files", multipart);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Update_ChangesAllowedMetadataAndPreservesFileContent()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "metadata.jpg",
            isSavedInDatabase: true,
            isPublic: true,
            description: "Before update");

        using var content = FilesApiTestHelpers.CreateUpdateContent(
            "After update",
            isPublic: false,
            isSavedInDatabase: true);
        using var response = await _fixture.Client.PutAsync($"/api/v1/Files/{fileId}", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updatedFile = await GetFileAsync(fileId);
        Assert.Equal("After update", updatedFile.Description);
        Assert.False(updatedFile.IsPublic);
        Assert.True(updatedFile.IsSavedInDatabase);
        Assert.Equal("metadata.jpg", updatedFile.OriginalFileName);
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, updatedFile.Content);
    }

    [Fact]
    public async Task Update_DatabaseToDisk_MovesContentToDiskAfterCommit()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "database-to-disk.jpg",
            isSavedInDatabase: true,
            isPublic: false);

        using var content = FilesApiTestHelpers.CreateUpdateContent(
            "Moved to disk",
            isPublic: false,
            isSavedInDatabase: false);
        using var response = await _fixture.Client.PutAsync($"/api/v1/Files/{fileId}", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updatedFile = await GetFileAsync(fileId);
        Assert.False(updatedFile.IsSavedInDatabase);
        Assert.Null(updatedFile.Content);
        Assert.NotNull(updatedFile.AbsolutePath);
        Assert.True(File.Exists(updatedFile.AbsolutePath));
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, await File.ReadAllBytesAsync(updatedFile.AbsolutePath!));
    }

    [Fact]
    public async Task Update_DiskToDatabase_PersistsContentAndDeletesOldDiskFileAfterCommit()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "disk-to-database.jpg",
            isSavedInDatabase: false,
            isPublic: false);
        var originalFile = await GetFileAsync(fileId);
        var oldPath = originalFile.AbsolutePath!;
        Assert.True(File.Exists(oldPath));

        using var content = FilesApiTestHelpers.CreateUpdateContent(
            "Moved to database",
            isPublic: false,
            isSavedInDatabase: true);
        using var response = await _fixture.Client.PutAsync($"/api/v1/Files/{fileId}", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updatedFile = await GetFileAsync(fileId);
        Assert.True(updatedFile.IsSavedInDatabase);
        Assert.Null(updatedFile.AbsolutePath);
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, updatedFile.Content);
        Assert.False(File.Exists(oldPath));
    }

    [Fact]
    public async Task Update_RejectsAttemptsToChangeFileContent()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "immutable-content.jpg",
            isSavedInDatabase: true,
            isPublic: true);
        using var content = new StringContent(
            "{\"Description\":\"Changed\",\"IsPublic\":true,\"IsSavedInDatabase\":true,\"Content\":\"bmV3" +
            "\"}",
            Encoding.UTF8,
            "application/json");

        using var response = await _fixture.Client.PutAsync($"/api/v1/Files/{fileId}", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(FilesApiTestHelpers.TestImageBytes, (await GetFileAsync(fileId)).Content);
    }

    [Fact]
    public async Task Remove_DatabaseStoredFile_DeletesDatabaseRecord()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "remove-from-database.jpg",
            isSavedInDatabase: true,
            isPublic: true);

        using var response = await _fixture.Client.DeleteAsync($"/api/v1/Files/{fileId}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.False(await FileExistsInDatabaseAsync(fileId));
    }

    [Fact]
    public async Task Remove_DiskStoredFile_DeletesDatabaseRecordAndDiskContent()
    {
        var fileId = await UploadAsync(
            FilesApiTestHelpers.TestImageBytes,
            "remove-from-disk.jpg",
            isSavedInDatabase: false,
            isPublic: false);
        var savedFile = await GetFileAsync(fileId);
        var path = savedFile.AbsolutePath!;
        Assert.True(File.Exists(path));

        using var response = await _fixture.Client.DeleteAsync($"/api/v1/Files/{fileId}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.False(await FileExistsInDatabaseAsync(fileId));
        Assert.False(File.Exists(path));
    }

    [Fact]
    public async Task OperationsForMissingFile_ReturnNotFound()
    {
        var missingId = Guid.NewGuid();

        using var downloadResponse = await _fixture.Client.GetAsync($"/api/v1/Files/{missingId}/download");
        using var privateDownloadResponse = await _fixture.Client.GetAsync($"/internal/files/{missingId}/download");
        using var privatePreviewResponse = await _fixture.Client.GetAsync($"/internal/files/{missingId}/preview");
        using var updateContent = FilesApiTestHelpers.CreateUpdateContent(
            "Missing",
            isPublic: true,
            isSavedInDatabase: true);
        using var updateResponse = await _fixture.Client.PutAsync($"/api/v1/Files/{missingId}", updateContent);
        using var removeResponse = await _fixture.Client.DeleteAsync($"/api/v1/Files/{missingId}");

        Assert.Equal(HttpStatusCode.NotFound, downloadResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, privateDownloadResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, privatePreviewResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, removeResponse.StatusCode);
    }

    [Fact]
    public async Task ServicePolicy_ProtectsUploadUpdateRemoveAndPrivateReadEndpoints()
    {
        var fileId = Guid.NewGuid();
        var protectedRequests = new Func<string, HttpRequestMessage>[]
        {
            mode => FilesApiTestHelpers.CreateRequest(
                HttpMethod.Post,
                "/api/v1/Files",
                mode,
                CreateUploadContent()),
            mode => FilesApiTestHelpers.CreateRequest(
                HttpMethod.Put,
                $"/api/v1/Files/{fileId}",
                mode,
                FilesApiTestHelpers.CreateUpdateContent("Description", true, true)),
            mode => FilesApiTestHelpers.CreateRequest(
                HttpMethod.Delete,
                $"/api/v1/Files/{fileId}",
                mode),
            mode => FilesApiTestHelpers.CreateRequest(
                HttpMethod.Get,
                $"/internal/files/{fileId}/download",
                mode),
            mode => FilesApiTestHelpers.CreateRequest(
                HttpMethod.Get,
                $"/internal/files/{fileId}/preview",
                mode)
        };

        foreach (var createRequest in protectedRequests)
        {
            using var anonymousRequest = createRequest("anonymous");
            using var anonymousResponse = await _fixture.Client.SendAsync(anonymousRequest);
            Assert.Equal(HttpStatusCode.Unauthorized, anonymousResponse.StatusCode);

            using var userRequest = createRequest("user");
            using var userResponse = await _fixture.Client.SendAsync(userRequest);
            Assert.Equal(HttpStatusCode.Forbidden, userResponse.StatusCode);
        }
    }

    private async Task<Guid> UploadAsync(
        byte[] content,
        string fileName,
        bool isSavedInDatabase,
        bool isPublic,
        string? description = null) =>
        await FilesApiTestHelpers.UploadAsync(
            _fixture.Client,
            content,
            fileName,
            isSavedInDatabase,
            isPublic,
            description);

    private async Task<UploadedFile> GetFileAsync(Guid id)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FilesContext>();
        return await dbContext.UploadedFiles.AsNoTracking().SingleAsync(file => file.Id == id);
    }

    private async Task<bool> FileExistsInDatabaseAsync(Guid id)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FilesContext>();
        return await dbContext.UploadedFiles.AnyAsync(file => file.Id == id);
    }

    private static MultipartFormDataContent CreateUploadContent()
    {
        var multipart = new MultipartFormDataContent();
        multipart.Add(new ByteArrayContent(FilesApiTestHelpers.TestImageBytes), "File", "auth-test.jpg");
        multipart.Add(new StringContent("true"), "IsSavedInDatabase");
        multipart.Add(new StringContent("true"), "IsPublic");
        return multipart;
    }
}
