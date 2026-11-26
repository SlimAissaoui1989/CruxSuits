using System.Text.Json;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Localization;

public sealed class LanguageCatalogCacheTests
{
    private const string CacheKey = "cruxsuits:refdata:v1:languages";

    #region GetAsync

    // Success case: verifies a Redis snapshot is cached in local memory and reused;
    // A pass confirms warm requests avoid repeated Redis reads;
    // Failure signals the L1 cache is bypassed.
    [Fact]
    public async Task GetAsync_WhenRedisContainsSnapshot_CachesItInMemory()
    {
        var languages = CreateLanguages("en", "fr");
        var distributedCache = new Mock<IDistributedCache>();
        distributedCache.Setup(cache => cache.GetAsync(CacheKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonSerializer.SerializeToUtf8Bytes(languages));
        using var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var cache = CreateCache(distributedCache.Object, memoryCache);

        var firstResult = await cache.GetAsync();
        var secondResult = await cache.GetAsync();

        Assert.Equal(new[] { "en", "fr" }, firstResult!.Select(language => language.Id));
        Assert.Same(firstResult, secondResult);
        distributedCache.Verify(service => service.GetAsync(CacheKey, It.IsAny<CancellationToken>()), Times.Once);
    }

    // Cache-miss case: verifies GetAsync reports an empty shared cache;
    // A pass lets the ReferenceData provider load the catalog from its repository;
    // Failure means a normal miss is confused with stale data.
    [Fact]
    public async Task GetAsync_WhenRedisMisses_ReturnsNull()
    {
        var distributedCache = new Mock<IDistributedCache>();
        distributedCache.Setup(cache => cache.GetAsync(CacheKey, It.IsAny<CancellationToken>()))
            .ReturnsAsync((byte[]?)null);
        using var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var cache = CreateCache(distributedCache.Object, memoryCache);

        var result = await cache.GetAsync();

        Assert.Null(result);
    }

    // Failure case: verifies GetAsync returns the last known catalog if Redis is unavailable;
    // A pass confirms warm services can keep serving cached languages during a Redis outage;
    // Failure signals a transient Redis failure discards a usable snapshot.
    [Fact]
    public async Task GetAsync_WhenRedisFails_ReturnsLastKnownGoodSnapshot()
    {
        var languages = CreateLanguages("en", "fr");
        var distributedCache = new Mock<IDistributedCache>();
        distributedCache.Setup(cache => cache.SetAsync(
                CacheKey,
                It.IsAny<byte[]>(),
                It.IsAny<DistributedCacheEntryOptions>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        distributedCache.Setup(cache => cache.GetAsync(CacheKey, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Redis unavailable"));
        using var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var cache = CreateCache(
            distributedCache.Object,
            memoryCache,
            localCacheTtl: TimeSpan.Zero);

        await cache.SetAsync(languages);
        var result = await cache.GetAsync();

        Assert.Equal(new[] { "en", "fr" }, result!.Select(language => language.Id));
    }

    // Failure case: verifies a cold Redis outage does not prevent fallback to the configured default;
    // A pass confirms the cache reports no catalog when neither Redis nor a local snapshot is available;
    // Failure signals a cold cache exception leaks into request handling.
    [Fact]
    public async Task GetAsync_WhenRedisFailsWithoutSnapshot_ReturnsNull()
    {
        var distributedCache = new Mock<IDistributedCache>();
        distributedCache.Setup(cache => cache.GetAsync(CacheKey, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Redis unavailable"));
        using var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var cache = CreateCache(distributedCache.Object, memoryCache);

        var result = await cache.GetAsync();

        Assert.Null(result);
    }

    #endregion

    #region SetAsync

    // Success case: verifies SetAsync publishes the complete snapshot and warms local memory;
    // A pass confirms another service can read Redis while this service avoids a second Redis read;
    // Failure signals either tier did not receive the snapshot.
    [Fact]
    public async Task SetAsync_WritesRedisAndWarmsLocalMemory()
    {
        var languages = CreateLanguages("en", "de");
        byte[]? serializedSnapshot = null;
        var distributedCache = new Mock<IDistributedCache>();
        distributedCache.Setup(cache => cache.SetAsync(
                CacheKey,
                It.IsAny<byte[]>(),
                It.IsAny<DistributedCacheEntryOptions>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, byte[], DistributedCacheEntryOptions, CancellationToken>((_, value, _, _) => serializedSnapshot = value)
            .Returns(Task.CompletedTask);
        using var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var cache = CreateCache(distributedCache.Object, memoryCache);

        await cache.SetAsync(languages);
        var result = await cache.GetAsync();

        Assert.NotNull(serializedSnapshot);
        Assert.Equal(new[] { "en", "de" }, JsonSerializer.Deserialize<LanguageResponse[]>(serializedSnapshot!)!.Select(language => language.Id));
        Assert.Equal(new[] { "en", "de" }, result!.Select(language => language.Id));
        distributedCache.Verify(service => service.GetAsync(CacheKey, It.IsAny<CancellationToken>()), Times.Never);
        distributedCache.Verify(service => service.SetAsync(
            CacheKey,
            It.IsAny<byte[]>(),
            It.Is<DistributedCacheEntryOptions>(options => options.AbsoluteExpirationRelativeToNow == TimeSpan.FromDays(1)),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies SetAsync keeps the new snapshot in memory when Redis publication fails;
    // A pass allows the current service to serve newly committed language records during a Redis outage;
    // Failure signals cache availability can make a committed language update unusable locally.
    [Fact]
    public async Task SetAsync_WhenRedisFails_KeepsSnapshotInLocalMemory()
    {
        var languages = CreateLanguages("en", "de");
        var distributedCache = new Mock<IDistributedCache>();
        distributedCache.Setup(cache => cache.SetAsync(
                CacheKey,
                It.IsAny<byte[]>(),
                It.IsAny<DistributedCacheEntryOptions>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Redis unavailable"));
        using var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var cache = CreateCache(distributedCache.Object, memoryCache);

        await cache.SetAsync(languages);
        var result = await cache.GetAsync();

        Assert.Equal(new[] { "en", "de" }, result!.Select(language => language.Id));
    }

    #endregion

    #region TestHelpers

    private static LanguageCatalogCache CreateCache(
        IDistributedCache distributedCache,
        IMemoryCache memoryCache,
        TimeSpan? localCacheTtl = null) =>
        new(
            distributedCache,
            memoryCache,
            Options.Create(new LanguageCatalogCacheOptions
            {
                LocalCacheTtl = localCacheTtl ?? TimeSpan.FromMinutes(2),
                RedisCacheTtl = TimeSpan.FromDays(1)
            }),
            NullLogger<LanguageCatalogCache>.Instance);

    private static IReadOnlyList<LanguageResponse> CreateLanguages(params string[] ids) =>
        ids.Select(id => new LanguageResponse
        {
            Id = id,
            EnglishName = id,
            NativeName = id,
            IsRightToLeft = false
        }).ToArray();

    #endregion
}
