using AutoMapper;
using CruxIT.Library;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class CurrencyServiceTests
{
    private readonly Mock<IReferenceDataUnitOfWork> _unitOfWork = new();
    private readonly Mock<ICurrencyRepository> _currencyRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly CurrencyService _service;

    public CurrencyServiceTests()
    {
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.Currency).Returns(_currencyRepository.Object);
        _service = new CurrencyService(_unitOfWork.Object, _mapper.Object);
    }

    #region CreateAsync

    // Success case: verifies creation adds, saves, and returns the created currency;
    // A pass confirms the valid creation scenario;
    // Failure signals a creation behavior regression.
    [Fact]
    public async Task CreateAsync_WhenCurrencyIsNew_AddsAndReturnsCreatedCurrency()
    {
        using var cancellation = new CancellationTokenSource();
        var request = new CurrencyRequest
        {
            Id = "QXZ",
            NumericCode = "997",
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        var currency = new Currency
        {
            Id = request.Id,
            NumericCode = request.NumericCode,
            Symbol = request.Symbol,
            DecimalSeparator = request.DecimalSeparator,
            MinorUnit = request.MinorUnit
        };
        var response = new CurrencyResponse { Id = currency.Id, NumericCode = currency.NumericCode };

        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(currency.Id, cancellation.Token))
            .ReturnsAsync((Currency?)null);
        _currencyRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Currency, bool>>>(), cancellation.Token))
            .ReturnsAsync((Currency?)null);
        _mapper.Setup(mapper => mapper.Map<Currency>(request)).Returns(currency);
        _currencyRepository.Setup(repository => repository.AddAsync(currency, cancellation.Token)).ReturnsAsync(currency);
        _mapper.Setup(mapper => mapper.Map<CurrencyResponse>(currency)).Returns(response);

        var result = await _service.CreateAsync(request, cancellation.Token);

        Assert.Same(response, result);
        _mapper.Verify(mapper => mapper.Map<Currency>(request), Times.Once);
        _currencyRepository.Verify(repository => repository.AddAsync(currency, cancellation.Token), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token), Times.Once);
        _mapper.Verify(mapper => mapper.Map<CurrencyResponse>(currency), Times.Once);
    }

    // Failure case: verifies creation rejects an existing currency ID as a conflict;
    // A pass confirms duplicate IDs are rejected;
    // Failure means a duplicate currency could be added.
    [Fact]
    public async Task CreateAsync_WhenCurrencyIdAlreadyExists_ThrowsConflictWithoutSaving()
    {
        const string currencyId = "USD";
        var request = new CurrencyRequest
        {
            Id = currencyId,
            NumericCode = "997",
            Symbol = "$",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(currencyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Currency { Id = currencyId });

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync(request));

        Assert.Equal(CxHttpStatusCode.Conflict, exception.StatusCode);
        _currencyRepository.Verify(repository => repository.AddAsync(
            It.IsAny<Currency>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies creation rejects a numeric code already assigned to another currency;
    // A pass confirms numeric-code uniqueness is enforced;
    // Failure means two currencies can receive the same numeric code.
    [Fact]
    public async Task CreateAsync_WhenNumericCodeAlreadyExists_ThrowsConflictWithoutSaving()
    {
        using var cancellation = new CancellationTokenSource();
        var request = new CurrencyRequest
        {
            Id = "QXZ",
            NumericCode = "840",
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        var existingCurrency = new Currency { Id = "USD", NumericCode = request.NumericCode };
        var currency = new Currency
        {
            Id = request.Id,
            NumericCode = request.NumericCode,
            Symbol = request.Symbol,
            DecimalSeparator = request.DecimalSeparator,
            MinorUnit = request.MinorUnit
        };

        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(request.Id, cancellation.Token))
            .ReturnsAsync((Currency?)null);
        _currencyRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.Is<Expression<Func<Currency, bool>>>(predicate => predicate.Compile()(existingCurrency)),
                cancellation.Token))
            .ReturnsAsync(existingCurrency);
        _mapper.Setup(mapper => mapper.Map<Currency>(request)).Returns(currency);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync(request, cancellation.Token));

        Assert.Equal(CxHttpStatusCode.Conflict, exception.StatusCode);
        _currencyRepository.Verify(repository => repository.AddAsync(
            It.IsAny<Currency>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region RemoveAsync

    // Success case: verifies RemoveAsync removes and saves an existing currency;
    // A pass confirms the currency is removed;
    // Failure signals a behavior regression.
    [Fact]
    public async Task RemoveAsync_RemovesCurrencyWhenFound()
    {
        const string currencyId = "QXZ";
        using var cancellation = new CancellationTokenSource();
        var currency = new Currency { Id = currencyId };
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(currencyId, cancellation.Token))
            .ReturnsAsync(currency);

        var sequence = new MockSequence();
        _currencyRepository.InSequence(sequence)
            .Setup(repository => repository.RemoveAsync(currency, cancellation.Token))
            .Returns(Task.CompletedTask);
        _unitOfWork.InSequence(sequence)
            .Setup(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token))
            .ReturnsAsync(1);

        await _service.RemoveAsync(currencyId, cancellation.Token);

        _currencyRepository.Verify(repository => repository.GetByIdOrDefaultAsync(currencyId, cancellation.Token), Times.Once);
        _currencyRepository.Verify(repository => repository.RemoveAsync(currency, cancellation.Token), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token), Times.Once);
    }

    // Failure case: verifies RemoveAsync throws not found and does not remove or save when the currency is missing;
    // A pass confirms the missing case is handled;
    // Failure means an unknown currency was accepted or mishandled.
    [Fact]
    public async Task RemoveAsync_WhenCurrencyDoesNotExist_ThrowsNotFoundWithoutRemoving()
    {
        const string currencyId = "ZZZ";
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(currencyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Currency?)null);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.RemoveAsync(currencyId));

        Assert.Equal(CxHttpStatusCode.NotFound, exception.StatusCode);
        _currencyRepository.Verify(repository => repository.RemoveAsync(It.IsAny<Currency>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region UpdateAsync

    // Success case: verifies UpdateAsync changes currency fields, preserves its ID, saves, and returns details;
    // A pass confirms the update scenario's expected values;
    // Failure signals a behavior regression.
    [Fact]
    public async Task UpdateAsync_UpdatesCurrencyFieldsPreservesIdAndReturnsDetails()
    {
        const string currencyId = "QXZ";
        using var cancellation = new CancellationTokenSource();
        var existingCurrency = new Currency
        {
            Id = currencyId,
            NumericCode = "997",
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        var request = new UpdateCurrencyRequest
        {
            NumericCode = "998",
            Symbol = "Q",
            DecimalSeparator = ',',
            MinorUnit = 0
        };
        var response = new CurrencyDetailsResponse
        {
            Id = currencyId,
            NumericCode = request.NumericCode,
            Symbol = request.Symbol,
            DecimalSeparator = request.DecimalSeparator,
            MinorUnit = request.MinorUnit
        };

        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(currencyId, cancellation.Token))
            .ReturnsAsync(existingCurrency);
        _mapper.Setup(mapper => mapper.Map(request, existingCurrency)).Callback(() =>
        {
            existingCurrency.NumericCode = request.NumericCode;
            existingCurrency.Symbol = request.Symbol;
            existingCurrency.DecimalSeparator = request.DecimalSeparator;
            existingCurrency.MinorUnit = request.MinorUnit;
        });
        _currencyRepository
            .Setup(repository => repository.UpdateAsync(existingCurrency, cancellation.Token))
            .ReturnsAsync(existingCurrency);
        _mapper.Setup(mapper => mapper.Map<CurrencyDetailsResponse>(existingCurrency)).Returns(response);

        var result = await _service.UpdateAsync(currencyId, request, cancellation.Token);

        Assert.Same(response, result);
        Assert.Equal(currencyId, existingCurrency.Id);
        Assert.Equal("998", existingCurrency.NumericCode);
        Assert.Equal("Q", existingCurrency.Symbol);
        Assert.Equal(',', existingCurrency.DecimalSeparator);
        Assert.Equal((byte)0, existingCurrency.MinorUnit);
        _currencyRepository.Verify(repository => repository.GetByIdOrDefaultAsync(currencyId, cancellation.Token), Times.Once);
        _mapper.Verify(mapper => mapper.Map(request, existingCurrency), Times.Once);
        _currencyRepository.Verify(repository => repository.UpdateAsync(existingCurrency, cancellation.Token), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token), Times.Once);
        _mapper.Verify(mapper => mapper.Map<CurrencyDetailsResponse>(existingCurrency), Times.Once);
    }

    // Failure case: verifies UpdateAsync throws NotFound when the currency ID is unknown;
    // A pass confirms the missing-item case is rejected;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task UpdateAsync_ThrowsNotFoundWhenCurrencyDoesNotExist()
    {
        const string currencyId = "ZZZ";
        var request = new UpdateCurrencyRequest
        {
            NumericCode = "997",
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(currencyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Currency?)null);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.UpdateAsync(currencyId, request));

        Assert.Equal(CxHttpStatusCode.NotFound, exception.StatusCode);
        _currencyRepository.Verify(repository => repository.GetByIdOrDefaultAsync(currencyId, It.IsAny<CancellationToken>()), Times.Once);
        _mapper.Verify(mapper => mapper.Map(It.IsAny<UpdateCurrencyRequest>(), It.IsAny<Currency>()), Times.Never);
        _currencyRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Currency>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Success case: verifies an unchanged NumericCode skips the duplicate-code lookup;
    // A pass confirms the unnecessary query is avoided;
    // Failure signals an extra database round trip on this update path.
    [Fact]
    public async Task UpdateAsync_WhenNumericCodeIsUnchanged_SkipsDuplicateCodeLookup()
    {
        const string currencyId = "QXZ";
        const string numericCode = "997";
        var existingCurrency = new Currency
        {
            Id = currencyId,
            NumericCode = numericCode,
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        var request = new UpdateCurrencyRequest
        {
            NumericCode = numericCode,
            Symbol = "Q",
            DecimalSeparator = ',',
            MinorUnit = 0
        };
        var response = new CurrencyDetailsResponse { Id = currencyId, NumericCode = numericCode };

        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(currencyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCurrency);
        _mapper.Setup(mapper => mapper.Map(request, existingCurrency)).Callback(() =>
        {
            existingCurrency.Symbol = request.Symbol;
            existingCurrency.DecimalSeparator = request.DecimalSeparator;
            existingCurrency.MinorUnit = request.MinorUnit;
        });
        _currencyRepository
            .Setup(repository => repository.UpdateAsync(existingCurrency, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCurrency);
        _mapper.Setup(mapper => mapper.Map<CurrencyDetailsResponse>(existingCurrency)).Returns(response);

        await _service.UpdateAsync(currencyId, request);

        _currencyRepository.Verify(repository => repository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Currency, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    // Failure case: verifies UpdateAsync throws Conflict when another currency already uses the requested numeric code;
    // A pass confirms duplicate numeric codes cannot be assigned;
    // Failure means conflicting currency data was accepted.
    [Fact]
    public async Task UpdateAsync_ThrowsConflictWhenNumericCodeBelongsToAnotherCurrency()
    {
        const string currencyId = "QXZ";
        const string duplicateNumericCode = "840";
        var existingCurrency = new Currency
        {
            Id = currencyId,
            NumericCode = "997",
            Symbol = "¤",
            DecimalSeparator = '.',
            MinorUnit = 2
        };
        var otherCurrency = new Currency { Id = "USD", NumericCode = duplicateNumericCode };
        var request = new UpdateCurrencyRequest
        {
            NumericCode = duplicateNumericCode,
            Symbol = "Q",
            DecimalSeparator = ',',
            MinorUnit = 0
        };
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(currencyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingCurrency);
        _mapper.Setup(mapper => mapper.Map(request, existingCurrency)).Callback(() =>
        {
            existingCurrency.NumericCode = request.NumericCode;
            existingCurrency.Symbol = request.Symbol;
            existingCurrency.DecimalSeparator = request.DecimalSeparator;
            existingCurrency.MinorUnit = request.MinorUnit;
        });
        _currencyRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Currency, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<Currency, bool>> predicate, CancellationToken _) =>
                predicate.Compile()(otherCurrency) ? otherCurrency : null);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.UpdateAsync(currencyId, request));

        Assert.Equal(CxHttpStatusCode.Conflict, exception.StatusCode);
        _currencyRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Currency>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region GetAllAsync

    // Success case: verifies GetAllAsync returns currencies localized for that language when language is provided;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_WhenLanguageIsProvided_ReturnsCurrenciesLocalizedForThatLanguage()
    {
        const string languageId = "fr";
        using var cancellation = new CancellationTokenSource();
        var currencies = new List<CurrencyResponse>
        {
            new() { Id = "EUR", Name = "euro", NumericCode = "978" }
        };
        _currencyRepository
            .Setup(repository => repository.GetAllLocalizedAsync<CurrencyResponse>(languageId, cancellation.Token))
            .ReturnsAsync(currencies);

        var result = await _service.GetAllAsync(languageId, cancellation.Token);

        Assert.Same(currencies, result);
        _currencyRepository.Verify(repository => repository.GetAllLocalizedAsync<CurrencyResponse>(
            languageId, cancellation.Token), Times.Once);
    }

    #endregion

    #region GetByIdAsync

    // Success case: verifies GetByIdAsync returns currency details when found;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByIdAsync_ReturnsCurrencyDetailsWhenFound()
    {
        const string currencyId = "USD";
        using var cancellation = new CancellationTokenSource();
        var currency = new CurrencyDetailsResponse { Id = currencyId, NumericCode = "840" };
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<CurrencyDetailsResponse>(currencyId, cancellation.Token))
            .ReturnsAsync(currency);

        var result = await _service.GetByIdAsync(currencyId, cancellation.Token);

        Assert.Same(currency, result);
        _currencyRepository.Verify(repository => repository.GetByIdOrDefaultAsync<CurrencyDetailsResponse>(
            currencyId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies GetByIdAsync throws not found when currency does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByIdAsync_ThrowsNotFoundWhenCurrencyDoesNotExist()
    {
        const string currencyId = "ZZZ";
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<CurrencyDetailsResponse>(
                currencyId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CurrencyDetailsResponse?)null);

        var exception = await Assert.ThrowsAsync<CxException>(() => _service.GetByIdAsync(currencyId));

        Assert.Equal(CxHttpStatusCode.NotFound, exception.StatusCode);

        _currencyRepository.Verify(repository => repository.GetByIdOrDefaultAsync<CurrencyDetailsResponse>(
            currencyId, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion
}
