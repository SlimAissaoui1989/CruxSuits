using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests;

internal sealed class CountriesBddApiFactory : WebApplicationFactory<ReferenceDataContext>
{
    internal string DatabaseName { get; } = $"ReferenceDataBddTests_{Guid.NewGuid():N}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureLogging(logging => logging.ClearProviders());
        builder.ConfigureAppConfiguration((_, configuration) =>
        {
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Authority"] = "https://identity.test.local",
                ["Jwt:Audience"] = "cruxsuits-reference-data-bdd-tests"
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
                var configuredConnectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection must be configured for the ReferenceDatas BDD tests.");
                var testConnectionString = new SqlConnectionStringBuilder(configuredConnectionString)
                {
                    InitialCatalog = DatabaseName
                }.ConnectionString;

                options.UseSqlServer(testConnectionString, sqlOptions => sqlOptions.UseCompatibilityLevel(120));
            });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = BddTestAuthenticationHandler.SchemeName;
                    options.DefaultChallengeScheme = BddTestAuthenticationHandler.SchemeName;
                    options.DefaultScheme = BddTestAuthenticationHandler.SchemeName;
                })
                .AddScheme<AuthenticationSchemeOptions, BddTestAuthenticationHandler>(
                    BddTestAuthenticationHandler.SchemeName,
                    _ => { });
        });
    }
}
