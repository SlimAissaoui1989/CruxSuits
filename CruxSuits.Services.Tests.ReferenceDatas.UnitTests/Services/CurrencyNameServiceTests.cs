using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;
using CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class CurrencyNameServiceTests
{
    #region GetAllAsync

    // Failure case: verifies GetAllAsync throws not found when currency does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetAllAsync_WhenCurrencyDoesNotExist_ThrowsNotFound()
    {
        const string currencyId = "ZZZ";
        var context = CreateContext(currencyId, currencyExists: false);

        await Assert.ThrowsAsync<CxException>(() => context.Service.GetAllAsync(currencyId));

        context.CurrencyNameRepository.Verify(repository => repository.GetAllAsync<CurrencyNameResponse>(
            It.IsAny<Expression<Func<CurrencyName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    // Success case: verifies GetAllAsync returns empty list when currency has no translations;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenCurrencyHasNoTranslations_ReturnsEmptyList()
    {
        const string currencyId = "QXZ";
        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetAllAsync<CurrencyNameResponse>(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await context.Service.GetAllAsync(currencyId);

        Assert.Empty(result);
        context.CurrencyNameRepository.Verify(repository => repository.GetAllAsync<CurrencyNameResponse>(
            It.Is<Expression<Func<CurrencyName, bool>>>(predicate =>
                predicate.Compile()(new CurrencyName { CurrencyId = currencyId })
                && !predicate.Compile()(new CurrencyName { CurrencyId = "USD" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Success case: verifies GetAllAsync returns all translations for that currency when currency has translations;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenCurrencyHasTranslations_ReturnsAllTranslationsForThatCurrency()
    {
        const string currencyId = "USD";
        var expectedNames = new List<CurrencyNameResponse>
        {
            new() { LanguageId = "en", Name = "US Dollar" },
            new() { LanguageId = "fr", Name = "dollar des États-Unis" },
            new() { LanguageId = "ar", Name = "دولار أمريكي" }
        };

        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetAllAsync<CurrencyNameResponse>(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<CurrencyName, bool>> predicate, CancellationToken _) =>
                predicate.Compile()(new CurrencyName { CurrencyId = currencyId })
                    ? expectedNames
                    : new List<CurrencyNameResponse>());

        var result = await context.Service.GetAllAsync(currencyId);

        Assert.Equal(expectedNames, result);
        Assert.Collection(
            result,
            name => Assert.Equal(("en", "US Dollar"),
                (name.LanguageId, name.Name)),
            name => Assert.Equal(("fr", "dollar des États-Unis"),
                (name.LanguageId, name.Name)),
            name => Assert.Equal(("ar", "دولار أمريكي"),
                (name.LanguageId, name.Name)));

        context.CurrencyNameRepository.Verify(repository => repository.GetAllAsync<CurrencyNameResponse>(
            It.Is<Expression<Func<CurrencyName, bool>>>(predicate =>
                predicate.Compile()(new CurrencyName { CurrencyId = currencyId })
                && !predicate.Compile()(new CurrencyName { CurrencyId = "CAD" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region GetByLanguageAsync

    // Success case: verifies GetByLanguageAsync returns that currency name when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByLanguageAsync_WhenTranslationExists_ReturnsThatCurrencyName()
    {
        const string currencyId = "USD";
        const string languageId = "fr";
        var expectedName = new CurrencyNameResponse
        {
            LanguageId = languageId,
            LanguageName = "French",
            Name = "dollar des États-Unis"
        };

        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync<CurrencyNameResponse>(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedName);

        var result = await context.Service.GetByLanguageAsync(currencyId, languageId);

        Assert.Equal(expectedName, result);
        context.CurrencyNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync<CurrencyNameResponse>(
            It.Is<Expression<Func<CurrencyName, bool>>>(predicate =>
                predicate.Compile()(new CurrencyName { CurrencyId = currencyId, LanguageId = languageId })
                && !predicate.Compile()(new CurrencyName { CurrencyId = currencyId, LanguageId = "en" })
                && !predicate.Compile()(new CurrencyName { CurrencyId = "CAD", LanguageId = languageId })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies GetByLanguageAsync throws not found when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByLanguageAsync_WhenTranslationDoesNotExist_ThrowsNotFound()
    {
        const string currencyId = "USD";
        const string languageId = "fr";
        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync<CurrencyNameResponse>(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((CurrencyNameResponse?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.GetByLanguageAsync(currencyId, languageId));

        context.CurrencyNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync<CurrencyNameResponse>(
            It.Is<Expression<Func<CurrencyName, bool>>>(predicate =>
                predicate.Compile()(new CurrencyName { CurrencyId = currencyId, LanguageId = languageId })
                && !predicate.Compile()(new CurrencyName { CurrencyId = currencyId, LanguageId = "en" })
                && !predicate.Compile()(new CurrencyName { CurrencyId = "CAD", LanguageId = languageId })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region CreateAsync

    // Success case: verifies CreateAsync adds and returns currency name when currency-language pair is new;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_WhenCurrencyLanguagePairIsNew_AddsAndReturnsCurrencyName()
    {
        const string currencyId = "USD";
        var request = new CurrencyNameRequest
        {
            LanguageId = "fr",
            Name = "dollar des États-Unis"
        };
        var expected = new CurrencyNameResponse
        {
            LanguageId = "fr",
            LanguageName = "French (Français)",
            Name = request.Name
        };
        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetAllAsync<CurrencyName>(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        var entity = new CurrencyName
        {
            CurrencyId = currencyId,
            LanguageId = request.LanguageId,
            Name = request.Name
        };
        context.Mapper.Setup(mapper => mapper.Map<CurrencyName>(request)).Returns(entity);
        context.CurrencyNameRepository
            .Setup(repository => repository.AddAsync(It.IsAny<CurrencyName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);
        context.Mapper.Setup(mapper => mapper.Map<CurrencyNameResponse>(entity)).Returns(expected);

        var result = await context.Service.CreateAsync(currencyId, request);

        Assert.Equal(expected, result);
        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(
            request.LanguageId, It.IsAny<CancellationToken>()), Times.Once);
        context.CurrencyNameRepository.Verify(repository => repository.AddAsync(
            It.Is<CurrencyName>(name => name.CurrencyId == currencyId
                && name.LanguageId == request.LanguageId
                && name.Name == request.Name),
            It.IsAny<CancellationToken>()), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies CreateAsync throws not found without adding when currency does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenCurrencyDoesNotExist_ThrowsNotFoundWithoutAdding()
    {
        const string currencyId = "ZZZ";
        var request = new CurrencyNameRequest
        {
            LanguageId = "fr",
            Name = "Unknown currency"
        };
        var context = CreateContext(currencyId, currencyExists: false);

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(currencyId, request));

        context.CurrencyNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<CurrencyName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
        context.CurrencyNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<CurrencyName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync throws bad request without adding when language does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenLanguageDoesNotExist_ThrowsBadRequestWithoutAdding()
    {
        const string currencyId = "USD";
        const string languageId = "zz";
        var request = new CurrencyNameRequest
        {
            LanguageId = languageId,
            Name = "Unknown language"
        };
        var context = CreateContext(currencyId);
        context.LanguageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Language?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(currencyId, request));

        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()), Times.Once);
        context.CurrencyNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<CurrencyName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
        context.CurrencyNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<CurrencyName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync rejects a duplicate translation for the same currency and language;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenCurrencyAlreadyHasTranslationInLanguage_RejectsDuplicate()
    {
        const string currencyId = "USD";
        var request = new CurrencyNameRequest
        {
            LanguageId = "en",
            Name = "US Dollar"
        };
        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CurrencyName
            {
                CurrencyId = currencyId,
                LanguageId = request.LanguageId,
                Name = request.Name,
            });

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(currencyId, request));

        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(
            request.LanguageId, It.IsAny<CancellationToken>()), Times.Once);
        context.CurrencyNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<CurrencyName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region UpdateAsync

    // Success case: verifies UpdateAsync updates the name and preserves currency and language when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task UpdateAsync_WhenTranslationExists_UpdatesNameAndPreservesCurrencyAndLanguage()
    {
        const string currencyId = "USD";
        const string languageId = "en";
        var request = new CurrencyNameUpdateRequest
        {
            Name = "United States dollar"
        };
        var existing = new CurrencyName
        {
            CurrencyId = currencyId,
            LanguageId = languageId,
            Name = "US Dollar"
        };
        var response = new CurrencyNameResponse
        {
            LanguageId = languageId,
            LanguageName = "English (English)",
            Name = request.Name
        };
        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        context.Mapper
            .Setup(mapper => mapper.Map(request, existing))
            .Callback(() => existing.Name = request.Name)
            .Returns(existing);
        context.CurrencyNameRepository
            .Setup(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        context.Mapper.Setup(mapper => mapper.Map<CurrencyNameResponse>(existing)).Returns(response);

        var result = await context.Service.UpdateAsync(currencyId, languageId, request);

        Assert.Same(response, result);
        Assert.Equal(currencyId, existing.CurrencyId);
        Assert.Equal(languageId, existing.LanguageId);
        Assert.Equal(request.Name, existing.Name);
        context.CurrencyNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.Is<Expression<Func<CurrencyName, bool>>>(predicate =>
                predicate.Compile()(new CurrencyName { CurrencyId = currencyId, LanguageId = languageId })
                && !predicate.Compile()(new CurrencyName { CurrencyId = "CAD", LanguageId = languageId })
                && !predicate.Compile()(new CurrencyName { CurrencyId = currencyId, LanguageId = "fr" })),
            It.IsAny<CancellationToken>()), Times.Once);
        context.CurrencyNameRepository.Verify(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies UpdateAsync throws not found without updating when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task UpdateAsync_WhenTranslationDoesNotExist_ThrowsNotFoundWithoutUpdating()
    {
        const string currencyId = "USD";
        const string languageId = "en";
        var request = new CurrencyNameUpdateRequest { Name = "United States dollar" };
        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((CurrencyName?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.UpdateAsync(currencyId, languageId, request));

        context.CurrencyNameRepository.Verify(repository => repository.UpdateAsync(
            It.IsAny<CurrencyName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        context.Mapper.Verify(mapper => mapper.Map(
            It.IsAny<CurrencyNameUpdateRequest>(), It.IsAny<CurrencyName>()), Times.Never);
    }

    #endregion

    #region RemoveAsync

    // Success case: verifies RemoveAsync removes and saves it when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_WhenTranslationExists_RemovesAndSavesIt()
    {
        const string currencyId = "USD";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var currencyName = new CurrencyName { CurrencyId = currencyId, LanguageId = languageId };
        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(), cancellation.Token))
            .ReturnsAsync(currencyName);

        await context.Service.RemoveAsync(currencyId, languageId, cancellation.Token);

        context.CurrencyNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.Is<Expression<Func<CurrencyName, bool>>>(predicate =>
                predicate.Compile()(new CurrencyName { CurrencyId = currencyId, LanguageId = languageId })
                && !predicate.Compile()(new CurrencyName { CurrencyId = "CAD", LanguageId = languageId })
                && !predicate.Compile()(new CurrencyName { CurrencyId = currencyId, LanguageId = "fr" })),
            cancellation.Token), Times.Once);
        context.CurrencyNameRepository.Verify(repository => repository.RemoveAsync(currencyName, cancellation.Token), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(cancellation.Token), Times.Once);
    }

    // Failure case: verifies RemoveAsync throws not found without removing when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task RemoveAsync_WhenTranslationDoesNotExist_ThrowsNotFoundWithoutRemoving()
    {
        const string currencyId = "USD";
        const string languageId = "en";
        var context = CreateContext(currencyId);
        context.CurrencyNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<CurrencyName, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CurrencyName?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.RemoveAsync(currencyId, languageId));

        context.CurrencyNameRepository.Verify(repository => repository.RemoveAsync(
            It.IsAny<CurrencyName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region TestHelpers

    private static TestContext CreateContext(string currencyId, bool currencyExists = true)
    {
        var currencyRepository = new Mock<ICurrencyRepository>();
        currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<CurrencyDetailsResponse>(
                currencyId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(currencyExists ? new CurrencyDetailsResponse { Id = currencyId } : null);

        var currencyNameRepository = new Mock<ICurrencyNameRepository>();
        var languageRepository = new Mock<ILanguageRepository>();
        languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string languageId, CancellationToken _) => new Language { Id = languageId });
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.SetupGet(work => work.Currency).Returns(currencyRepository.Object);
        unitOfWork.SetupGet(work => work.CurrencyName).Returns(currencyNameRepository.Object);
        unitOfWork.SetupGet(work => work.Language).Returns(languageRepository.Object);

        var mapper = new Mock<IMapper>();
        return new TestContext(
            new CurrencyNameService(unitOfWork.Object, mapper.Object),
            currencyNameRepository,
            languageRepository,
            unitOfWork,
            mapper);
    }

    private sealed record TestContext(
        CurrencyNameService Service,
        Mock<ICurrencyNameRepository> CurrencyNameRepository,
        Mock<ILanguageRepository> LanguageRepository,
        Mock<IReferenceDataUnitOfWork> UnitOfWork,
        Mock<IMapper> Mapper);

    #endregion
}
