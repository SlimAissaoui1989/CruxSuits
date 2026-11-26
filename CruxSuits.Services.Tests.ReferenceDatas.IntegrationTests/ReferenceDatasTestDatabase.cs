using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CruxSuits.Services.ReferenceDatas.Tests;

internal static class ReferenceDatasTestDatabase
{
    internal static string GetConnectionString(IConfiguration configuration, string databaseName)
    {
        var provider = configuration["FILES_INTEGRATION_TEST_DATABASE_PROVIDER"];
        var local = string.Equals(provider, "Local", StringComparison.OrdinalIgnoreCase);
        var docker = string.Equals(provider, "Docker", StringComparison.OrdinalIgnoreCase);
        if (!string.IsNullOrWhiteSpace(provider) && !local && !docker)
            throw new InvalidOperationException($"Unsupported test database provider '{provider}'. Use 'Local' or 'Docker'.");

        var connectionString = docker
            ? configuration.GetConnectionString("DefaultConnection")
            : configuration["FILES_INTEGRATION_TEST_CONNECTION_STRING"] ?? configuration.GetConnectionString("DefaultConnection");

        var connection = string.IsNullOrWhiteSpace(connectionString) ? null : new SqlConnectionStringBuilder(connectionString);
        if (connection is null && docker && configuration["FILES_TEST_SQL_PASSWORD"] is { Length: > 0 } password)
        {
            connection = new SqlConnectionStringBuilder
            {
                DataSource = "localhost,14333",
                UserID = "sa",
                Password = password,
                Encrypt = false,
                TrustServerCertificate = true
            };
        }

        if (connection is null)
            throw new InvalidOperationException("Configure FILES_INTEGRATION_TEST_CONNECTION_STRING for local SQL Server, or ConnectionStrings__DefaultConnection or FILES_TEST_SQL_PASSWORD for Docker SQL Server.");

        connection.InitialCatalog = databaseName;
        return connection.ConnectionString;
    }
}
