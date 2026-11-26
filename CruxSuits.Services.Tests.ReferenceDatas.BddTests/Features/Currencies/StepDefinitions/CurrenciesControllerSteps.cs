using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CruxSuits.Shared.Constants;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Tests.BddTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.BddTests.Features.Currencies.StepDefinitions;

[Binding]
public sealed partial class CurrenciesControllerSteps
{
    private ReferenceDatasBddFixture _fixture = null!;
    private HttpResponseMessage? _response;
    private string? _deletedCurrencyId;

    [BeforeScenario("currencies_controller")]
    public async Task StartApi()
    {
        _fixture = new ReferenceDatasBddFixture();
        await _fixture.InitializeAsync();
    }

    [AfterScenario("currencies_controller")]
    public async Task StopApi()
    {
        _response?.Dispose();
        if (_fixture is not null)
            await _fixture.DisposeAsync();
    }

    [Given("the test currency \"(.*)\" exists")]
    public Task GivenTestCurrencyExists(string currencyId) =>
        EnsureCurrencyAsync(currencyId);

    [Given("the test currency \"(.*)\" exists with numeric code \"(.*)\"")]
    public Task GivenTestCurrencyExistsWithNumericCode(string currencyId, string numericCode) =>
        EnsureCurrencyAsync(currencyId, numericCode);

    [Given("the test currency \"(.*)\" exists with numeric code \"(.*)\", symbol \"(.*)\", decimal separator \"(.)\" and minor unit (.*)")]
    public Task GivenTestCurrencyExistsWithDetails(
        string currencyId,
        string numericCode,
        string symbol,
        char decimalSeparator,
        byte minorUnit) =>
        EnsureCurrencyAsync(currencyId, numericCode, symbol, decimalSeparator, minorUnit);

    [Then("the currency API response status is (.*)")]
    public async Task ThenCurrencyApiResponseStatusIs(int statusCode)
    {
        var responseBody = await _response!.Content.ReadAsStringAsync();
        Assert.True((int)_response.StatusCode == statusCode,
            $"Expected {(HttpStatusCode)statusCode} but received {(int)_response.StatusCode}: {responseBody}");
    }

    private async Task EnsureCurrencyAsync(
        string currencyId,
        string? numericCode = null,
        string? symbol = null,
        char? decimalSeparator = null,
        byte? minorUnit = null)
    {
        await using var scope = _fixture.Factory.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ReferenceDataContext>();
        var currency = await context.Currencies.FindAsync(currencyId);
        if (currency is not null)
        {
            if (numericCode is not null)
                Assert.Equal(numericCode, currency.NumericCode);
            if (symbol is not null)
                Assert.Equal(symbol, currency.Symbol);
            if (decimalSeparator is not null)
                Assert.Equal(decimalSeparator, currency.DecimalSeparator);
            if (minorUnit is not null)
                Assert.Equal(minorUnit, currency.MinorUnit);
            return;
        }

        context.Currencies.Add(new Currency
        {
            Id = currencyId,
            NumericCode = numericCode ?? (currencyId == "USD" ? "840" : "997"),
            Symbol = symbol ?? "¤",
            DecimalSeparator = decimalSeparator ?? '.',
            MinorUnit = minorUnit ?? 2,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        });
        await context.SaveChangesAsync();
    }

    private async Task SendAsync(HttpMethod method, string uri, object? body = null, string? acceptLanguage = null)
    {
        _response?.Dispose();
        using var request = new HttpRequestMessage(method, uri);
        if (body is not null)
            request.Content = JsonContent.Create(body, options: new JsonSerializerOptions { PropertyNamingPolicy = null });
        if (!string.IsNullOrWhiteSpace(acceptLanguage))
            request.Headers.TryAddWithoutValidation(HttpHeaderNames.AcceptLanguage, acceptLanguage);
        _response = await _fixture.Client.SendAsync(request);
    }

    private static string GetString(JsonElement element, string propertyName) =>
        element.EnumerateObject()
            .Single(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            .Value.GetString()!;

    private static byte GetByte(JsonElement element, string propertyName) =>
        element.EnumerateObject()
            .Single(property => string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase))
            .Value.GetByte();
}
