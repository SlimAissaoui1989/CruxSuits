using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeAreaName;
using System.Linq.Expressions;

namespace CruxSuits.Services.ReferenceDatas.Services;

public sealed class AdministrativeAreaNameService : IAdministrativeAreaNameService
{
    private readonly IReferenceDataUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdministrativeAreaNameService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AdministrativeAreaNameResponse>> GetAllAsync(string countryId, string areaCode, CancellationToken cancellationToken = default)
    {
        var administrativeAreaId = await GetExistingAreaIdAsync(countryId, areaCode, cancellationToken);

        return await _unitOfWork.AdministrativeAreaName.GetAllAsync<AdministrativeAreaNameResponse>(
            name => name.AdministrativeAreaId == administrativeAreaId,
            cancellationToken);
    }

    public async Task<AdministrativeAreaNameResponse> GetByLanguageAsync(
        string countryId, string areaCode,
        string languageId,
        CancellationToken cancellationToken = default)
    {
        var administrativeAreaId = await GetExistingAreaIdAsync(countryId, areaCode, cancellationToken);

        return await _unitOfWork.AdministrativeAreaName.GetFirstOrDefaultAsync<AdministrativeAreaNameResponse>(
            MatchesTranslation(administrativeAreaId, languageId),
            cancellationToken) ?? throw new CxException(CxHttpStatusCode.NotFound);
    }

    public async Task<AdministrativeAreaNameResponse> CreateAsync(
        string countryId, string areaCode,
        AdministrativeAreaNameRequest request,
        CancellationToken cancellationToken = default)
    {
        var administrativeAreaId = await GetExistingAreaIdAsync(countryId, areaCode, cancellationToken);
        await EnsureLanguageExistsAsync(request.LanguageId, cancellationToken);

        var existingName = await _unitOfWork.AdministrativeAreaName.GetFirstOrDefaultAsync(
            MatchesTranslation(administrativeAreaId, request.LanguageId),
            cancellationToken);
        if (existingName is not null)
            throw new CxException(CxHttpStatusCode.Conflict,
                $"An administrative area name already exists for administrative area '{administrativeAreaId}' and language '{request.LanguageId}'.");

        var administrativeAreaName = _mapper.Map<AdministrativeAreaName>(request);
        administrativeAreaName.AdministrativeAreaId = administrativeAreaId;

        var createdAdministrativeAreaName = await _unitOfWork.AdministrativeAreaName.AddAsync(administrativeAreaName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AdministrativeAreaNameResponse>(createdAdministrativeAreaName);
    }

    public async Task<AdministrativeAreaNameResponse> UpdateAsync(
        string countryId, string areaCode,
        string languageId,
        AdministrativeAreaNameUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var administrativeAreaId = await GetExistingAreaIdAsync(countryId, areaCode, cancellationToken);
        var administrativeAreaName = await _unitOfWork.AdministrativeAreaName.GetFirstOrDefaultAsync(
            MatchesTranslation(administrativeAreaId, languageId),
            cancellationToken);
        if (administrativeAreaName is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        _mapper.Map(request, administrativeAreaName);
        var updatedAdministrativeAreaName = await _unitOfWork.AdministrativeAreaName.UpdateAsync(administrativeAreaName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AdministrativeAreaNameResponse>(updatedAdministrativeAreaName);
    }

    public async Task RemoveAsync(
        string countryId, string areaCode,
        string languageId,
        CancellationToken cancellationToken = default)
    {
        var administrativeAreaId = await GetExistingAreaIdAsync(countryId, areaCode, cancellationToken);
        var administrativeAreaName = await _unitOfWork.AdministrativeAreaName.GetFirstOrDefaultAsync(
            MatchesTranslation(administrativeAreaId, languageId),
            cancellationToken);
        if (administrativeAreaName is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        await _unitOfWork.AdministrativeAreaName.RemoveAsync(administrativeAreaName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static Expression<Func<AdministrativeAreaName, bool>> MatchesTranslation(string administrativeAreaId, string languageId) =>
        name => name.AdministrativeAreaId == administrativeAreaId && name.LanguageId == languageId;

    private async Task<string> GetExistingAreaIdAsync(string countryId, string areaCode, CancellationToken cancellationToken)
    {
        var administrativeAreaId = _unitOfWork.AdministrativeArea.BuildId(countryId, areaCode);
        var area = await _unitOfWork.AdministrativeArea.GetByIdOrDefaultAsync<AdministrativeAreaDetailsResponse>(administrativeAreaId, cancellationToken);
        if (area is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        return administrativeAreaId;
    }

    private async Task EnsureLanguageExistsAsync(string languageId, CancellationToken cancellationToken)
    {
        var language = await _unitOfWork.Language.GetByIdOrDefaultAsync(languageId, cancellationToken);
        if (language is null)
            throw new CxException(CxHttpStatusCode.BadRequest);
    }
}
