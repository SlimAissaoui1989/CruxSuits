using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

public sealed class CountryRepositoryTests
{
    #region AddAsync

    [Fact]
    public async Task AddAsync_WhenCountryIsValid_AddsAndTracksCountry()
    {
        using var fixture = new CountryRepositoryFixture();
        var country = CountryRepositoryFixture.CreateCountry();

        var result = await fixture.Repository.AddAsync(country);

        Assert.Same(country, result);
        Assert.Equal(EntityState.Added, fixture.Context.Entry(country).State);
    }

    [Theory]
    [InlineData("USA", "USA", "840", "Country ID must contain exactly two ASCII letters.")]
    [InlineData("US", "US", "840", "Alpha3Code must contain exactly three ASCII letters.")]
    [InlineData("US", "USA", "84A", "NumericCode must contain exactly three ASCII digits.")]
    [InlineData("US", "USA", "840", "CurrencyId is required.", " ")]
    public async Task AddAsync_WhenCountryCodeIsInvalid_ThrowsArgumentException(
        string id,
        string alpha3Code,
        string numericCode,
        string message,
        string currencyId = "USD")
    {
        using var fixture = new CountryRepositoryFixture();
        var country = CountryRepositoryFixture.CreateCountry(id, alpha3Code, numericCode, currencyId);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => fixture.Repository.AddAsync(country));

        Assert.StartsWith(message, exception.Message, StringComparison.Ordinal);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(country).State);
    }

    [Fact]
    public async Task AddAsync_WhenCurrencyDoesNotExist_ThrowsArgumentException()
    {
        using var fixture = new CountryRepositoryFixture();
        var country = CountryRepositoryFixture.CreateCountry(currencyId: "ZZZ");

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => fixture.Repository.AddAsync(country));

        Assert.Equal("Currency 'ZZZ' does not exist. (Parameter 'currencyId')", exception.Message);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(country).State);
    }

    [Fact]
    public async Task AddAsync_WhenCountryIdAlreadyExists_ThrowsInvalidOperationException()
    {
        using var fixture = new CountryRepositoryFixture();
        fixture.Context.Countries.Add(CountryRepositoryFixture.CreateCountry());
        await fixture.Context.SaveChangesAsync();
        var duplicate = CountryRepositoryFixture.CreateCountry(alpha3Code: "CAN", numericCode: "124");

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => fixture.Repository.AddAsync(duplicate));

        Assert.Equal("Country 'US' already exists.", exception.Message);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(duplicate).State);
    }

    [Fact]
    public async Task AddAsync_WhenAlpha3CodeAlreadyExists_ThrowsInvalidOperationException()
    {
        using var fixture = new CountryRepositoryFixture();
        fixture.Context.Countries.Add(CountryRepositoryFixture.CreateCountry());
        await fixture.Context.SaveChangesAsync();
        var duplicate = CountryRepositoryFixture.CreateCountry(id: "CA", numericCode: "124");

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => fixture.Repository.AddAsync(duplicate));

        Assert.Equal("Alpha3Code 'USA' is already assigned to another country.", exception.Message);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(duplicate).State);
    }

    [Fact]
    public async Task AddAsync_WhenNumericCodeAlreadyExists_ThrowsInvalidOperationException()
    {
        using var fixture = new CountryRepositoryFixture();
        fixture.Context.Countries.Add(CountryRepositoryFixture.CreateCountry());
        await fixture.Context.SaveChangesAsync();
        var duplicate = CountryRepositoryFixture.CreateCountry(id: "CA", alpha3Code: "CAN");

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => fixture.Repository.AddAsync(duplicate));

        Assert.Equal("NumericCode '840' is already assigned to another country.", exception.Message);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(duplicate).State);
    }

    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_WhenCountryIsValid_UpdatesAndTracksCountry()
    {
        using var fixture = new CountryRepositoryFixture();
        var existing = CountryRepositoryFixture.CreateCountry();
        fixture.Context.Countries.Add(existing);
        await fixture.Context.SaveChangesAsync();
        var update = CountryRepositoryFixture.CreateCountry(alpha3Code: "USA", numericCode: "841");

        var result = await fixture.Repository.UpdateAsync(update);

        Assert.Same(existing, result);
        Assert.Equal("USA", existing.Alpha3Code);
        Assert.Equal("841", existing.NumericCode);
        Assert.Equal(EntityState.Modified, fixture.Context.Entry(existing).State);
    }

    [Theory]
    [InlineData("USA", "USA", "840", "Country ID must contain exactly two ASCII letters.")]
    [InlineData("US", "US", "840", "Alpha3Code must contain exactly three ASCII letters.")]
    [InlineData("US", "USA", "84A", "NumericCode must contain exactly three ASCII digits.")]
    [InlineData("US", "USA", "840", "CurrencyId is required.", " ")]
    public async Task UpdateAsync_WhenCountryCodeIsInvalid_ThrowsArgumentException(
        string id,
        string alpha3Code,
        string numericCode,
        string message,
        string currencyId = "USD")
    {
        using var fixture = new CountryRepositoryFixture();
        var update = CountryRepositoryFixture.CreateCountry(id, alpha3Code, numericCode, currencyId);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => fixture.Repository.UpdateAsync(update));

        Assert.StartsWith(message, exception.Message, StringComparison.Ordinal);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(update).State);
    }

    [Fact]
    public async Task UpdateAsync_WhenCurrencyDoesNotExist_ThrowsArgumentException()
    {
        using var fixture = new CountryRepositoryFixture();
        var update = CountryRepositoryFixture.CreateCountry(currencyId: "ZZZ");

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => fixture.Repository.UpdateAsync(update));

        Assert.Equal("Currency 'ZZZ' does not exist. (Parameter 'currencyId')", exception.Message);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(update).State);
    }

    [Fact]
    public async Task UpdateAsync_WhenAlpha3CodeBelongsToAnotherCountry_ThrowsInvalidOperationException()
    {
        using var fixture = new CountryRepositoryFixture();
        fixture.Context.Countries.Add(CountryRepositoryFixture.CreateCountry());
        await fixture.Context.SaveChangesAsync();
        var update = CountryRepositoryFixture.CreateCountry(id: "CA", numericCode: "124");

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => fixture.Repository.UpdateAsync(update));

        Assert.Equal("Alpha3Code 'USA' is already assigned to another country.", exception.Message);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(update).State);
    }

    [Fact]
    public async Task UpdateAsync_WhenNumericCodeBelongsToAnotherCountry_ThrowsInvalidOperationException()
    {
        using var fixture = new CountryRepositoryFixture();
        fixture.Context.Countries.Add(CountryRepositoryFixture.CreateCountry());
        await fixture.Context.SaveChangesAsync();
        var update = CountryRepositoryFixture.CreateCountry(id: "CA", alpha3Code: "CAN");

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => fixture.Repository.UpdateAsync(update));

        Assert.Equal("NumericCode '840' is already assigned to another country.", exception.Message);
        Assert.Equal(EntityState.Detached, fixture.Context.Entry(update).State);
    }

    #endregion
}
