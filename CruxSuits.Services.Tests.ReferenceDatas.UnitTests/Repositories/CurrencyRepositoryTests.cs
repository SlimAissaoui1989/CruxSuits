using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

public sealed class CurrencyRepositoryTests
{
    #region GetAllLocalizedAsync

    // Success case: verifies GetAllLocalizedAsync returns localized name and currency codes when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllLocalizedAsync_WhenTranslationExists_ReturnsLocalizedNameAndCurrencyCodes()
    {
        using var fixture = new CurrencyRepositoryFixture();
        await fixture.AddCurrencyAsync("QXZ", "998", ("fr", "Devise de test"));

        var currencies = await fixture.Repository.GetAllLocalizedAsync<CurrencyResponse>("fr");

        var currency = Assert.Single(currencies, item => item.Id == "QXZ");
        Assert.Equal("Devise de test", currency.Name);
        Assert.Equal("998", currency.NumericCode);
    }

    // Success case: verifies GetAllLocalizedAsync uses unknown name when translation is missing;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllLocalizedAsync_WhenTranslationIsMissing_UsesUnknownName()
    {
        using var fixture = new CurrencyRepositoryFixture();
        await fixture.AddCurrencyAsync("QXZ", "998", ("en", "Test currency"));

        var currencies = await fixture.Repository.GetAllLocalizedAsync<CurrencyResponse>("fr");

        var currency = Assert.Single(currencies, item => item.Id == "QXZ");
        Assert.Equal("Unknown", currency.Name);
        Assert.Equal("998", currency.NumericCode);
    }

    #endregion
}
