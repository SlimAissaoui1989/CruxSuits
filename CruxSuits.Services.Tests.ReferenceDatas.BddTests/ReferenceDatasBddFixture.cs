using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Localization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests;

public sealed class ReferenceDatasBddFixture : IAsyncLifetime
{
    private readonly Dictionary<string, string?> _originalEnvironmentValues = new();
    private bool _databaseCreated;
    internal CountriesBddApiFactory Factory { get; private set; } = null!;
    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        SetTestEnvironmentVariable("Jwt__Authority", "https://identity.test.local");
        SetTestEnvironmentVariable("Jwt__Audience", "cruxsuits-reference-data-bdd-tests");
        Factory = new CountriesBddApiFactory();
        Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });

        await using var scope = Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        await context.Database.EnsureCreatedAsync();
        _databaseCreated = true;

        var languageCatalogProvider = scope.ServiceProvider.GetRequiredService<ILanguageCatalogProvider>();
        await languageCatalogProvider.RefreshAsync();
    }

    public async Task DisposeAsync()
    {
        Client?.Dispose();
        try
        {
            if (Factory is not null)
            {
                try
                {
                    if (_databaseCreated)
                    {
                        await using var scope = Factory.Services.CreateAsyncScope();
                        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
                        await context.Database.EnsureDeletedAsync();
                    }
                }
                finally
                {
                    await Factory.DisposeAsync();
                }
            }
        }
        finally
        {
            foreach (var (name, value) in _originalEnvironmentValues)
                Environment.SetEnvironmentVariable(name, value);
        }
    }

    private void SetTestEnvironmentVariable(string name, string value)
    {
        _originalEnvironmentValues[name] = Environment.GetEnvironmentVariable(name);
        Environment.SetEnvironmentVariable(name, value);
    }
}
