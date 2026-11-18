using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;
using Xunit;

namespace CruxSuits.Services.Files.Tests.IntegrationTests;

public sealed class FilesServiceIntegrationFixture : IAsyncLifetime
{
    private readonly string _databaseName = $"FilesIntegrationTests_{Guid.NewGuid():N}";
    private readonly Dictionary<string, string?> _originalEnvironmentValues = new();
    private readonly MsSqlContainer? _sqlServer;
    private string? _baseConnectionString;
    private bool _databaseCreated;

    private string? _fileStorageRoot;

    internal FilesApiFactory Factory { get; private set; } = null!;
    public HttpClient Client { get; private set; } = null!;

    public FilesServiceIntegrationFixture()
    {
        var provider = Environment.GetEnvironmentVariable(
            "FILES_INTEGRATION_TEST_DATABASE_PROVIDER");

        if (string.Equals(provider, "Docker", StringComparison.OrdinalIgnoreCase))
        {
            _sqlServer = new MsSqlBuilder(
                    "mcr.microsoft.com/mssql/server:2022-latest")
                .Build();
            return;
        }

        if (!string.IsNullOrWhiteSpace(provider)
            && !string.Equals(provider, "Local", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Unsupported integration test database provider '{provider}'. Use 'Local' or 'Docker'.");
        }

        var externalConnectionString = Environment.GetEnvironmentVariable(
            "FILES_INTEGRATION_TEST_CONNECTION_STRING");

        _baseConnectionString = string.IsNullOrWhiteSpace(externalConnectionString)
            ? throw new InvalidOperationException(
                "Set FILES_INTEGRATION_TEST_CONNECTION_STRING in the selected runsettings file to use your local SQL Server, " +
                "or set FILES_INTEGRATION_TEST_DATABASE_PROVIDER to 'Docker'.")
            : externalConnectionString;
    }

    public async Task InitializeAsync()
    {
        if (_sqlServer is not null)
        {
            await _sqlServer.StartAsync();
            _baseConnectionString = _sqlServer.GetConnectionString();
        }

        var databaseConnection = new SqlConnectionStringBuilder(
            _baseConnectionString ?? throw new InvalidOperationException("The SQL Server connection was not initialized."))
        {
            InitialCatalog = "master"
        };

        await using (var sqlConnection = new SqlConnection(databaseConnection.ConnectionString))
        {
            await sqlConnection.OpenAsync();
            await using var createDatabase = sqlConnection.CreateCommand();
            createDatabase.CommandText = $"CREATE DATABASE [{_databaseName}]";
            await createDatabase.ExecuteNonQueryAsync();
            _databaseCreated = true;
        }

        databaseConnection.InitialCatalog = _databaseName;

        _fileStorageRoot = Path.Combine(
            Path.GetTempPath(),
            "CruxSuitsFilesIntegrationTests",
            Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_fileStorageRoot);

        // WebApplicationFactory's host configuration callback may run after Program has
        // already read these settings while registering service defaults.
        SetTestEnvironmentVariable("ConnectionStrings__DefaultConnection", databaseConnection.ConnectionString);
        SetTestEnvironmentVariable("FileStorage__RootPath", _fileStorageRoot);
        SetTestEnvironmentVariable("Jwt__Authority", "https://identity.test.local");
        SetTestEnvironmentVariable("Jwt__Audience", "cruxsuits-files-integration-tests");

        Factory = new FilesApiFactory(databaseConnection.ConnectionString, _fileStorageRoot);
        Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
        Client.DefaultRequestHeaders.Add("current_language", "en");
        Client.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");

        await using var scope = Factory.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FilesContext>();
        await dbContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        Client?.Dispose();
        if (Factory is not null)
            await Factory.DisposeAsync();

        RestoreTestEnvironmentVariables();

        if (_baseConnectionString is not null && _databaseCreated)
        {
            SqlConnection.ClearAllPools();
            var masterConnection = new SqlConnectionStringBuilder(_baseConnectionString)
            {
                InitialCatalog = "master"
            };

            await using var sqlConnection = new SqlConnection(masterConnection.ConnectionString);
            await sqlConnection.OpenAsync();
            await using var dropDatabase = sqlConnection.CreateCommand();
            dropDatabase.CommandText = $"ALTER DATABASE [{_databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{_databaseName}]";
            await dropDatabase.ExecuteNonQueryAsync();
        }

        if (_sqlServer is not null)
            await _sqlServer.DisposeAsync();

        if (_fileStorageRoot is not null && Directory.Exists(_fileStorageRoot))
            Directory.Delete(_fileStorageRoot, recursive: true);
    }

    private void SetTestEnvironmentVariable(string name, string value)
    {
        if (!_originalEnvironmentValues.ContainsKey(name))
            _originalEnvironmentValues[name] = Environment.GetEnvironmentVariable(name);

        Environment.SetEnvironmentVariable(name, value);
    }

    private void RestoreTestEnvironmentVariables()
    {
        foreach (var (name, value) in _originalEnvironmentValues)
            Environment.SetEnvironmentVariable(name, value);
    }
}
