using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;
using CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;
using System.Linq.Expressions;

namespace CruxSuits.Services.ReferenceDatas.Services;

public sealed class CurrencyNameService : ICurrencyNameService
{
    private readonly IReferenceDataUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CurrencyNameService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CurrencyNameResponse>> GetAllAsync(string currencyId, CancellationToken cancellationToken = default)
    {
        await EnsureCurrencyExistsAsync(currencyId, cancellationToken);

        return await _unitOfWork.CurrencyName.GetAllAsync<CurrencyNameResponse>(
            name => name.CurrencyId == currencyId,
            cancellationToken);
    }

    public async Task<CurrencyNameResponse> GetByLanguageAsync(
        string currencyId,
        string languageId,
        CancellationToken cancellationToken = default)
    {
        await EnsureCurrencyExistsAsync(currencyId, cancellationToken);

        return await _unitOfWork.CurrencyName.GetFirstOrDefaultAsync<CurrencyNameResponse>(
            MatchesTranslation(currencyId, languageId),
            cancellationToken) ?? throw new CxException(CxHttpStatusCode.NotFound);
    }

    public async Task<CurrencyNameResponse> CreateAsync(
        string currencyId,
        CurrencyNameRequest request,
        CancellationToken cancellationToken = default)
    {
        await EnsureCurrencyExistsAsync(currencyId, cancellationToken);
        await EnsureLanguageExistsAsync(request.LanguageId, cancellationToken);

        var existingName = await _unitOfWork.CurrencyName.GetFirstOrDefaultAsync(
            MatchesTranslation(currencyId, request.LanguageId),
            cancellationToken);
        if (existingName is not null)
            throw new CxException(CxHttpStatusCode.Conflict,
                $"A currency name already exists for currency '{currencyId}' and language '{request.LanguageId}'.");

        var currencyName = _mapper.Map<CurrencyName>(request);
        currencyName.CurrencyId = currencyId;

        var createdCurrencyName = await _unitOfWork.CurrencyName.AddAsync(currencyName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CurrencyNameResponse>(createdCurrencyName);
    }

    public async Task<CurrencyNameResponse> UpdateAsync(
        string currencyId,
        string languageId,
        CurrencyNameUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var currencyName = await _unitOfWork.CurrencyName.GetFirstOrDefaultAsync(
            MatchesTranslation(currencyId, languageId),
            cancellationToken);
        if (currencyName is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        _mapper.Map(request, currencyName);
        var updatedCurrencyName = await _unitOfWork.CurrencyName.UpdateAsync(currencyName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CurrencyNameResponse>(updatedCurrencyName);
    }

    public async Task RemoveAsync(
        string currencyId,
        string languageId,
        CancellationToken cancellationToken = default)
    {
        var currencyName = await _unitOfWork.CurrencyName.GetFirstOrDefaultAsync(
            MatchesTranslation(currencyId, languageId),
            cancellationToken);
        if (currencyName is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        await _unitOfWork.CurrencyName.RemoveAsync(currencyName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static Expression<Func<CurrencyName, bool>> MatchesTranslation(string currencyId, string languageId) =>
        name => name.CurrencyId == currencyId && name.LanguageId == languageId;

    private async Task EnsureCurrencyExistsAsync(string currencyId, CancellationToken cancellationToken)
    {
        var currency = await _unitOfWork.Currency.GetByIdOrDefaultAsync<CurrencyDetailsResponse>(currencyId, cancellationToken);
        if (currency is null)
            throw new CxException(CxHttpStatusCode.NotFound);
    }

    private async Task EnsureLanguageExistsAsync(string languageId, CancellationToken cancellationToken)
    {
        var language = await _unitOfWork.Language.GetByIdOrDefaultAsync(languageId, cancellationToken);
        if (language is null)
            throw new CxException(CxHttpStatusCode.BadRequest);
    }
}
