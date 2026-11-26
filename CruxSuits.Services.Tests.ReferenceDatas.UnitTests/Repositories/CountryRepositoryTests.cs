using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

public sealed class CountryRepositoryTests
{
    #region AddAsync

    // Success case: verifies AddAsync adds and tracks country;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task AddAsync_AddsAndTracksCountry()
    {
        using var fixture = new CountryRepositoryFixture();
        var country = CountryRepositoryFixture.CreateCountry();

        var result = await fixture.Repository.AddAsync(country);

        Assert.Same(country, result);
        Assert.Equal(EntityState.Added, fixture.Context.Entry(country).State);
    }

    #endregion

    #region UpdateAsync

    // Success case: verifies UpdateAsync updates and tracks country;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task UpdateAsync_UpdatesAndTracksCountry()
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

    #endregion
}
