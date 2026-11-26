using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;

namespace CruxSuits.Services.ReferenceDatas.Services;

public sealed class LocalityService : ILocalityService
{
    private readonly IReferenceDataUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LocalityService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<List<LocalitySuggestionResponse>> AutocompleteAsync(
        string query, string languageId, CancellationToken cancellationToken = default)
    {
        var search = query.Trim();
        return search.Length < 2
            ? Task.FromResult(new List<LocalitySuggestionResponse>())
            : _unitOfWork.Locality.AutocompleteAsync(search, languageId, cancellationToken);
    }

    public async Task<List<LocalityDetailsResponse>> GetAllAsync(
        string countryId, string areaCode, CancellationToken cancellationToken = default)
    {
        var administrativeAreaId = await GetExistingAdministrativeAreaIdAsync(countryId, areaCode, cancellationToken);
        return await _unitOfWork.Locality.GetAllAsync<LocalityDetailsResponse>(
            locality => locality.AdministrativeAreaId == administrativeAreaId, cancellationToken);
    }

    public async Task<LocalityDetailsResponse> GetByCodeAsync(
        string countryId, string areaCode, string localityCode, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.Locality;
        var administrativeAreaId = await GetExistingAdministrativeAreaIdAsync(countryId, areaCode, cancellationToken);
        var id = repository.BuildId(administrativeAreaId, localityCode);
        return await repository.GetByIdOrDefaultAsync<LocalityDetailsResponse>(id, cancellationToken)
            ?? throw new CxException(CxHttpStatusCode.NotFound);
    }

    public async Task<LocalityDetailsResponse> CreateAsync(
        string countryId, string areaCode, LocalityRequest request, CancellationToken cancellationToken = default)
    {
        var administrativeAreaId = await GetExistingAdministrativeAreaIdAsync(countryId, areaCode, cancellationToken);

        if (administrativeAreaId.Length + 1 + request.Code.Length > 11)
            throw new CxException(CxHttpStatusCode.BadRequest,
                "The administrative area ID and locality code must form an ID of at most 11 characters.");

        var repository = _unitOfWork.Locality;
        if (await repository.ExistsIncludingDeletedAsync(administrativeAreaId, request.Code, cancellationToken))
            throw new CxException(CxHttpStatusCode.Conflict);

        var locality = _mapper.Map<Locality>(request);
        locality.AdministrativeAreaId = administrativeAreaId;
        var created = await repository.AddAsync(locality, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<LocalityDetailsResponse>(created);
    }

    public async Task RemoveAsync(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.Locality;
        var administrativeAreaId = await GetExistingAdministrativeAreaIdAsync(countryId, areaCode, cancellationToken);
        var id = repository.BuildId(administrativeAreaId, localityCode);
        var locality = await repository.GetByIdOrDefaultAsync(id, cancellationToken)
            ?? throw new CxException(CxHttpStatusCode.NotFound);

        await RemoveAsync(locality, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveByAdministrativeAreaIdAsync(string administrativeAreaId, CancellationToken cancellationToken = default)
    {
        var localities = await _unitOfWork.Locality.GetAllAsync(
            locality => locality.AdministrativeAreaId == administrativeAreaId && !locality.IsDeleted,
            cancellationToken);

        foreach (var locality in localities)
            await RemoveAsync(locality, cancellationToken);
    }

    private Task RemoveAsync(Locality locality, CancellationToken cancellationToken) =>
        _unitOfWork.Locality.RemoveAsync(locality, cancellationToken);

    private async Task<string> GetExistingAdministrativeAreaIdAsync(string countryId, string areaCode, CancellationToken cancellationToken)
    {
        var id = _unitOfWork.AdministrativeArea.BuildId(countryId, areaCode);
        if (await _unitOfWork.AdministrativeArea.GetByIdOrDefaultAsync(id, cancellationToken) is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        return id;
    }
}
