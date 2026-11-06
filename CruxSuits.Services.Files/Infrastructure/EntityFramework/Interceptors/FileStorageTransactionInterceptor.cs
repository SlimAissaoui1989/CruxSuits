using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace CruxSuits.Services.Files.Infrastructure.EntityFramework.Interceptors
{
    public sealed class FileStorageTransactionInterceptor : DbTransactionInterceptor
    {
        private readonly PendingFileStorageOperations _pendingFileStorageOperations;

        public FileStorageTransactionInterceptor(PendingFileStorageOperations pendingFileStorageOperations)
        {
            _pendingFileStorageOperations = pendingFileStorageOperations;
        }

        public override void TransactionCommitted(DbTransaction transaction, TransactionEndEventData eventData)
        {
            if (eventData.Context is not null)
            {
                _pendingFileStorageOperations.Publish(eventData.Context.ContextId.InstanceId);
            }

            base.TransactionCommitted(transaction, eventData);
        }

        public override Task TransactionCommittedAsync(
            DbTransaction transaction,
            TransactionEndEventData eventData,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                _pendingFileStorageOperations.Publish(eventData.Context.ContextId.InstanceId);
            }

            return base.TransactionCommittedAsync(transaction, eventData, cancellationToken);
        }

        public override void TransactionRolledBack(DbTransaction transaction, TransactionEndEventData eventData)
        {
            if (eventData.Context is not null)
            {
                _pendingFileStorageOperations.Rollback(eventData.Context.ContextId.InstanceId);
            }

            base.TransactionRolledBack(transaction, eventData);
        }

        public override Task TransactionRolledBackAsync(
            DbTransaction transaction,
            TransactionEndEventData eventData,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                _pendingFileStorageOperations.Rollback(eventData.Context.ContextId.InstanceId);
            }

            return base.TransactionRolledBackAsync(transaction, eventData, cancellationToken);
        }

        public override void TransactionFailed(DbTransaction transaction, TransactionErrorEventData eventData)
        {
            if (eventData.Context is not null)
            {
                _pendingFileStorageOperations.Rollback(eventData.Context.ContextId.InstanceId);
            }

            base.TransactionFailed(transaction, eventData);
        }

        public override Task TransactionFailedAsync(
            DbTransaction transaction,
            TransactionErrorEventData eventData,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                _pendingFileStorageOperations.Rollback(eventData.Context.ContextId.InstanceId);
            }

            return base.TransactionFailedAsync(transaction, eventData, cancellationToken);
        }
    }
}
