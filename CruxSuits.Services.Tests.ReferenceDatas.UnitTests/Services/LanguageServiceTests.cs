using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Localization;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Moq;
using Xunit;

namespace CruxSuits.Services.Tests.UnitTests.Services;

public sealed class LanguageServiceTests
{
    private readonly Mock<IReferenceDataUnitOfWork> _unitOfWork = new();
    private readonly Mock<ILanguageRepository> _languageRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<ILanguageCatalogProvider> _languageCatalogProvider = new();
    private readonly LanguageService _service;

    public LanguageServiceTests()
    {
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.Language).Returns(_languageRepository.Object);
        _service = new LanguageService(_unitOfWork.Object, _mapper.Object, _languageCatalogProvider.Object);
    }

    #region GetAllAsync

    // Success case: verifies GetAllAsync returns all languages from the repository;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_ReturnsLanguages()
    {
        using var cancellation = new CancellationTokenSource();
        var languages = new List<LanguageResponse>
        {
            CreateLanguageResponse("en", "English", "English", isRightToLeft: false),
            CreateLanguageResponse("ar", "Arabic", "العربية", isRightToLeft: true)
        };
        _languageRepository
            .Setup(repository => repository.GetAllAsync<LanguageResponse>(null, cancellation.Token))
            .ReturnsAsync(languages);

        var result = await _service.GetAllAsync(cancellation.Token);

        Assert.Same(languages, result);
        _languageRepository.Verify(repository => repository.GetAllAsync<LanguageResponse>(null, cancellation.Token), Times.Once);
    }

    #endregion

    #region GetByIdAsync

    // Success case: verifies GetByIdAsync returns language details when found;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByIdAsync_ReturnsLanguageDetailsWhenFound()
    {
        const string languageId = "ar";
        using var cancellation = new CancellationTokenSource();
        var language = CreateLanguageDetailsResponse(languageId, "Arabic", "العربية", isRightToLeft: true);
        _languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<LanguageDetailsResponse>(languageId, cancellation.Token))
            .ReturnsAsync(language);

        var result = await _service.GetByIdAsync(languageId, cancellation.Token);

        Assert.Same(language, result);
        _languageRepository.Verify(repository => repository.GetByIdOrDefaultAsync<LanguageDetailsResponse>(
            languageId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies GetByIdAsync throws not found when the language does not exist;
    // A pass confirms the missing case is handled;
    // Failure means the request was accepted or mishandled.
    [Fact]
    public async Task GetByIdAsync_ThrowsNotFoundWhenLanguageDoesNotExist()
    {
        const string languageId = "ZZ";
        _languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<LanguageDetailsResponse>(
                languageId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((LanguageDetailsResponse?)null);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.GetByIdAsync(languageId));

        Assert.Equal(CxHttpStatusCode.NotFound, exception.StatusCode);
        _languageRepository.Verify(repository => repository.GetByIdOrDefaultAsync<LanguageDetailsResponse>(
            languageId, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region CreateAsync

    // Success case: verifies CreateAsync adds and saves a new language;
    // A pass confirms the language is persisted and returned;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_AddsAndReturnsLanguageWhenIdIsNew()
    {
        var request = new LanguageRequest
        {
            Id = "QX",
            EnglishName = "Test language",
            NativeName = "Test",
            IsRightToLeft = false
        };
        var language = new Language
        {
            Id = request.Id,
            EnglishName = request.EnglishName,
            NativeName = request.NativeName,
            IsRightToLeft = request.IsRightToLeft
        };
        var response = CreateLanguageResponse(request.Id, request.EnglishName, request.NativeName, request.IsRightToLeft);
        using var cancellation = new CancellationTokenSource();
        _mapper.Setup(mapper => mapper.Map<Language>(request)).Returns(language);
        _languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(request.Id, cancellation.Token))
            .ReturnsAsync((Language?)null);
        _languageRepository.Setup(repository => repository.AddAsync(language, cancellation.Token)).ReturnsAsync(language);
        _mapper.Setup(mapper => mapper.Map<LanguageResponse>(language)).Returns(response);
        var sequence = new MockSequence();
        _unitOfWork.InSequence(sequence)
            .Setup(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token))
            .ReturnsAsync(1);
        _languageCatalogProvider.InSequence(sequence)
            .Setup(provider => provider.RefreshAsync(cancellation.Token))
            .Returns(Task.CompletedTask);

        var result = await _service.CreateAsync(request, cancellation.Token);

        Assert.Same(response, result);
        _languageRepository.Verify(repository => repository.AddAsync(language, cancellation.Token), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token), Times.Once);
        _languageCatalogProvider.Verify(provider => provider.RefreshAsync(cancellation.Token), Times.Once);
    }

    // Failure case: verifies CreateAsync throws conflict and does not persist a duplicate language;
    // A pass confirms duplicate IDs are rejected;
    // Failure means the duplicate was added or the conflict was not reported.
    [Fact]
    public async Task CreateAsync_WhenLanguageIdAlreadyExists_ThrowsConflictWithoutAdding()
    {
        var request = new LanguageRequest
        {
            Id = "en",
            EnglishName = "English",
            NativeName = "English",
            IsRightToLeft = false
        };
        var existingLanguage = new Language { Id = "en", EnglishName = "English", NativeName = "English" };
        _mapper.Setup(mapper => mapper.Map<Language>(request)).Returns(new Language { Id = request.Id });
        _languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(request.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingLanguage);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync(request));

        Assert.Equal(CxHttpStatusCode.Conflict, exception.StatusCode);
        _languageRepository.Verify(repository => repository.AddAsync(It.IsAny<Language>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _languageCatalogProvider.Verify(provider => provider.RefreshAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region UpdateAsync

    // Success case: verifies UpdateAsync updates and saves an existing language;
    // A pass confirms the expected values are returned;
    // Failure signals a behavior regression.
    [Fact]
    public async Task UpdateAsync_UpdatesAndReturnsLanguageWhenFound()
    {
        const string languageId = "QX";
        using var cancellation = new CancellationTokenSource();
        var language = new Language
        {
            Id = languageId,
            EnglishName = "Test language",
            NativeName = "Test",
            IsRightToLeft = false
        };
        var request = new UpdateLanguageRequest
        {
            EnglishName = "Updated language",
            NativeName = "Updated",
            IsRightToLeft = true
        };
        var response = CreateLanguageResponse(languageId, request.EnglishName, request.NativeName, request.IsRightToLeft);
        _languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(languageId, cancellation.Token))
            .ReturnsAsync(language);
        _mapper.Setup(mapper => mapper.Map(request, language)).Callback(() =>
        {
            language.EnglishName = request.EnglishName;
            language.NativeName = request.NativeName;
            language.IsRightToLeft = request.IsRightToLeft;
        });
        _languageRepository.Setup(repository => repository.UpdateAsync(language, cancellation.Token)).ReturnsAsync(language);
        _mapper.Setup(mapper => mapper.Map<LanguageResponse>(language)).Returns(response);
        var sequence = new MockSequence();
        _unitOfWork.InSequence(sequence)
            .Setup(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token))
            .ReturnsAsync(1);
        _languageCatalogProvider.InSequence(sequence)
            .Setup(provider => provider.RefreshAsync(cancellation.Token))
            .Returns(Task.CompletedTask);

        var result = await _service.UpdateAsync(languageId, request, cancellation.Token);

        Assert.Same(response, result);
        Assert.Equal(languageId, language.Id);
        _languageRepository.Verify(repository => repository.UpdateAsync(language, cancellation.Token), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token), Times.Once);
        _languageCatalogProvider.Verify(provider => provider.RefreshAsync(cancellation.Token), Times.Once);
    }

    // Failure case: verifies UpdateAsync throws not found without updating when the language is missing;
    // A pass confirms the missing case is handled;
    // Failure means an unknown language was accepted or mishandled.
    [Fact]
    public async Task UpdateAsync_WhenLanguageDoesNotExist_ThrowsNotFoundWithoutUpdating()
    {
        const string languageId = "ZZ";
        var request = new UpdateLanguageRequest
        {
            EnglishName = "Updated language",
            NativeName = "Updated",
            IsRightToLeft = true
        };
        _languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Language?)null);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.UpdateAsync(languageId, request));

        Assert.Equal(CxHttpStatusCode.NotFound, exception.StatusCode);
        _languageRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Language>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _languageCatalogProvider.Verify(provider => provider.RefreshAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region RemoveAsync

    // Success case: verifies RemoveAsync removes and saves an existing language;
    // A pass confirms the language is removed;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_RemovesLanguageWhenFound()
    {
        const string languageId = "QX";
        using var cancellation = new CancellationTokenSource();
        var language = new Language { Id = languageId };
        _languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(languageId, cancellation.Token))
            .ReturnsAsync(language);
        _languageRepository.Setup(repository => repository.RemoveAsync(language, cancellation.Token)).Returns(Task.CompletedTask);
        var sequence = new MockSequence();
        _unitOfWork.InSequence(sequence)
            .Setup(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token))
            .ReturnsAsync(1);
        _languageCatalogProvider.InSequence(sequence)
            .Setup(provider => provider.RefreshAsync(cancellation.Token))
            .Returns(Task.CompletedTask);

        await _service.RemoveAsync(languageId, cancellation.Token);

        _languageRepository.Verify(repository => repository.RemoveAsync(language, cancellation.Token), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token), Times.Once);
        _languageCatalogProvider.Verify(provider => provider.RefreshAsync(cancellation.Token), Times.Once);
    }

    // Failure case: verifies RemoveAsync throws not found and does not save when the language is missing;
    // A pass confirms the missing case is handled;
    // Failure means an unknown language was accepted or mishandled.
    [Fact]
    public async Task RemoveAsync_WhenLanguageDoesNotExist_ThrowsNotFoundWithoutRemoving()
    {
        const string languageId = "ZZ";
        _languageRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(languageId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Language?)null);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.RemoveAsync(languageId));

        Assert.Equal(CxHttpStatusCode.NotFound, exception.StatusCode);
        _languageRepository.Verify(repository => repository.RemoveAsync(It.IsAny<Language>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _languageCatalogProvider.Verify(provider => provider.RefreshAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region TestHelpers

    private static LanguageResponse CreateLanguageResponse(string id, string englishName, string nativeName, bool isRightToLeft) =>
        new()
        {
            Id = id,
            EnglishName = englishName,
            NativeName = nativeName,
            IsRightToLeft = isRightToLeft
        };

    private static LanguageDetailsResponse CreateLanguageDetailsResponse(string id, string englishName, string nativeName, bool isRightToLeft) =>
        new()
        {
            Id = id,
            EnglishName = englishName,
            NativeName = nativeName,
            IsRightToLeft = isRightToLeft
        };

    #endregion
}
