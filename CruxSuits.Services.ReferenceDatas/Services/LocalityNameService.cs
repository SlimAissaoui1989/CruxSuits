using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;
using System.Linq.Expressions;

namespace CruxSuits.Services.ReferenceDatas.Services;

public sealed class LocalityNameService : ILocalityNameService
{
    private readonly IReferenceDataUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LocalityNameService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<LocalityNameResponse>> GetAllAsync(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken = default)
    {
        var localityId = await GetExistingLocalityIdAsync(countryId, areaCode, localityCode, cancellationToken);

        return await _unitOfWork.LocalityName.GetAllAsync<LocalityNameResponse>(
            name => name.LocalityId == localityId,
            cancellationToken);
    }

    public async Task<LocalityNameResponse> GetByLanguageAsync(
        string countryId, string areaCode, string localityCode,
        string languageId,
        CancellationToken cancellationToken = default)
    {
        var localityId = await GetExistingLocalityIdAsync(countryId, areaCode, localityCode, cancellationToken);

        return await _unitOfWork.LocalityName.GetFirstOrDefaultAsync<LocalityNameResponse>(
            MatchesTranslation(localityId, languageId),
            cancellationToken) ?? throw new CxException(CxHttpStatusCode.NotFound);
    }

    public async Task<LocalityNameResponse> CreateAsync(
        string countryId, string areaCode, string localityCode,
        LocalityNameRequest request,
        CancellationToken cancellationToken = default)
    {
        var localityId = await GetExistingLocalityIdAsync(countryId, areaCode, localityCode, cancellationToken);
        await EnsureLanguageExistsAsync(request.LanguageId, cancellationToken);

        var existingName = await _unitOfWork.LocalityName.GetFirstOrDefaultAsync(
            MatchesTranslation(localityId, request.LanguageId),
            cancellationToken);
        if (existingName is not null)
            throw new CxException(CxHttpStatusCode.Conflict,
                $"An locality name already exists for locality '{localityId}' and language '{request.LanguageId}'.");

        var localityName = _mapper.Map<LocalityName>(request);
        localityName.LocalityId = localityId;

        var createdLocalityName = await _unitOfWork.LocalityName.AddAsync(localityName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<LocalityNameResponse>(createdLocalityName);
    }

    public async Task<LocalityNameResponse> UpdateAsync(
        string countryId, string areaCode, string localityCode,
        string languageId,
        LocalityNameUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var localityId = await GetExistingLocalityIdAsync(countryId, areaCode, localityCode, cancellationToken);
        var localityName = await _unitOfWork.LocalityName.GetFirstOrDefaultAsync(
            MatchesTranslation(localityId, languageId),
            cancellationToken);
        if (localityName is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        _mapper.Map(request, localityName);
        var updatedLocalityName = await _unitOfWork.LocalityName.UpdateAsync(localityName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<LocalityNameResponse>(updatedLocalityName);
    }

    public async Task RemoveAsync(
        string countryId, string areaCode, string localityCode,
        string languageId,
        CancellationToken cancellationToken = default)
    {
        var localityId = await GetExistingLocalityIdAsync(countryId, areaCode, localityCode, cancellationToken);
        var localityName = await _unitOfWork.LocalityName.GetFirstOrDefaultAsync(
            MatchesTranslation(localityId, languageId),
            cancellationToken);
        if (localityName is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        await _unitOfWork.LocalityName.RemoveAsync(localityName, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static Expression<Func<LocalityName, bool>> MatchesTranslation(string localityId, string languageId) =>
        name => name.LocalityId == localityId && name.LanguageId == languageId;

    private async Task<string> GetExistingLocalityIdAsync(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken)
    {
        var administrativeAreaId = _unitOfWork.AdministrativeArea.BuildId(countryId, areaCode);
        if (await _unitOfWork.AdministrativeArea.GetByIdOrDefaultAsync(administrativeAreaId, cancellationToken) is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        var localityId = _unitOfWork.Locality.BuildId(administrativeAreaId, localityCode);
        var locality = await _unitOfWork.Locality.GetByIdOrDefaultAsync<LocalityDetailsResponse>(localityId, cancellationToken);
        if (locality is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        return localityId;
    }

    private async Task EnsureLanguageExistsAsync(string languageId, CancellationToken cancellationToken)
    {
        var language = await _unitOfWork.Language.GetByIdOrDefaultAsync(languageId, cancellationToken);
        if (language is null)
            throw new CxException(CxHttpStatusCode.BadRequest);
    }
}
