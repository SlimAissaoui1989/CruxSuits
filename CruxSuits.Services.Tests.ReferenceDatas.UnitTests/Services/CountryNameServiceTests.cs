using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Country;
using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class CountryNameServiceTests
{
    #region GetAllAsync

    [Fact]
    public async Task GetAllAsync_WhenCountryDoesNotExist_ThrowsNotFound()
    {
        const string countryId = "XX";
        var context = CreateContext(countryId, countryExists: false);

        await Assert.ThrowsAsync<CxException>(() => context.Service.GetAllAsync(countryId));

        context.CountryNameRepository.Verify(repository => repository.GetAllAsync<CountryNameResponse>(
            It.IsAny<Expression<Func<CountryName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetAllAsync_WhenCountryHasNoTranslations_ReturnsEmptyList()
    {
        const string countryId = "QX";
        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryNameResponse>(
                It.IsAny<Expression<Func<CountryName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await context.Service.GetAllAsync(countryId);

        Assert.Empty(result);
        context.CountryNameRepository.Verify(repository => repository.GetAllAsync<CountryNameResponse>(
            It.Is<Expression<Func<CountryName, bool>>>(predicate =>
                predicate.Compile()(new CountryName { CountryId = countryId })
                && !predicate.Compile()(new CountryName { CountryId = "US" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_WhenCountryHasTranslations_ReturnsAllTranslationsForThatCountry()
    {
        const string countryId = "US";
        var expectedNames = new List<CountryNameResponse>
        {
            new() { LanguageId = "en", Name = "United States", Nationality = "American" },
            new() { LanguageId = "fr", Name = "États-Unis", Nationality = "Américaine" },
            new() { LanguageId = "ar", Name = "الولايات المتحدة", Nationality = "أمريكي" }
        };

        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryNameResponse>(
                It.IsAny<Expression<Func<CountryName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<CountryName, bool>> predicate, CancellationToken _) =>
                predicate.Compile()(new CountryName { CountryId = countryId })
                    ? expectedNames
                    : new List<CountryNameResponse>());

        var result = await context.Service.GetAllAsync(countryId);

        Assert.Equal(expectedNames, result);
        Assert.Collection(
            result,
            name => Assert.Equal(("en", "United States", "American"),
                (name.LanguageId, name.Name, name.Nationality)),
            name => Assert.Equal(("fr", "États-Unis", "Américaine"),
                (name.LanguageId, name.Name, name.Nationality)),
            name => Assert.Equal(("ar", "الولايات المتحدة", "أمريكي"),
                (name.LanguageId, name.Name, name.Nationality)));

        context.CountryNameRepository.Verify(repository => repository.GetAllAsync<CountryNameResponse>(
            It.Is<Expression<Func<CountryName, bool>>>(predicate =>
                predicate.Compile()(new CountryName { CountryId = countryId })
                && !predicate.Compile()(new CountryName { CountryId = "CA" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region GetByLanguageAsync

    [Fact]
    public async Task GetByLanguageAsync_WhenTranslationExists_ReturnsThatCountryName()
    {
        const string countryId = "US";
        const string languageId = "fr";
        var expectedName = new CountryNameResponse
        {
            LanguageId = languageId,
            LanguageName = "French",
            Name = "États-Unis",
            Nationality = "Américaine"
        };

        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryNameResponse>(
                It.IsAny<Expression<Func<CountryName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<CountryName, bool>> predicate, CancellationToken _) =>
                predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = languageId })
                    ? [expectedName]
                    : []);

        var result = await context.Service.GetByLanguageAsync(countryId, languageId);

        Assert.Equal(expectedName, result);
        context.CountryNameRepository.Verify(repository => repository.GetAllAsync<CountryNameResponse>(
            It.Is<Expression<Func<CountryName, bool>>>(predicate =>
                predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = languageId })
                && !predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = "en" })
                && !predicate.Compile()(new CountryName { CountryId = "CA", LanguageId = languageId })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetByLanguageAsync_WhenTranslationDoesNotExist_ThrowsNotFound()
    {
        const string countryId = "US";
        const string languageId = "fr";
        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryNameResponse>(
                It.IsAny<Expression<Func<CountryName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        await Assert.ThrowsAsync<CxException>(() => context.Service.GetByLanguageAsync(countryId, languageId));

        context.CountryNameRepository.Verify(repository => repository.GetAllAsync<CountryNameResponse>(
            It.Is<Expression<Func<CountryName, bool>>>(predicate =>
                predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = languageId })
                && !predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = "en" })
                && !predicate.Compile()(new CountryName { CountryId = "CA", LanguageId = languageId })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region CreateAsync

    [Fact]
    public async Task CreateAsync_WhenCountryLanguagePairIsNew_AddsAndReturnsCountryName()
    {
        const string countryId = "US";
        var request = new CountryNameRequest
        {
            LanguageId = "fr",
            Name = "États-Unis",
            Nationality = "Américaine"
        };
        var expected = new CountryNameResponse
        {
            LanguageId = "fr",
            LanguageName = "French (Français)",
            Name = request.Name,
            Nationality = request.Nationality
        };
        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryName>(
                It.IsAny<Expression<Func<CountryName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        var entity = new CountryName
        {
            CountryId = countryId,
            LanguageId = request.LanguageId,
            Name = request.Name,
            Nationality = request.Nationality
        };
        context.Mapper.Setup(mapper => mapper.Map<CountryName>(request)).Returns(entity);
        context.CountryNameRepository
            .Setup(repository => repository.AddAsync(It.IsAny<CountryName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);
        context.Mapper.Setup(mapper => mapper.Map<CountryNameResponse>(entity)).Returns(expected);

        var result = await context.Service.CreateAsync(countryId, request);

        Assert.Equal(expected, result);
        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(
            request.LanguageId, It.IsAny<CancellationToken>()), Times.Once);
        context.CountryNameRepository.Verify(repository => repository.AddAsync(
            It.Is<CountryName>(name => name.CountryId == countryId
                && name.LanguageId == request.LanguageId
                && name.Name == request.Name
                && name.Nationality == request.Nationality),
            It.IsAny<CancellationToken>()), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WhenCountryDoesNotExist_ThrowsNotFoundWithoutAdding()
    {
        const string countryId = "XX";
        var request = new CountryNameRequest
        {
            LanguageId = "fr",
            Name = "Pays inexistant",
            Nationality = "Inconnue"
        };
        var context = CreateContext(countryId, countryExists: false);

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(countryId, request));

        context.CountryNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<CountryName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
        context.CountryNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<CountryName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenLanguageDoesNotExist_ThrowsBadRequestWithoutAdding()
    {
        const string countryId = "US";
        const string languageId = "zz";
        var request = new CountryNameRequest
        {
            LanguageId = languageId,
            Name = "Unknown language",
            Nationality = "Unknown"
        };
        var context = CreateContext(countryId);
        context.LanguageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Language?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(countryId, request));

        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()), Times.Once);
        context.CountryNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<CountryName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
        context.CountryNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<CountryName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_WhenCountryAlreadyHasTranslationInLanguage_RejectsDuplicate()
    {
        const string countryId = "US";
        var request = new CountryNameRequest
        {
            LanguageId = "en",
            Name = "United States",
            Nationality = "American"
        };
        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CountryName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CountryName
            {
                CountryId = countryId,
                LanguageId = request.LanguageId,
                Name = request.Name,
                Nationality = request.Nationality
            });

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(countryId, request));

        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(
            request.LanguageId, It.IsAny<CancellationToken>()), Times.Once);
        context.CountryNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<CountryName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region UpdateAsync

    [Fact]
    public async Task UpdateAsync_WhenTranslationExists_UpdatesDesignationsAndPreservesCountryAndLanguage()
    {
        const string countryId = "US";
        const string languageId = "en";
        var request = new CountryNameUpdateRequest
        {
            Name = "United States of America",
            Nationality = "American"
        };
        var existing = new CountryName
        {
            CountryId = countryId,
            LanguageId = languageId,
            Name = "United States",
            Nationality = "American"
        };
        var response = new CountryNameResponse
        {
            LanguageId = languageId,
            LanguageName = "English (English)",
            Name = request.Name,
            Nationality = request.Nationality
        };
        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CountryName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        context.Mapper
            .Setup(mapper => mapper.Map(request, existing))
            .Callback(() =>
            {
                existing.Name = request.Name;
                existing.Nationality = request.Nationality;
            })
            .Returns(existing);
        context.CountryNameRepository
            .Setup(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        context.Mapper.Setup(mapper => mapper.Map<CountryNameResponse>(existing)).Returns(response);

        var result = await context.Service.UpdateAsync(countryId, languageId, request);

        Assert.Same(response, result);
        Assert.Equal(countryId, existing.CountryId);
        Assert.Equal(languageId, existing.LanguageId);
        Assert.Equal(request.Name, existing.Name);
        Assert.Equal(request.Nationality, existing.Nationality);
        context.CountryNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.Is<Expression<Func<CountryName, bool>>>(predicate =>
                predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = languageId })
                && !predicate.Compile()(new CountryName { CountryId = "CA", LanguageId = languageId })
                && !predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = "fr" })),
            It.IsAny<CancellationToken>()), Times.Once);
        context.CountryNameRepository.Verify(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_WhenTranslationDoesNotExist_ThrowsNotFoundWithoutUpdating()
    {
        const string countryId = "US";
        const string languageId = "en";
        var request = new CountryNameUpdateRequest { Name = "United States of America", Nationality = "American" };
        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CountryName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((CountryName?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.UpdateAsync(countryId, languageId, request));

        context.CountryNameRepository.Verify(repository => repository.UpdateAsync(
            It.IsAny<CountryName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        context.Mapper.Verify(mapper => mapper.Map(
            It.IsAny<CountryNameUpdateRequest>(), It.IsAny<CountryName>()), Times.Never);
    }

    #endregion

    #region RemoveAsync

    [Fact]
    public async Task RemoveAsync_WhenTranslationExists_RemovesAndSavesIt()
    {
        const string countryId = "US";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var countryName = new CountryName { CountryId = countryId, LanguageId = languageId };
        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CountryName, bool>>>(), cancellation.Token))
            .ReturnsAsync(countryName);

        await context.Service.RemoveAsync(countryId, languageId, cancellation.Token);

        context.CountryNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.Is<Expression<Func<CountryName, bool>>>(predicate =>
                predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = languageId })
                && !predicate.Compile()(new CountryName { CountryId = "CA", LanguageId = languageId })
                && !predicate.Compile()(new CountryName { CountryId = countryId, LanguageId = "fr" })),
            cancellation.Token), Times.Once);
        context.CountryNameRepository.Verify(repository => repository.RemoveAsync(countryName, cancellation.Token), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(cancellation.Token), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_WhenTranslationDoesNotExist_ThrowsNotFoundWithoutRemoving()
    {
        const string countryId = "US";
        const string languageId = "en";
        var context = CreateContext(countryId);
        context.CountryNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CountryName, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CountryName?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.RemoveAsync(countryId, languageId));

        context.CountryNameRepository.Verify(repository => repository.RemoveAsync(
            It.IsAny<CountryName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region TestHelpers

    private static TestContext CreateContext(string countryId, bool countryExists = true)
    {
        var countryRepository = new Mock<ICountryRepository>();
        countryRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<CountryDetailsResponse>(
                countryId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(countryExists ? new CountryDetailsResponse { Id = countryId } : null);

        var countryNameRepository = new Mock<ICountryNameRepository>();
        var languageRepository = new Mock<ILanguageRepository>();
        languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string languageId, CancellationToken _) => new Language { Id = languageId });
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.SetupGet(work => work.Country).Returns(countryRepository.Object);
        unitOfWork.SetupGet(work => work.CountryName).Returns(countryNameRepository.Object);
        unitOfWork.SetupGet(work => work.Language).Returns(languageRepository.Object);

        var mapper = new Mock<IMapper>();
        return new TestContext(
            new CountryNameService(unitOfWork.Object, mapper.Object),
            countryNameRepository,
            languageRepository,
            unitOfWork,
            mapper);
    }

    private sealed record TestContext(
        CountryNameService Service,
        Mock<ICountryNameRepository> CountryNameRepository,
        Mock<ILanguageRepository> LanguageRepository,
        Mock<IReferenceDataUnitOfWork> UnitOfWork,
        Mock<IMapper> Mapper);

    #endregion
}
