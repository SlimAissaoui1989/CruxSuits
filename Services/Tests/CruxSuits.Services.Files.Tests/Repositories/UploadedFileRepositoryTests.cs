using CruxIT.Library.DataToolkit.EntityFramework.Enums;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Interceptors;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.Files.Tests.Repositories;

public sealed class UploadedFileRepositoryTests
{
    [Fact]
    public async Task AddAsync_RequiresContent()
    {
        using var fixture = new RepositoryFixture();
        var entity = RepositoryFixture.CreateEntity(content: null, isPublic: true, isSavedInDatabase: true);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => fixture.Repository.AddAsync(entity));

        Assert.Equal("File content is required.", exception.Message);
    }

    [Fact]
    public async Task AddAsync_WhenDatabaseStorageIsSelected_SetsMetadataAndTracksEntity()
    {
        using var fixture = new RepositoryFixture();
        var content = new byte[] { 1, 2, 3, 4 };
        var entity = RepositoryFixture.CreateEntity(content, isPublic: true, isSavedInDatabase: true);

        var result = await fixture.Repository.AddAsync(entity);

        Assert.Same(entity, result);
        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.Equal("image/jpeg", entity.FileType);
        Assert.Equal(".jpg", entity.Extension);
        Assert.Equal(content.LongLength, entity.SizeInBytes);
        Assert.Equal(content, entity.Content);
        Assert.Null(entity.AbsolutePath);
        Assert.Equal(EntityState.Added, fixture.Context.Entry(entity).State);
    }

    [Fact]
    public async Task AddAsync_WhenDiskStorageIsSelected_StagesContentAndClearsDatabaseContent()
    {
        using var fixture = new RepositoryFixture();
        var content = new byte[] { 3, 4, 5 };
        var entity = RepositoryFixture.CreateEntity(content, isPublic: true, isSavedInDatabase: false);

        await fixture.Repository.AddAsync(entity);

        Assert.Null(entity.Content);
        Assert.Equal(content.LongLength, entity.SizeInBytes);
        Assert.NotNull(entity.AbsolutePath);
        Assert.Contains("test-user_", entity.AbsolutePath, StringComparison.Ordinal);
        Assert.EndsWith(".jpg.pending", Directory.GetFiles(Path.GetDirectoryName(entity.AbsolutePath)!).Single());
        Assert.False(File.Exists(entity.AbsolutePath));

        await fixture.Context.SaveChangesAsync();
        fixture.PendingOperations.Publish(fixture.Context.ContextId.InstanceId);

        Assert.True(File.Exists(entity.AbsolutePath));
        Assert.Equal(content, await File.ReadAllBytesAsync(entity.AbsolutePath));
    }

    [Fact]
    public async Task AddAsync_WhenDiskStagingIsCancelled_RestoresEntityContent()
    {
        using var fixture = new RepositoryFixture();
        var content = new byte[] { 9, 8, 7 };
        var entity = RepositoryFixture.CreateEntity(content, isPublic: true, isSavedInDatabase: false);
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => fixture.Repository.AddAsync(entity, cancellation.Token));

        Assert.Equal(content, entity.Content);
        Assert.Null(entity.AbsolutePath);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesOnlyDescriptionAndPublicFlagWhenStorageDoesNotChange()
    {
        using var fixture = new RepositoryFixture();
        var stored = RepositoryFixture.CreateEntity([1, 2, 3], isPublic: true, isSavedInDatabase: true);
        stored.Id = Guid.NewGuid();
        stored.OriginalFileName = "original.jpg";
        fixture.Context.UploadedFiles.Add(stored);
        await fixture.Context.SaveChangesAsync();

        var update = RepositoryFixture.CreateEntity([7, 7, 7], isPublic: true, isSavedInDatabase: true);
        update.Id = stored.Id;
        update.OriginalFileName = "replacement.exe";
        update.Description = "Updated";
        update.IsPublic = true;

        var result = await fixture.Repository.UpdateAsync(update);

        Assert.Same(stored, result);
        Assert.Equal("Updated", stored.Description);
        Assert.True(stored.IsPublic);
        Assert.Equal("original.jpg", stored.OriginalFileName);
        Assert.Equal("image/jpeg", stored.FileType);
        Assert.Equal(new byte[] { 1, 2, 3 }, stored.Content);
    }

    [Fact]
    public async Task UpdateAsync_WhenMovingDiskFileToDatabase_QueuesDiskDeletionUntilPublish()
    {
        using var fixture = new RepositoryFixture();
        var oldDiskFile = fixture.CreateStoredDiskFile([4, 5, 6], isPublic: true);
        fixture.Context.UploadedFiles.Add(oldDiskFile);
        await fixture.Context.SaveChangesAsync();
        var oldPath = oldDiskFile.AbsolutePath!;
        var update = RepositoryFixture.CreateEntity(null, isPublic: true, isSavedInDatabase: true);
        update.Id = oldDiskFile.Id;
        update.Description = "Moved to database";

        var result = await fixture.Repository.UpdateAsync(update);

        Assert.Same(oldDiskFile, result);
        Assert.True(oldDiskFile.IsSavedInDatabase);
        Assert.Null(oldDiskFile.AbsolutePath);
        Assert.Equal(new byte[] { 4, 5, 6 }, oldDiskFile.Content);
        Assert.True(File.Exists(oldPath));

        await fixture.Context.SaveChangesAsync();
        fixture.PendingOperations.Publish(fixture.Context.ContextId.InstanceId);

        Assert.False(File.Exists(oldPath));
    }

    [Fact]
    public async Task UpdateAsync_WhenMovingDatabaseFileToDisk_StagesAndPublishesContent()
    {
        using var fixture = new RepositoryFixture();
        var existing = RepositoryFixture.CreateEntity([2, 4, 6], isPublic: true, isSavedInDatabase: true);
        existing.Id = Guid.NewGuid();
        fixture.Context.UploadedFiles.Add(existing);
        await fixture.Context.SaveChangesAsync();
        var update = RepositoryFixture.CreateEntity(null, isPublic: true, isSavedInDatabase: false);
        update.Id = existing.Id;
        update.Description = "Stored on disk";

        await fixture.Repository.UpdateAsync(update);

        Assert.False(existing.IsSavedInDatabase);
        Assert.Null(existing.Content);
        Assert.NotNull(existing.AbsolutePath);
        Assert.True(File.Exists(existing.AbsolutePath + ".pending"));

        await fixture.Context.SaveChangesAsync();
        fixture.PendingOperations.Publish(fixture.Context.ContextId.InstanceId);

        Assert.True(File.Exists(existing.AbsolutePath));
        Assert.Equal(new byte[] { 2, 4, 6 }, await File.ReadAllBytesAsync(existing.AbsolutePath));
    }

    [Fact]
    public async Task UpdateAsync_WhenTargetIsMissing_ThrowsNotFound()
    {
        using var fixture = new RepositoryFixture();
        var missing = RepositoryFixture.CreateEntity([1], isPublic: true, isSavedInDatabase: true);
        missing.Id = Guid.NewGuid();

        await Assert.ThrowsAsync<CxException>(() => fixture.Repository.UpdateAsync(missing));
    }

    [Fact]
    public async Task UpdateAsync_WhenDiskContentIsMissing_ThrowsAndDoesNotChangeStorageFlag()
    {
        using var fixture = new RepositoryFixture();
        var existing = fixture.CreateStoredDiskFile([1], isPublic: true);
        fixture.Context.UploadedFiles.Add(existing);
        await fixture.Context.SaveChangesAsync();
        File.Delete(existing.AbsolutePath!);
        var update = RepositoryFixture.CreateEntity(null, isPublic: true, isSavedInDatabase: true);
        update.Id = existing.Id;

        await Assert.ThrowsAsync<InvalidOperationException>(() => fixture.Repository.UpdateAsync(update));

        Assert.False(existing.IsSavedInDatabase);
    }

    [Fact]
    public async Task RemoveAsync_WhenFileIsStoredInDatabase_RemovesRecordWithoutDiskWork()
    {
        using var fixture = new RepositoryFixture();
        var entity = RepositoryFixture.CreateEntity([1], isPublic: true, isSavedInDatabase: true);
        entity.Id = Guid.NewGuid();
        fixture.Context.UploadedFiles.Add(entity);
        await fixture.Context.SaveChangesAsync();

        await fixture.Repository.RemoveAsync(entity);

        Assert.Equal(EntityState.Deleted, fixture.Context.Entry(entity).State);
        Assert.Empty(Directory.GetFiles(fixture.RootPath, "*", SearchOption.AllDirectories));
    }

    [Fact]
    public async Task RemoveAsync_WhenFileIsOnDisk_DeletesFileOnlyAfterPublish()
    {
        using var fixture = new RepositoryFixture();
        var entity = fixture.CreateStoredDiskFile([1, 3, 5], isPublic: true);
        fixture.Context.UploadedFiles.Add(entity);
        await fixture.Context.SaveChangesAsync();
        var path = entity.AbsolutePath!;

        await fixture.Repository.RemoveAsync(entity);

        Assert.Equal(EntityState.Deleted, fixture.Context.Entry(entity).State);
        Assert.True(File.Exists(path));
        fixture.PendingOperations.Publish(fixture.Context.ContextId.InstanceId);
        Assert.False(File.Exists(path));
    }

    [Fact]
    public async Task RemoveAsync_WhenTargetIsMissing_ThrowsNotFound()
    {
        using var fixture = new RepositoryFixture();
        var missing = RepositoryFixture.CreateEntity([1], isPublic: true, isSavedInDatabase: true);
        missing.Id = Guid.NewGuid();

        await Assert.ThrowsAsync<CxException>(() => fixture.Repository.RemoveAsync(missing));
    }

    [Fact]
    public async Task GetPublicForDownloadAsync_ReturnsPublicFileAndContent()
    {
        using var fixture = new RepositoryFixture();
        var publicFile = RepositoryFixture.CreateEntity([2, 3], isPublic: true, isSavedInDatabase: true);
        publicFile.Id = Guid.NewGuid();
        publicFile.IsPublic = true;
        var privateFile = RepositoryFixture.CreateEntity([4], isPublic: true, isSavedInDatabase: true);
        privateFile.Id = Guid.NewGuid();
        privateFile.IsPublic = false;
        fixture.Context.UploadedFiles.AddRange(publicFile, privateFile);
        await fixture.Context.SaveChangesAsync();

        var result = await fixture.Repository.GetPublicForDownloadAsync(publicFile.Id);
        var denied = await fixture.Repository.GetPublicForDownloadAsync(privateFile.Id);

        Assert.NotNull(result);
        Assert.Equal(new byte[] { 2, 3 }, result.Content);
        Assert.Null(denied);
    }

    [Fact]
    public async Task GetPrivateForDownloadAsync_ReturnsPrivateFileAndContent()
    {
        using var fixture = new RepositoryFixture();
        var privateFile = RepositoryFixture.CreateEntity([8, 9], isPublic: true, isSavedInDatabase: true);
        privateFile.Id = Guid.NewGuid();
        privateFile.IsPublic = false;
        fixture.Context.UploadedFiles.Add(privateFile);
        await fixture.Context.SaveChangesAsync();

        var result = await fixture.Repository.GetPrivateForDownloadAsync(privateFile.Id);

        Assert.NotNull(result);
        Assert.False(result.IsPublic);
        Assert.Equal(new byte[] { 8, 9 }, result.Content);
    }

    [Fact]
    public async Task DownloadQueries_ReadDiskContentAndRejectMissingStorageContent()
    {
        using var fixture = new RepositoryFixture();
        var diskFile = fixture.CreateStoredDiskFile([3, 6, 9], isPublic: true);
        diskFile.IsPublic = true;
        fixture.Context.UploadedFiles.Add(diskFile);
        await fixture.Context.SaveChangesAsync();

        var result = await fixture.Repository.GetPublicForDownloadAsync(diskFile.Id);

        Assert.Equal(new byte[] { 3, 6, 9 }, result!.Content);

        var brokenDatabaseFile = RepositoryFixture.CreateEntity(null, isPublic: true, isSavedInDatabase: true);
        brokenDatabaseFile.Id = Guid.NewGuid();
        fixture.Context.UploadedFiles.Add(brokenDatabaseFile);
        await fixture.Context.SaveChangesAsync();
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => fixture.Repository.GetPublicForDownloadAsync(brokenDatabaseFile.Id));
    }

    [Fact]
    public async Task DownloadQueries_RejectDiskFileWithMissingPathOrMissingPhysicalFile()
    {
        using var fixture = new RepositoryFixture();
        var missingPath = fixture.CreateStoredDiskFile([1], isPublic: false);
        missingPath.Id = Guid.NewGuid();
        missingPath.AbsolutePath = null;
        fixture.Context.UploadedFiles.Add(missingPath);
        var missingFile = fixture.CreateStoredDiskFile([2], isPublic: false);
        missingFile.Id = Guid.NewGuid();
        File.Delete(missingFile.AbsolutePath!);
        fixture.Context.UploadedFiles.Add(missingFile);
        await fixture.Context.SaveChangesAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => fixture.Repository.GetPrivateForDownloadAsync(missingPath.Id));
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => fixture.Repository.GetPrivateForDownloadAsync(missingFile.Id));
    }

    [Fact]
    public async Task GetAllAsync_IsNotImplemented()
    {
        using var fixture = new RepositoryFixture();

        await Assert.ThrowsAsync<NotImplementedException>(() => fixture.Repository.GetAllAsync());
    }
}
