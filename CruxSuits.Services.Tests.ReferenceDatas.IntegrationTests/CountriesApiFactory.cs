using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

internal sealed class CountriesApiFactory : WebApplicationFactory<ReferenceDataContext>
{
    private readonly string _databaseName = $"ReferenceDataIntegrationTests_{Guid.NewGuid():N}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureLogging(logging => logging.ClearProviders());
        builder.ConfigureAppConfiguration((_, configuration) =>
        {
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Authority"] = "https://identity.test.local",
                ["Jwt:Audience"] = "cruxsuits-reference-data-integration-tests"
            });
        });
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<ReferenceDataContext>>();
            services.RemoveAll<DbContextOptions>();
            services.RemoveAll<IDbContextOptionsConfiguration<ReferenceDataContext>>();
            services.RemoveAll<ReferenceDataContext>();
            services.AddDbContext<ReferenceDataContext>((serviceProvider, options) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var connectionString = ReferenceDatasTestDatabase.GetConnectionString(configuration, _databaseName);
                options.UseSqlServer(connectionString, sqlOptions => sqlOptions.UseCompatibilityLevel(120));
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IntegrationTestAuthenticationHandler.SchemeName;
                    options.DefaultChallengeScheme = IntegrationTestAuthenticationHandler.SchemeName;
                    options.DefaultScheme = IntegrationTestAuthenticationHandler.SchemeName;
                })
                .AddScheme<AuthenticationSchemeOptions, IntegrationTestAuthenticationHandler>(
                    IntegrationTestAuthenticationHandler.SchemeName,
                    _ => { });
        });
    }
}
