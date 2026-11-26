namespace CruxSuits.Services.Shared.Localization;

public sealed class LanguageCatalogCacheOptions
{
    public const string SectionName = "Localization:Cache";

    public string RedisKey { get; set; } = "cruxsuits:refdata:v1:languages";

    public TimeSpan LocalCacheTtl { get; set; } = TimeSpan.FromMinutes(2);

    public TimeSpan RedisCacheTtl { get; set; } = TimeSpan.FromDays(1);
}
