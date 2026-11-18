using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CruxSuits.Services.Files.Tests.IntegrationTests;

internal sealed class FilesApiFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;
    private readonly string _fileStorageRoot;

    public FilesApiFactory(string connectionString, string fileStorageRoot)
    {
        _connectionString = connectionString;
        _fileStorageRoot = fileStorageRoot;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((_, configuration) =>
        {
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = _connectionString,
                ["FileStorage:RootPath"] = _fileStorageRoot,
                ["Jwt:Authority"] = "https://identity.test.local",
                ["Jwt:Audience"] = "cruxsuits-files-integration-tests"
            });
        });

        builder.ConfigureTestServices(services =>
        {
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
