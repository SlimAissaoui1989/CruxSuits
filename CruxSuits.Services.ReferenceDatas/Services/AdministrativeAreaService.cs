using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;

namespace CruxSuits.Services.ReferenceDatas.Services;

public sealed class AdministrativeAreaService : IAdministrativeAreaService
{
    private readonly IReferenceDataUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILocalityService _localityService;

    public AdministrativeAreaService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper, ILocalityService localityService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localityService = localityService;
    }

    public async Task<List<AdministrativeAreaDetailsResponse>> GetAllAsync(
        string countryId, CancellationToken cancellationToken = default)
    {
        await EnsureCountryExistsAsync(countryId, cancellationToken);
        return await _unitOfWork.AdministrativeArea.GetAllAsync<AdministrativeAreaDetailsResponse>(
            area => area.CountryId == countryId, cancellationToken);
    }

    public async Task<AdministrativeAreaDetailsResponse> GetByCodeAsync(
        string countryId, string areaCode, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.AdministrativeArea;
        var id = repository.BuildId(countryId, areaCode);
        return await repository.GetByIdOrDefaultAsync<AdministrativeAreaDetailsResponse>(id, cancellationToken)
            ?? throw new CxException(CxHttpStatusCode.NotFound);
    }

    public async Task<AdministrativeAreaDetailsResponse> CreateAsync(
        string countryId, AdministrativeAreaRequest request, CancellationToken cancellationToken = default)
    {
        await EnsureCountryExistsAsync(countryId, cancellationToken);

        var repository = _unitOfWork.AdministrativeArea;
        if (await repository.ExistsIncludingDeletedAsync(countryId, request.Code, cancellationToken))
            throw new CxException(CxHttpStatusCode.Conflict);

        var area = _mapper.Map<AdministrativeArea>(request);
        area.CountryId = countryId;
        var created = await repository.AddAsync(area, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AdministrativeAreaDetailsResponse>(created);
    }

    public async Task RemoveAsync(string countryId, string areaCode, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.AdministrativeArea;
        var id = repository.BuildId(countryId, areaCode);
        var area = await repository.GetByIdOrDefaultAsync(id, cancellationToken)
            ?? throw new CxException(CxHttpStatusCode.NotFound);

        await RemoveAsync(area, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveByCountryIdAsync(string countryId, CancellationToken cancellationToken = default)
    {
        var areas = await _unitOfWork.AdministrativeArea.GetAllAsync(
            area => area.CountryId == countryId && !area.IsDeleted,
            cancellationToken);
        foreach (var area in areas)
            await RemoveAsync(area, cancellationToken);
    }

    private async Task RemoveAsync(AdministrativeArea area, CancellationToken cancellationToken)
    {
        await _localityService.RemoveByAdministrativeAreaIdAsync(area.Id, cancellationToken);
        await _unitOfWork.AdministrativeArea.RemoveAsync(area, cancellationToken);
    }

    private async Task EnsureCountryExistsAsync(string countryId, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Country.GetByIdOrDefaultAsync(countryId, cancellationToken) is null)
            throw new CxException(CxHttpStatusCode.NotFound);
    }
}
