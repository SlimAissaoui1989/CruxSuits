using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas.Country;
using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;

namespace CruxSuits.Services.ReferenceDatas.Services;

public sealed class CountryNameService : ICountryNameService
{
    private readonly IReferenceDataUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CountryNameService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CountryNameResponse>> GetAllAsync(string countryId, CancellationToken cancellationToken = default)
    {
        await EnsureCountryExistsAsync(countryId, cancellationToken);

        return await _unitOfWork.CountryName.GetAllAsync<CountryNameResponse>(
            name => name.CountryId == countryId,
            cancellationToken);
    }

    public async Task<CountryNameResponse> GetByLanguageAsync(
        string countryId,
        string languageId,
        CancellationToken cancellationToken = default)
    {
        await EnsureCountryExistsAsync(countryId, cancellationToken);

        var names = await _unitOfWork.CountryName.GetAllAsync<CountryNameResponse>(
            name => name.CountryId == countryId && name.LanguageId == languageId,
            cancellationToken);
        return names.SingleOrDefault() ?? throw new CxException(CxHttpStatusCode.NotFound);
    }

    public async Task<CountryNameResponse> CreateAsync(
        string countryId,
        CountryNameRequest request,
        CancellationToken cancellationToken = default)
    {
        await EnsureCountryExistsAsync(countryId, cancellationToken);
        await EnsureLanguageExistsAsync(request.LanguageId, cancellationToken);

        var existingName = await _unitOfWork.CountryName.GetFirstOrDefaultAsync(
            name => name.CountryId == countryId && name.LanguageId == request.LanguageId,
            cancellationToken);
        if (existingName is not null)
            throw new CxException(CxHttpStatusCode.Conflict,
                $"A country name already exists for country '{countryId}' and language '{request.LanguageId}'.");

        var countryName = _mapper.Map<CountryName>(request);
        countryName.CountryId = countryId;

        var createdCountryName = await _unitOfWork.CountryName.AddAsync(countryName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CountryNameResponse>(createdCountryName);
    }

    public async Task<CountryNameResponse> UpdateAsync(
        string countryId,
        string languageId,
        CountryNameUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var countryName = await _unitOfWork.CountryName.GetFirstOrDefaultAsync(
            name => name.CountryId == countryId && name.LanguageId == languageId,
            cancellationToken);
        if (countryName is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        _mapper.Map(request, countryName);
        var updatedCountryName = await _unitOfWork.CountryName.UpdateAsync(countryName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CountryNameResponse>(updatedCountryName);
    }

    public async Task RemoveAsync(
        string countryId,
        string languageId,
        CancellationToken cancellationToken = default)
    {
        var countryName = await _unitOfWork.CountryName.GetFirstOrDefaultAsync(
            name => name.CountryId == countryId && name.LanguageId == languageId,
            cancellationToken);
        if (countryName is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        await _unitOfWork.CountryName.RemoveAsync(countryName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureCountryExistsAsync(string countryId, CancellationToken cancellationToken)
    {
        var country = await _unitOfWork.Country.GetByIdOrDefaultAsync<CountryDetailsResponse>(countryId, cancellationToken);
        if (country is null)
            throw new CxException(CxHttpStatusCode.NotFound);
    }

    private async Task EnsureLanguageExistsAsync(string languageId, CancellationToken cancellationToken)
    {
        var language = await _unitOfWork.Language.GetByIdOrDefaultAsync(languageId, cancellationToken);
        if (language is null)
            throw new CxException(CxHttpStatusCode.BadRequest);
    }
}
