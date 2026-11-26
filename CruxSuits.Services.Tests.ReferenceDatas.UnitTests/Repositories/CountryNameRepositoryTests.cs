using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

public sealed class CountryNameRepositoryTests
{
    #region CountryAndLanguagePair_HasUniqueDatabaseIndex

    // Contract check: verifies CountryAndLanguagePair has unique database index;
    // A pass confirms the declared contract;
    // Failure means the expected contract is missing.
    [Fact]
    public void CountryAndLanguagePair_HasUniqueDatabaseIndex()
    {
        using var fixture = new CountryNameRepositoryFixture();
        var entityType = fixture.Context.Model.FindEntityType(typeof(CountryName));

        Assert.NotNull(entityType);
        Assert.Contains(entityType.GetIndexes(), index =>
            index.IsUnique
            && index.Properties.Select(property => property.Name)
                .SequenceEqual([nameof(CountryName.CountryId), nameof(CountryName.LanguageId)]));
    }

    #endregion

    #region AddAsync

    // Success case: verifies AddAsync persists the translation when country name is valid;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task AddAsync_WhenCountryNameIsValid_PersistsTheTranslation()
    {
        using var fixture = new CountryNameRepositoryFixture();
        var countryName = new CountryName
        {
            CountryId = "US",
            LanguageId = "fr",
            Name = "États-Unis",
            Nationality = "Américaine"
        };

        var added = await fixture.Repository.AddAsync(countryName);
        await fixture.Context.SaveChangesAsync();

        Assert.Same(countryName, added);
        var persisted = await fixture.Context.CountryNames.SingleAsync(name =>
            name.CountryId == "US" && name.LanguageId == "fr");
        Assert.Equal("États-Unis", persisted.Name);
        Assert.Equal("Américaine", persisted.Nationality);
    }

    #endregion
}
