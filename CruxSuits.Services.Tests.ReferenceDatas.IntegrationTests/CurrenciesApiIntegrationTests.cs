using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[Collection(ReferenceDatasIntegrationCollection.Name)]
public sealed class CurrenciesApiIntegrationTests
{
    private static readonly JsonSerializerOptions RequestJsonOptions = new() { PropertyNamingPolicy = null };

    private readonly ReferenceDatasIntegrationFixture _fixture;

    public CurrenciesApiIntegrationTests(ReferenceDatasIntegrationFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Create_CreatesCurrencyAndReturnsCreatedAtGetById()
    {
        var (currencyId, numericCode) = await GetUniqueCurrencyIdentityAsync();
        var request = new CurrencyRequest
        {
            Id = currencyId,
            NumericCode = numericCode,
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };

        using var response = await _fixture.Client.PostAsJsonAsync("/api/v1/Currencies", request, RequestJsonOptions);

        Assert.True(response.StatusCode == HttpStatusCode.Created,
            await response.Content.ReadAsStringAsync());
        var created = await response.Content.ReadFromJsonAsync<CurrencyResponse>();
        Assert.NotNull(created);
        Assert.Equal(currencyId, created.Id);
        Assert.Equal(numericCode, created.NumericCode);

        using var getResponse = await _fixture.Client.GetAsync($"/api/v1/Currencies/{currencyId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var currency = await getResponse.Content.ReadFromJsonAsync<CurrencyDetailsResponse>();
        Assert.NotNull(currency);
        Assert.Equal(currencyId, currency.Id);
        Assert.Equal(numericCode, currency.NumericCode);
        Assert.Equal("¤", currency.Symbol);
        Assert.Equal('.', currency.DecimalSeparator);
        Assert.Equal((byte)2, currency.MinorUnit);
    }

    [Fact]
    public async Task Create_ReturnsConflictWhenCurrencyIdAlreadyExists()
    {
        var (_, numericCode) = await GetUniqueCurrencyIdentityAsync();
        var request = new CurrencyRequest
        {
            Id = "USD",
            NumericCode = numericCode,
            Symbol = "$",
            DecimalSeparator = '.',
            MinorUnit = 2
        };

        using var response = await _fixture.Client.PostAsJsonAsync("/api/v1/Currencies", request, RequestJsonOptions);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsConflictWhenNumericCodeAlreadyExists()
    {
        var (currencyId, _) = await GetUniqueCurrencyIdentityAsync();
        var request = new CurrencyRequest
        {
            Id = currencyId,
            NumericCode = "840",
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };

        using var response = await _fixture.Client.PostAsJsonAsync("/api/v1/Currencies", request, RequestJsonOptions);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Update_UpdatesCurrencyFieldsAndReturnsDetails()
    {
        var (currencyId, currentNumericCode) = await GetUniqueCurrencyIdentityAsync();
        var (_, updatedNumericCode) = await GetUniqueCurrencyIdentityAsync(new[] { currentNumericCode });
        var createRequest = new CurrencyRequest
        {
            Id = currencyId,
            NumericCode = currentNumericCode,
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        using var createResponse = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Currencies", createRequest, RequestJsonOptions);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var updateRequest = new UpdateCurrencyRequest
        {
            NumericCode = updatedNumericCode,
            Symbol = "Q",
            DecimalSeparator = ',',
            MinorUnit = 0
        };
        using var response = await _fixture.Client.PutAsJsonAsync(
            $"/api/v1/Currencies/{currencyId}", updateRequest, RequestJsonOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updated = await response.Content.ReadFromJsonAsync<CurrencyDetailsResponse>();
        Assert.NotNull(updated);
        Assert.Equal(currencyId, updated.Id);
        Assert.Equal(updatedNumericCode, updated.NumericCode);
        Assert.Equal("Q", updated.Symbol);
        Assert.Equal(',', updated.DecimalSeparator);
        Assert.Equal((byte)0, updated.MinorUnit);
    }

    [Fact]
    public async Task Update_ReturnsNotFoundWhenCurrencyDoesNotExist()
    {
        var currencyId = await GetUniqueCurrencyIdAsync();
        var request = new UpdateCurrencyRequest
        {
            NumericCode = "996",
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };

        using var response = await _fixture.Client.PutAsJsonAsync(
            $"/api/v1/Currencies/{currencyId}", request, RequestJsonOptions);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsConflictWhenNumericCodeAlreadyExists()
    {
        var (currencyId, numericCode) = await GetUniqueCurrencyIdentityAsync();
        var createRequest = new CurrencyRequest
        {
            Id = currencyId,
            NumericCode = numericCode,
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        using var createResponse = await _fixture.Client.PostAsJsonAsync(
            "/api/v1/Currencies", createRequest, RequestJsonOptions);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var updateRequest = new UpdateCurrencyRequest
        {
            NumericCode = "840",
            Symbol = "Q",
            DecimalSeparator = ',',
            MinorUnit = 0
        };
        using var response = await _fixture.Client.PutAsJsonAsync(
            $"/api/v1/Currencies/{currencyId}", updateRequest, RequestJsonOptions);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFoundWhenCurrencyDoesNotExist()
    {
        var currencyId = await GetUniqueCurrencyIdAsync();

        using var response = await _fixture.Client.DeleteAsync($"/api/v1/Currencies/{currencyId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNoContentAndDeletedCurrencyIsNotFound()
    {
        var (currencyId, numericCode) = await GetUniqueCurrencyIdentityAsync();
        await using (var scope = _fixture.Factory.Services.CreateAsyncScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
            context.Currencies.Add(new Currency
            {
                Id = currencyId,
                NumericCode = numericCode,
                Symbol = "¤",
                DecimalSeparator = '.',
                MinorUnit = 2,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            });
            await context.SaveChangesAsync();
        }

        using var deleteResponse = await _fixture.Client.DeleteAsync($"/api/v1/Currencies/{currencyId}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        using var getResponse = await _fixture.Client.GetAsync($"/api/v1/Currencies/{currencyId}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    private async Task<(string Id, string NumericCode)> GetUniqueCurrencyIdentityAsync(
        IEnumerable<string>? excludedNumericCodes = null)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var existing = await context.Currencies
            .IgnoreQueryFilters()
            .Select(currency => new { currency.Id, currency.NumericCode })
            .ToListAsync();
        var existingIds = existing.Select(currency => currency.Id).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var existingCodes = existing.Select(currency => currency.NumericCode)
            .Concat(excludedNumericCodes ?? Array.Empty<string>())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        string currencyId;
        do
        {
            currencyId = new string(Enumerable.Range(0, 3)
                .Select(_ => (char)('A' + Random.Shared.Next(26)))
                .ToArray());
        }
        while (existingIds.Contains(currencyId));

        var numericCode = Enumerable.Range(0, 1000)
            .Select(value => value.ToString("D3"))
            .First(code => !existingCodes.Contains(code));

        return (currencyId, numericCode);
    }

    private async Task<string> GetUniqueCurrencyIdAsync() =>
        (await GetUniqueCurrencyIdentityAsync()).Id;
}
