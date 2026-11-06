using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace CruxSuits.Services.Files.Infrastructure.EntityFramework.Interceptors
{
    public sealed class TransactionLogInterceptor : DbTransactionInterceptor
    {
        private readonly ILogger<TransactionLogInterceptor> _logger;

        public TransactionLogInterceptor(ILogger<TransactionLogInterceptor> logger)
        {
            _logger = logger;
        }

        public override ValueTask<InterceptionResult> TransactionCommittingAsync(
            DbTransaction transaction,
            TransactionEventData eventData,
            InterceptionResult result,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "About to commit transaction {TransactionId}",
                eventData.TransactionId);

            return base.TransactionCommittingAsync(transaction, eventData, result, cancellationToken);
        }

        public override Task TransactionCommittedAsync(
            DbTransaction transaction,
            TransactionEndEventData eventData,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Transaction {TransactionId} committed",
                eventData.TransactionId);

            return base.TransactionCommittedAsync(transaction, eventData, cancellationToken);
        }

        public override Task TransactionRolledBackAsync(
            DbTransaction transaction,
            TransactionEndEventData eventData,
            CancellationToken cancellationToken = default)
        {
            _logger.LogWarning(
                "Transaction {TransactionId} rolled back",
                eventData.TransactionId);

            return base.TransactionRolledBackAsync(transaction, eventData, cancellationToken);
        }

        public override Task TransactionFailedAsync(
            DbTransaction transaction,
            TransactionErrorEventData eventData,
            CancellationToken cancellationToken = default)
        {
            _logger.LogError(
                "Transaction {TransactionId} failed",
                eventData.TransactionId);

            return base.TransactionFailedAsync(transaction, eventData, cancellationToken);
        }
    }
}
