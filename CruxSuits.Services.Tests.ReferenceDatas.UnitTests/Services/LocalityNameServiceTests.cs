using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class LocalityNameServiceTests
{
    #region GetAllAsync

    // Failure case: verifies GetAllAsync throws not found when locality does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetAllAsync_WhenLocalityDoesNotExist_ThrowsNotFound()
    {
        const string localityId = "TN-TU-ZZZ";
        var context = CreateContext(localityId, areaExists: false);

        await Assert.ThrowsAsync<CxException>(() => context.Service.GetAllAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6)));

        context.LocalityNameRepository.Verify(repository => repository.GetAllAsync<LocalityNameResponse>(
            It.IsAny<Expression<Func<LocalityName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    // Success case: verifies GetAllAsync returns empty list when locality has no translations;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenLocalityHasNoTranslations_ReturnsEmptyList()
    {
        const string localityId = "TN-TU-ZQA";
        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetAllAsync<LocalityNameResponse>(
                It.IsAny<Expression<Func<LocalityName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await context.Service.GetAllAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6));

        Assert.Empty(result);
        context.LocalityNameRepository.Verify(repository => repository.GetAllAsync<LocalityNameResponse>(
            It.Is<Expression<Func<LocalityName, bool>>>(predicate =>
                predicate.Compile()(new LocalityName { LocalityId = localityId })
                && !predicate.Compile()(new LocalityName { LocalityId = "TN-TU-BRD" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Success case: verifies GetAllAsync returns all translations for that locality when locality has translations;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenLocalityHasTranslations_ReturnsAllTranslationsForThatLocality()
    {
        const string localityId = "TN-TU-BRD";
        var expectedNames = new List<LocalityNameResponse>
        {
            new() { LanguageId = "en", Name = "Le Bardo" },
            new() { LanguageId = "fr", Name = "Le Bardo" },
            new() { LanguageId = "ar", Name = "باردو" }
        };

        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetAllAsync<LocalityNameResponse>(
                It.IsAny<Expression<Func<LocalityName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<LocalityName, bool>> predicate, CancellationToken _) =>
                predicate.Compile()(new LocalityName { LocalityId = localityId })
                    ? expectedNames
                    : new List<LocalityNameResponse>());

        var result = await context.Service.GetAllAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6));

        Assert.Equal(expectedNames, result);
        Assert.Collection(
            result,
            name => Assert.Equal(("en", "Le Bardo"),
                (name.LanguageId, name.Name)),
            name => Assert.Equal(("fr", "Le Bardo"),
                (name.LanguageId, name.Name)),
            name => Assert.Equal(("ar", "باردو"),
                (name.LanguageId, name.Name)));

        context.LocalityNameRepository.Verify(repository => repository.GetAllAsync<LocalityNameResponse>(
            It.Is<Expression<Func<LocalityName, bool>>>(predicate =>
                predicate.Compile()(new LocalityName { LocalityId = localityId })
                && !predicate.Compile()(new LocalityName { LocalityId = "TN-TU-MRS" })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region GetByLanguageAsync

    // Success case: verifies GetByLanguageAsync returns that locality name when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByLanguageAsync_WhenTranslationExists_ReturnsThatLocalityName()
    {
        const string localityId = "TN-TU-BRD";
        const string languageId = "fr";
        var expectedName = new LocalityNameResponse
        {
            LanguageId = languageId,
            LanguageName = "French",
            Name = "Le Bardo"
        };

        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync<LocalityNameResponse>(
                It.IsAny<Expression<Func<LocalityName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedName);

        var result = await context.Service.GetByLanguageAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), languageId);

        Assert.Equal(expectedName, result);
        context.LocalityNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync<LocalityNameResponse>(
            It.Is<Expression<Func<LocalityName, bool>>>(predicate =>
                predicate.Compile()(new LocalityName { LocalityId = localityId, LanguageId = languageId })
                && !predicate.Compile()(new LocalityName { LocalityId = localityId, LanguageId = "en" })
                && !predicate.Compile()(new LocalityName { LocalityId = "TN-TU-MRS", LanguageId = languageId })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies GetByLanguageAsync throws not found when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByLanguageAsync_WhenTranslationDoesNotExist_ThrowsNotFound()
    {
        const string localityId = "TN-TU-BRD";
        const string languageId = "fr";
        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync<LocalityNameResponse>(
                It.IsAny<Expression<Func<LocalityName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((LocalityNameResponse?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.GetByLanguageAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), languageId));

        context.LocalityNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync<LocalityNameResponse>(
            It.Is<Expression<Func<LocalityName, bool>>>(predicate =>
                predicate.Compile()(new LocalityName { LocalityId = localityId, LanguageId = languageId })
                && !predicate.Compile()(new LocalityName { LocalityId = localityId, LanguageId = "en" })
                && !predicate.Compile()(new LocalityName { LocalityId = "TN-TU-MRS", LanguageId = languageId })),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region CreateAsync

    // Success case: verifies CreateAsync adds and returns locality name when locality-language pair is new;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_WhenLocalityLanguagePairIsNew_AddsAndReturnsLocalityName()
    {
        const string localityId = "TN-TU-BRD";
        var request = new LocalityNameRequest
        {
            LanguageId = "fr",
            Name = "Le Bardo"
        };
        var expected = new LocalityNameResponse
        {
            LanguageId = "fr",
            LanguageName = "French (Français)",
            Name = request.Name
        };
        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetAllAsync<LocalityName>(
                It.IsAny<Expression<Func<LocalityName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        var entity = new LocalityName
        {
            LocalityId = localityId,
            LanguageId = request.LanguageId,
            Name = request.Name
        };
        context.Mapper.Setup(mapper => mapper.Map<LocalityName>(request)).Returns(entity);
        context.LocalityNameRepository
            .Setup(repository => repository.AddAsync(It.IsAny<LocalityName>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);
        context.Mapper.Setup(mapper => mapper.Map<LocalityNameResponse>(entity)).Returns(expected);

        var result = await context.Service.CreateAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), request);

        Assert.Equal(expected, result);
        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(
            request.LanguageId, It.IsAny<CancellationToken>()), Times.Once);
        context.LocalityNameRepository.Verify(repository => repository.AddAsync(
            It.Is<LocalityName>(name => name.LocalityId == localityId
                && name.LanguageId == request.LanguageId
                && name.Name == request.Name),
            It.IsAny<CancellationToken>()), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies CreateAsync throws not found without adding when locality does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenLocalityDoesNotExist_ThrowsNotFoundWithoutAdding()
    {
        const string localityId = "TN-TU-ZZZ";
        var request = new LocalityNameRequest
        {
            LanguageId = "fr",
            Name = "Unknown locality"
        };
        var context = CreateContext(localityId, areaExists: false);

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), request));

        context.LocalityNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<LocalityName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
        context.LocalityNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<LocalityName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync throws bad request without adding when language does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenLanguageDoesNotExist_ThrowsBadRequestWithoutAdding()
    {
        const string localityId = "TN-TU-BRD";
        const string languageId = "zz";
        var request = new LocalityNameRequest
        {
            LanguageId = languageId,
            Name = "Unknown language"
        };
        var context = CreateContext(localityId);
        context.LanguageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Language?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), request));

        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()), Times.Once);
        context.LocalityNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<LocalityName, bool>>>(),
            It.IsAny<CancellationToken>()), Times.Never);
        context.LocalityNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<LocalityName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync rejects a duplicate translation for the same locality and language;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenLocalityAlreadyHasTranslationInLanguage_RejectsDuplicate()
    {
        const string localityId = "TN-TU-BRD";
        var request = new LocalityNameRequest
        {
            LanguageId = "en",
            Name = "Le Bardo"
        };
        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<LocalityName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new LocalityName
            {
                LocalityId = localityId,
                LanguageId = request.LanguageId,
                Name = request.Name,
            });

        await Assert.ThrowsAsync<CxException>(() => context.Service.CreateAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), request));

        context.LanguageRepository.Verify(repository => repository.GetByIdOrDefaultAsync(
            request.LanguageId, It.IsAny<CancellationToken>()), Times.Once);
        context.LocalityNameRepository.Verify(repository => repository.AddAsync(
            It.IsAny<LocalityName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region UpdateAsync

    // Success case: verifies UpdateAsync updates the name and preserves locality and language when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task UpdateAsync_WhenTranslationExists_UpdatesNameAndPreservesLocalityAndLanguage()
    {
        const string localityId = "TN-TU-BRD";
        const string languageId = "en";
        var request = new LocalityNameUpdateRequest
        {
            Name = "Greater Bardo"
        };
        var existing = new LocalityName
        {
            LocalityId = localityId,
            LanguageId = languageId,
            Name = "Le Bardo"
        };
        var response = new LocalityNameResponse
        {
            LanguageId = languageId,
            LanguageName = "English (English)",
            Name = request.Name
        };
        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<LocalityName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        context.Mapper
            .Setup(mapper => mapper.Map(request, existing))
            .Callback(() => existing.Name = request.Name)
            .Returns(existing);
        context.LocalityNameRepository
            .Setup(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        context.Mapper.Setup(mapper => mapper.Map<LocalityNameResponse>(existing)).Returns(response);

        var result = await context.Service.UpdateAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), languageId, request);

        Assert.Same(response, result);
        Assert.Equal(localityId, existing.LocalityId);
        Assert.Equal(languageId, existing.LanguageId);
        Assert.Equal(request.Name, existing.Name);
        context.LocalityNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.Is<Expression<Func<LocalityName, bool>>>(predicate =>
                predicate.Compile()(new LocalityName { LocalityId = localityId, LanguageId = languageId })
                && !predicate.Compile()(new LocalityName { LocalityId = "TN-TU-MRS", LanguageId = languageId })
                && !predicate.Compile()(new LocalityName { LocalityId = localityId, LanguageId = "fr" })),
            It.IsAny<CancellationToken>()), Times.Once);
        context.LocalityNameRepository.Verify(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies UpdateAsync throws not found without updating when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task UpdateAsync_WhenTranslationDoesNotExist_ThrowsNotFoundWithoutUpdating()
    {
        const string localityId = "TN-TU-BRD";
        const string languageId = "en";
        var request = new LocalityNameUpdateRequest { Name = "Greater Bardo" };
        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<LocalityName, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((LocalityName?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.UpdateAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), languageId, request));

        context.LocalityNameRepository.Verify(repository => repository.UpdateAsync(
            It.IsAny<LocalityName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        context.Mapper.Verify(mapper => mapper.Map(
            It.IsAny<LocalityNameUpdateRequest>(), It.IsAny<LocalityName>()), Times.Never);
    }

    #endregion

    #region RemoveAsync

    // Success case: verifies RemoveAsync removes and saves it when translation exists;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_WhenTranslationExists_RemovesAndSavesIt()
    {
        const string localityId = "TN-TU-BRD";
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var localityName = new LocalityName { LocalityId = localityId, LanguageId = languageId };
        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<LocalityName, bool>>>(), cancellation.Token))
            .ReturnsAsync(localityName);

        await context.Service.RemoveAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), languageId, cancellation.Token);

        context.LocalityNameRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.Is<Expression<Func<LocalityName, bool>>>(predicate =>
                predicate.Compile()(new LocalityName { LocalityId = localityId, LanguageId = languageId })
                && !predicate.Compile()(new LocalityName { LocalityId = "TN-TU-MRS", LanguageId = languageId })
                && !predicate.Compile()(new LocalityName { LocalityId = localityId, LanguageId = "fr" })),
            cancellation.Token), Times.Once);
        context.LocalityNameRepository.Verify(repository => repository.RemoveAsync(localityName, cancellation.Token), Times.Once);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(cancellation.Token), Times.Once);
    }

    // Failure case: verifies RemoveAsync throws not found without removing when translation does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task RemoveAsync_WhenTranslationDoesNotExist_ThrowsNotFoundWithoutRemoving()
    {
        const string localityId = "TN-TU-BRD";
        const string languageId = "en";
        var context = CreateContext(localityId);
        context.LocalityNameRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<LocalityName, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((LocalityName?)null);

        await Assert.ThrowsAsync<CxException>(() => context.Service.RemoveAsync(localityId.Substring(0, 2), localityId.Substring(3, 2), localityId.Substring(6), languageId));

        context.LocalityNameRepository.Verify(repository => repository.RemoveAsync(
            It.IsAny<LocalityName>(), It.IsAny<CancellationToken>()), Times.Never);
        context.UnitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region TestHelpers

    private static TestContext CreateContext(string localityId, bool areaExists = true)
    {
        var localityRepository = new Mock<ILocalityRepository>();
        var countryId = localityId[..2];
        var areaCode = localityId[3..5];
        var administrativeAreaId = localityId[..5];
        var localityCode = localityId[6..];
        var areaRepository = new Mock<IAdministrativeAreaRepository>();
        areaRepository.Setup(repository => repository.BuildId(countryId, areaCode)).Returns(administrativeAreaId);
        areaRepository.Setup(repository => repository.GetByIdOrDefaultAsync(administrativeAreaId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AdministrativeArea { Id = administrativeAreaId });
        localityRepository.Setup(repository => repository.BuildId(administrativeAreaId, localityCode))
            .Returns(localityId);
        localityRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<LocalityDetailsResponse>(
                localityId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(areaExists ? new LocalityDetailsResponse { Id = localityId } : null);

        var localityNameRepository = new Mock<ILocalityNameRepository>();
        var languageRepository = new Mock<ILanguageRepository>();
        languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string languageId, CancellationToken _) => new Language { Id = languageId });
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.SetupGet(work => work.AdministrativeArea).Returns(areaRepository.Object);
        unitOfWork.SetupGet(work => work.Locality).Returns(localityRepository.Object);
        unitOfWork.SetupGet(work => work.LocalityName).Returns(localityNameRepository.Object);
        unitOfWork.SetupGet(work => work.Language).Returns(languageRepository.Object);

        var mapper = new Mock<IMapper>();
        return new TestContext(
            new LocalityNameService(unitOfWork.Object, mapper.Object),
            localityNameRepository,
            languageRepository,
            unitOfWork,
            mapper);
    }

    private sealed record TestContext(
        LocalityNameService Service,
        Mock<ILocalityNameRepository> LocalityNameRepository,
        Mock<ILanguageRepository> LanguageRepository,
        Mock<IReferenceDataUnitOfWork> UnitOfWork,
        Mock<IMapper> Mapper);

    #endregion
}
