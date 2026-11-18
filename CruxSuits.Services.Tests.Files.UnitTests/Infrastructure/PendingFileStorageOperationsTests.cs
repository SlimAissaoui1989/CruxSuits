using CruxSuits.Services.Files.Infrastructure.EntityFramework.Interceptors;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace CruxSuits.Services.Files.Tests.UnitTests.Infrastructure;

public sealed class PendingFileStorageOperationsTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), "CruxSuitsFilesTests", Guid.NewGuid().ToString("N"));
    private readonly PendingFileStorageOperations _operations = new(NullLogger<PendingFileStorageOperations>.Instance);

    [Fact]
    public void Publish_MovesStagedUploadToDestination()
    {
        Directory.CreateDirectory(_directory);
        var temporaryPath = Path.Combine(_directory, "file.pending");
        var destinationPath = Path.Combine(_directory, "file.jpg");
        File.WriteAllBytes(temporaryPath, [7, 8, 9]);
        var contextId = Guid.NewGuid();
        _operations.Register(contextId, temporaryPath, destinationPath);

        _operations.Publish(contextId);

        Assert.False(File.Exists(temporaryPath));
        Assert.Equal(new byte[] { 7, 8, 9 }, File.ReadAllBytes(destinationPath));
    }

    [Fact]
    public void Rollback_DeletesStagedUploadAndCancelsPendingDelete()
    {
        Directory.CreateDirectory(_directory);
        var temporaryPath = Path.Combine(_directory, "file.pending");
        var existingPath = Path.Combine(_directory, "existing.jpg");
        File.WriteAllBytes(temporaryPath, [1]);
        File.WriteAllBytes(existingPath, [2]);
        var contextId = Guid.NewGuid();
        _operations.Register(contextId, temporaryPath, Path.Combine(_directory, "final.jpg"));
        _operations.RegisterDeleteOnCommit(contextId, existingPath);

        _operations.Rollback(contextId);

        Assert.False(File.Exists(temporaryPath));
        Assert.True(File.Exists(existingPath));
    }

    [Fact]
    public void Publish_DeletesFileScheduledForDeletionAfterCommit()
    {
        Directory.CreateDirectory(_directory);
        var existingPath = Path.Combine(_directory, "existing.jpg");
        File.WriteAllBytes(existingPath, [2]);
        var contextId = Guid.NewGuid();
        _operations.RegisterDeleteOnCommit(contextId, existingPath);

        _operations.Publish(contextId);

        Assert.False(File.Exists(existingPath));
    }

    public void Dispose()
    {
        if (Directory.Exists(_directory))
            Directory.Delete(_directory, recursive: true);
    }
}
