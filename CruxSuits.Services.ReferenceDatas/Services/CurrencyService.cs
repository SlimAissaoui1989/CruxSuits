using CruxIT.Library;
using AutoMapper;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;

namespace CruxSuits.Services.ReferenceDatas.Services;

public sealed class CurrencyService : ICurrencyService
{
    private readonly IReferenceDataUnitOfWork _unitOfWork;
    private readonly IMapper? _mapper;

    public CurrencyService(IReferenceDataUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public CurrencyService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<List<CurrencyResponse>> GetAllAsync(string languageId, CancellationToken cancellationToken = default) =>
        _unitOfWork.Currency.GetAllLocalizedAsync<CurrencyResponse>(languageId, cancellationToken);

    public async Task<CurrencyDetailsResponse> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var currency = await _unitOfWork.Currency.GetByIdOrDefaultAsync<CurrencyDetailsResponse>(id, cancellationToken);
        if (currency is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        return currency;
    }

    public async Task<CurrencyResponse> CreateAsync(CurrencyRequest request, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.Currency.GetByIdOrDefaultAsync(request.Id, cancellationToken) is not null)
            throw new CxException(CxHttpStatusCode.Conflict, $"Currency '{request.Id}' already exists.");

        var currency = Mapper.Map<Currency>(request);

        var duplicateNumericCode = await _unitOfWork.Currency.GetFirstOrDefaultAsync(
            existing => existing.Id != currency.Id && existing.NumericCode == currency.NumericCode,
            cancellationToken);
        if (duplicateNumericCode is not null)
            throw new CxException(CxHttpStatusCode.Conflict,
                $"NumericCode '{currency.NumericCode}' is already assigned to another currency.");

        var createdCurrency = await _unitOfWork.Currency.AddAsync(currency, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Mapper.Map<CurrencyResponse>(createdCurrency);
    }

    public async Task<CurrencyDetailsResponse> UpdateAsync(
        string id,
        UpdateCurrencyRequest request,
        CancellationToken cancellationToken = default)
    {
        var existingCurrency = await _unitOfWork.Currency.GetByIdOrDefaultAsync(id, cancellationToken);
        if (existingCurrency is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        var duplicateNumericCode = await _unitOfWork.Currency.GetFirstOrDefaultAsync(
            existing => existing.Id != id && existing.NumericCode == request.NumericCode,
            cancellationToken);
        if (duplicateNumericCode is not null)
            throw new CxException(CxHttpStatusCode.Conflict,
                $"NumericCode '{request.NumericCode}' is already assigned to another currency.");

        Mapper.Map(request, existingCurrency);
        var updatedCurrency = await _unitOfWork.Currency.UpdateAsync(existingCurrency, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Mapper.Map<CurrencyDetailsResponse>(updatedCurrency);
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var currency = await _unitOfWork.Currency.GetByIdOrDefaultAsync(id, cancellationToken);
        if (currency is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        await _unitOfWork.Currency.RemoveAsync(currency, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private IMapper Mapper => _mapper ?? throw new InvalidOperationException(
        "An AutoMapper instance is required for currency create and update operations.");
}
