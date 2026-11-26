using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

namespace CruxSuits.Services.Shared.Localization;

public static class LanguageCatalogCacheServiceExtensions
{
    public static WebApplicationBuilder AddCruxSuitsLanguageCatalogCache(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();
        builder.Services.Configure<LanguageCatalogCacheOptions>(
            builder.Configuration.GetSection(LanguageCatalogCacheOptions.SectionName));

        var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
        if (!string.IsNullOrWhiteSpace(redisConnectionString))
        {
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
                options.InstanceName = "CruxSuits:";
            });
        }
        else if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Testing"))
        {
            builder.Services.AddDistributedMemoryCache();
        }
        else
        {
            throw new InvalidOperationException(
                "Connection string 'Redis' is required outside Development and Testing environments.");
        }

        builder.Services.AddSingleton<ILanguageCatalogCache, LanguageCatalogCache>();
        builder.Services.AddScoped<ILanguageRequestResolver, LanguageRequestResolver>();
        return builder;
    }
}
