using System.Text.Json;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CruxSuits.Services.Shared.Localization;

public sealed class LanguageCatalogCache : ILanguageCatalogCache
{
    private const string LastKnownGoodSuffix = ":last-known-good";
    private static readonly JsonSerializerOptions SerializerOptions = new();

    private readonly IDistributedCache _distributedCache;
    private readonly IMemoryCache _memoryCache;
    private readonly LanguageCatalogCacheOptions _options;
    private readonly ILogger<LanguageCatalogCache> _logger;

    public LanguageCatalogCache(
        IDistributedCache distributedCache,
        IMemoryCache memoryCache,
        IOptions<LanguageCatalogCacheOptions> options,
        ILogger<LanguageCatalogCache> logger)
    {
        _distributedCache = distributedCache;
        _memoryCache = memoryCache;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<IReadOnlyList<LanguageResponse>?> GetAsync(CancellationToken cancellationToken = default)
    {
        if (_memoryCache.TryGetValue(_options.RedisKey, out IReadOnlyList<LanguageResponse>? cachedLanguages))
            return cachedLanguages;

        try
        {
            var snapshot = await _distributedCache.GetAsync(_options.RedisKey, cancellationToken);
            if (snapshot is null)
                return null;

            var languages = JsonSerializer.Deserialize<LanguageResponse[]>(snapshot, SerializerOptions);
            if (languages is null)
                return null;

            CacheLocally(languages);
            return languages;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Could not read the language catalog from the distributed cache.");
            return GetLastKnownGood();
        }
    }

    public async Task SetAsync(
        IReadOnlyList<LanguageResponse> languages,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(languages);

        var snapshot = languages.ToArray();
        CacheLocally(snapshot);

        try
        {
            var serializedSnapshot = JsonSerializer.SerializeToUtf8Bytes(snapshot, SerializerOptions);
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _options.RedisCacheTtl
            };

            await _distributedCache.SetAsync(
                _options.RedisKey,
                serializedSnapshot,
                cacheOptions,
                cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning("Publishing the language catalog was cancelled; the local snapshot remains available.");
        }
        catch (Exception exception)
        {
            _logger.LogWarning(exception, "Could not publish the language catalog to the distributed cache.");
        }
    }

    private IReadOnlyList<LanguageResponse>? GetLastKnownGood() =>
        _memoryCache.TryGetValue(_options.RedisKey + LastKnownGoodSuffix, out IReadOnlyList<LanguageResponse>? languages)
            ? languages
            : null;

    private void CacheLocally(IReadOnlyList<LanguageResponse> languages)
    {
        if (_options.LocalCacheTtl > TimeSpan.Zero)
            _memoryCache.Set(_options.RedisKey, languages, _options.LocalCacheTtl);
        else
            _memoryCache.Remove(_options.RedisKey);

        _memoryCache.Set(_options.RedisKey + LastKnownGoodSuffix, languages);
    }
}
