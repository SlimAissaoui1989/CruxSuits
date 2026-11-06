using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace CruxSuits.Services.Files.Infrastructure.EntityFramework.Interceptors
{
    public sealed class PendingFileStorageOperations
    {
        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<string, string>> _operations = new();
        private readonly ConcurrentDictionary<Guid, ConcurrentDictionary<string, byte>> _deleteOnCommit = new();
        private readonly ILogger<PendingFileStorageOperations> _logger;

        public PendingFileStorageOperations(ILogger<PendingFileStorageOperations> logger)
        {
            _logger = logger;
        }

        public void Register(Guid contextId, string temporaryPath, string finalPath)
        {
            var contextOperations = _operations.GetOrAdd(
                contextId,
                _ => new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase));

            contextOperations[temporaryPath] = finalPath;
        }

        public void Remove(Guid contextId, string temporaryPath)
        {
            if (!_operations.TryGetValue(contextId, out var contextOperations))
            {
                DeleteIfExists(temporaryPath);
                return;
            }

            contextOperations.TryRemove(temporaryPath, out _);
            DeleteIfExists(temporaryPath);

            if (contextOperations.IsEmpty)
            {
                _operations.TryRemove(contextId, out _);
            }
        }

        public void RegisterDeleteOnCommit(Guid contextId, string path)
        {
            var contextFiles = _deleteOnCommit.GetOrAdd(
                contextId,
                _ => new ConcurrentDictionary<string, byte>(StringComparer.OrdinalIgnoreCase));
            contextFiles[path] = 0;
        }

        public void CancelDeleteOnCommit(Guid contextId, string path)
        {
            if (!_deleteOnCommit.TryGetValue(contextId, out var contextFiles))
                return;

            contextFiles.TryRemove(path, out _);
            if (contextFiles.IsEmpty)
                _deleteOnCommit.TryRemove(contextId, out _);
        }

        public void Publish(Guid contextId)
        {
            var publishSucceeded = true;
            if (_operations.TryGetValue(contextId, out var contextOperations))
            {
                foreach (var operation in contextOperations)
                {
                    try
                    {
                        if (!System.IO.File.Exists(operation.Key))
                        {
                            throw new FileNotFoundException("The staged upload file is missing.", operation.Key);
                        }

                        System.IO.File.Move(operation.Key, operation.Value);
                        contextOperations.TryRemove(operation.Key, out _);
                    }
                    catch (Exception exception)
                    {
                        publishSucceeded = false;
                        _logger.LogError(
                            exception,
                            "Could not publish staged upload {TemporaryPath} to {FinalPath} after transaction commit.",
                            operation.Key,
                            operation.Value);
                    }
                }
            }

            if (publishSucceeded && _deleteOnCommit.TryRemove(contextId, out var filesToDelete))
            {
                foreach (var path in filesToDelete.Keys)
                {
                    DeleteIfExists(path);
                }
            }

            if (_operations.TryGetValue(contextId, out contextOperations) && contextOperations.IsEmpty)
            {
                _operations.TryRemove(contextId, out _);
            }
        }

        public void Rollback(Guid contextId)
        {
            if (!_operations.TryRemove(contextId, out var contextOperations))
            {
                _deleteOnCommit.TryRemove(contextId, out _);
                return;
            }

            foreach (var temporaryPath in contextOperations.Keys)
            {
                DeleteIfExists(temporaryPath);
            }

            _deleteOnCommit.TryRemove(contextId, out _);
        }

        private void DeleteIfExists(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "Could not remove staged upload file at {FilePath}.", path);
            }
        }
    }
}
