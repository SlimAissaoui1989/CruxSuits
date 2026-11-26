using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeAreaName;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class AdministrativeAreaNameServiceTests
{
    #region GetAllAsync

    // Failure case: verifies GetAllAsync throws not found when administrative area does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetAllAsync_WhenAdministrativeAreaDoesNotExist_ThrowsNotFound()
    {
        const string administrativeAreaId = "TN-ZZ";
        var context = CreateContext(administrativeAreaId, areaExists: false);

        await Assert.ThrowsAsync<CxException>(() => context.Service.GetAllAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3)));

        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetAllAsync<AdministrativeAreaNameResponse>(
            It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    // Success case: verifies GetAllAsync returns empty list when administrative area has no translations;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenAdministrativeAreaHasNoTranslations_ReturnsEmptyList()
    {
        const string administrativeAreaId = "TN-QZ";
        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetAllAsync<AdministrativeAreaNameResponse>(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await context.Service.GetAllAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3));

        Assert.Empty(result);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetAllAsync<AdministrativeAreaNameResponse>(
            It.Is<Expression<Func<AdministrativeAreaName, bool>>>(predicate =>
                predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = "TN-SF" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Success case: verifies GetAllAsync returns all translations for that administrative area when administrative area has translations;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenAdministrativeAreaHasTranslations_ReturnsAllTranslationsForThatAdministrativeArea()
    {
        const string administrativeAreaId = "TN-SF";
        var expectedNames = new List<AdministrativeAreaNameResponse>
        {
            new() { LanguageId = "en", Name = "Sfax" },
            new() { LanguageId = "fr", Name = "Sfax" },
            new() { LanguageId = "ar", Name = "صفاقس" }
        };

        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetAllAsync<AdministrativeAreaNameResponse>(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<AdministrativeAreaName, bool>> predicate, CancellationToken _) =>
                predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId })
                    ? expectedNames
                    : new List<AdministrativeAreaNameResponse>());

        var result = await context.Service.GetAllAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3));

        Assert.Equal(expectedNames, result);
        Assert.Collection(
            result,
            name => Assert.Equal(("en", "Sfax"),
                (name.LanguageId, name.Name)),
            name => Assert.Equal(("fr", "Sfax"),
                (name.LanguageId, name.Name)),
            name => Assert.Equal(("ar", "صفاقس"),
                (name.LanguageId, name.Name)));

        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetAllAsync<AdministrativeAreaNameResponse>(
            It.Is<Expression<Func<AdministrativeAreaName, bool>>>(predicate =>
                predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = "TN-GA" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region GetByLanguageAsync

    // Success case: verifies GetByLanguageAsync returns that administrative area name when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByLanguageAsync_WhenTranslationExists_ReturnsThatAdministrativeAreaName()
    {
        const string administrativeAreaId = "TN-SF";
        const string languageId = "fr";
        var expectedName = new AdministrativeAreaNameResponse
        {
            LanguageId = languageId,
            LanguageName = "French",
            Name = "Sfax"
        };

        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync<AdministrativeAreaNameResponse>(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedName);

        var result = await context.Service.GetByLanguageAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), languageId);

        Assert.Equal(expectedName, result);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync<AdministrativeAreaNameResponse>(
            It.Is<Expression<Func<AdministrativeAreaName, bool>>>(predicate =>
                predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = languageId })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = "en" })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = "TN-GA", LanguageId = languageId })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies GetByLanguageAsync throws not found when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByLanguageAsync_WhenTranslationDoesNotExist_ThrowsNotFound()
    {
        const string administrativeAreaId = "TN-SF";
        const string languageId = "fr";
        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync<AdministrativeAreaNameResponse>(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((AdministrativeAreaNameResponse?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.GetByLanguageAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), languageId));

        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync<AdministrativeAreaNameResponse>(
            It.Is<Expression<Func<AdministrativeAreaName, bool>>>(predicate =>
                predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = languageId })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = "en" })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = "TN-GA", LanguageId = languageId })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region CreateAsync

    // Success case: verifies CreateAsync adds and returns administrative area name when administrative area-language pair is new;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_WhenAdministrativeAreaLanguagePairIsNew_AddsAndReturnsAdministrativeAreaName()
    {
        const string administrativeAreaId = "TN-SF";
        var request = new AdministrativeAreaNameRequest
        {
            LanguageId = "fr",
            Name = "Sfax"
        };
        var expected = new AdministrativeAreaNameResponse
        {
            LanguageId = "fr",
            LanguageName = "French (Français)",
            Name = request.Name
        };
        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetAllAsync<AdministrativeAreaName>(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        var entity = new AdministrativeAreaName
        {
            AdministrativeAreaId = administrativeAreaId,
            LanguageId = request.LanguageId,
            Name = request.Name
        };
        context.Mapper.Setup(mapper => mapper.Map<AdministrativeAreaName>(request)).Returns(entity);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.AddAsync(It.IsAny<AdministrativeAreaName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);
        context.Mapper.Setup(mapper => mapper.Map<AdministrativeAreaNameResponse>(entity)).Returns(expected);

        var result = await context.Service.CreateAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), request);

        Assert.Equal(expected, result);
        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(
            request.LanguageId, It.IsAny<CancellationToken>()), Times.Once);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.AddAsync(
            It.Is<AdministrativeAreaName>(name => name.AdministrativeAreaId == administrativeAreaId
                && name.LanguageId == request.LanguageId
                && name.Name == request.Name),
            It.IsAny<CancellationToken>()), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies CreateAsync throws not found without adding when administrative area does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenAdministrativeAreaDoesNotExist_ThrowsNotFoundWithoutAdding()
    {
        const string administrativeAreaId = "TN-ZZ";
        var request = new AdministrativeAreaNameRequest
        {
            LanguageId = "fr",
            Name = "Unknown administrative area"
        };
        var context = CreateContext(administrativeAreaId, areaExists: false);

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), request));

        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<AdministrativeAreaName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync throws bad request without adding when language does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenLanguageDoesNotExist_ThrowsBadRequestWithoutAdding()
    {
        const string administrativeAreaId = "TN-SF";
        const string languageId = "zz";
        var request = new AdministrativeAreaNameRequest
        {
            LanguageId = languageId,
            Name = "Unknown language"
        };
        var context = CreateContext(administrativeAreaId);
        context.LanguageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Language?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), request));

        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()), Times.Once);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<AdministrativeAreaName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync rejects a duplicate translation for the same administrative area and language;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenAdministrativeAreaAlreadyHasTranslationInLanguage_RejectsDuplicate()
    {
        const string administrativeAreaId = "TN-SF";
        var request = new AdministrativeAreaNameRequest
        {
            LanguageId = "en",
            Name = "Sfax"
        };
        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AdministrativeAreaName
            {
                AdministrativeAreaId = administrativeAreaId,
                LanguageId = request.LanguageId,
                Name = request.Name,
            });

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), request));

        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(
            request.LanguageId, It.IsAny<CancellationToken>()), Times.Once);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<AdministrativeAreaName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region UpdateAsync

    // Success case: verifies UpdateAsync updates the name and preserves administrative area and language when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task UpdateAsync_WhenTranslationExists_UpdatesNameAndPreservesAdministrativeAreaAndLanguage()
    {
        const string administrativeAreaId = "TN-SF";
        const string languageId = "en";
        var request = new AdministrativeAreaNameUpdateRequest
        {
            Name = "Greater Sfax"
        };
        var existing = new AdministrativeAreaName
        {
            AdministrativeAreaId = administrativeAreaId,
            LanguageId = languageId,
            Name = "Sfax"
        };
        var response = new AdministrativeAreaNameResponse
        {
            LanguageId = languageId,
            LanguageName = "English (English)",
            Name = request.Name
        };
        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        context.Mapper
            .Setup(mapper => mapper.Map(request, existing))
            .Callback(() => existing.Name = request.Name)
            .Returns(existing);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        context.Mapper.Setup(mapper => mapper.Map<AdministrativeAreaNameResponse>(existing)).Returns(response);

        var result = await context.Service.UpdateAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), languageId, request);

        Assert.Same(response, result);
        Assert.Equal(administrativeAreaId, existing.AdministrativeAreaId);
        Assert.Equal(languageId, existing.LanguageId);
        Assert.Equal(request.Name, existing.Name);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.Is<Expression<Func<AdministrativeAreaName, bool>>>(predicate =>
                predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = languageId })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = "TN-GA", LanguageId = languageId })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = "fr" })),
            It.IsAny<CancellationToken>()), Times.Once);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies UpdateAsync throws not found without updating when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task UpdateAsync_WhenTranslationDoesNotExist_ThrowsNotFoundWithoutUpdating()
    {
        const string administrativeAreaId = "TN-SF";
        const string languageId = "en";
        var request = new AdministrativeAreaNameUpdateRequest { Name = "Greater Sfax" };
        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((AdministrativeAreaName?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.UpdateAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), languageId, request));

        context.AdministrativeAreaNameRepository.Verify(repository => repository.UpdateAsync(
            It.IsAny<AdministrativeAreaName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        context.Mapper.Verify(mapper => mapper.Map(
            It.IsAny<AdministrativeAreaNameUpdateRequest>(), It.IsAny<AdministrativeAreaName>()), Times.Never);
    }

    #endregion

    #region RemoveAsync

    // Success case: verifies RemoveAsync removes and saves it when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_WhenTranslationExists_RemovesAndSavesIt()
    {
        const string administrativeAreaId = "TN-SF";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var administrativeAreaName = new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = languageId };
        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(), cancellation.Token))
            .ReturnsAsync(administrativeAreaName);

        await context.Service.RemoveAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), languageId, cancellation.Token);

        context.AdministrativeAreaNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.Is<Expression<Func<AdministrativeAreaName, bool>>>(predicate =>
                predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = languageId })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = "TN-GA", LanguageId = languageId })
                && !predicate.Compile()(new AdministrativeAreaName { AdministrativeAreaId = administrativeAreaId, LanguageId = "fr" })),
            cancellation.Token), Times.Once);
        context.AdministrativeAreaNameRepository.Verify(repository => repository.RemoveAsync(administrativeAreaName, cancellation.Token), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(cancellation.Token), Times.Once);
    }

    // Failure case: verifies RemoveAsync throws not found without removing when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task RemoveAsync_WhenTranslationDoesNotExist_ThrowsNotFoundWithoutRemoving()
    {
        const string administrativeAreaId = "TN-SF";
        const string languageId = "en";
        var context = CreateContext(administrativeAreaId);
        context.AdministrativeAreaNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<AdministrativeAreaName, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AdministrativeAreaName?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.RemoveAsync(administrativeAreaId.Substring(0, 2), administrativeAreaId.Substring(3), languageId));

        context.AdministrativeAreaNameRepository.Verify(repository => repository.RemoveAsync(
            It.IsAny<AdministrativeAreaName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region TestHelpers

    private static TestContext CreateContext(string administrativeAreaId, bool areaExists = true)
    {
        var administrativeAreaRepository = new Mock<IAdministrativeAreaRepository>();
        var countryId = administrativeAreaId[..2];
        var areaCode = administrativeAreaId[3..];
        administrativeAreaRepository.Setup(repository => repository.BuildId(countryId, areaCode))
            .Returns(administrativeAreaId);
        administrativeAreaRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<AdministrativeAreaDetailsResponse>(
                administrativeAreaId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(areaExists ? new AdministrativeAreaDetailsResponse { Id = administrativeAreaId } : null);

        var administrativeAreaNameRepository = new Mock<IAdministrativeAreaNameRepository>();
        var languageRepository = new Mock<ILanguageRepository>();
        languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string languageId, CancellationToken _) => new Language { Id = languageId });
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.SetupGet(work => work.AdministrativeArea).Returns(administrativeAreaRepository.Object);
        unitOfWork.SetupGet(work => work.AdministrativeAreaName).Returns(administrativeAreaNameRepository.Object);
        unitOfWork.SetupGet(work => work.Language).Returns(languageRepository.Object);

        var mapper = new Mock<IMapper>();
        return new TestContext(
            new AdministrativeAreaNameService(unitOfWork.Object, mapper.Object),
            administrativeAreaNameRepository,
            languageRepository,
            unitOfWork,
            mapper);
    }

    private sealed record TestContext(
        AdministrativeAreaNameService Service,
        Mock<IAdministrativeAreaNameRepository> AdministrativeAreaNameRepository,
        Mock<ILanguageRepository> LanguageRepository,
        Mock<IReferenceDataUnitOfWork> UnitOfWork,
        Mock<IMapper> Mapper);

    #endregion
}
