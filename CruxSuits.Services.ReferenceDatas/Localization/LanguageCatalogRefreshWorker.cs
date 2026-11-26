using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CruxSuits.Services.ReferenceDatas.Localization;

public sealed class LanguageCatalogRefreshWorker : BackgroundService
{
    private static readonly TimeSpan RefreshInterval = TimeSpan.FromMinutes(2);

    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<LanguageCatalogRefreshWorker> _logger;

    public LanguageCatalogRefreshWorker(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<LanguageCatalogRefreshWorker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        try
        {
            await RefreshCatalogAsync(stoppingToken);

            using var timer = new PeriodicTimer(RefreshInterval);
            while (await timer.WaitForNextTickAsync(stoppingToken))
                await RefreshCatalogAsync(stoppingToken);
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // The host is shutting down.
        }
    }

    private async Task RefreshCatalogAsync(CancellationToken cancellationToken)
    {
        try
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var provider = scope.ServiceProvider.GetRequiredService<ILanguageCatalogProvider>();
            await provider.RefreshAsync(cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Host shutdown interrupts the refresh without indicating a service failure.
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Could not refresh the language catalog; the worker will retry.");
        }
    }
}
