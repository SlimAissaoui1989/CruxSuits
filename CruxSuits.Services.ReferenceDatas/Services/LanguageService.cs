using CruxIT.Library;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using AutoMapper;

namespace CruxSuits.Services.ReferenceDatas.Services;

public sealed class LanguageService : ILanguageService
{
    private readonly IReferenceDataUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILanguageCatalogProvider _languageCatalogProvider;

    public LanguageService(
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        ILanguageCatalogProvider languageCatalogProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _languageCatalogProvider = languageCatalogProvider;
    }

    public Task<List<LanguageResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _unitOfWork.Language.GetAllAsync<LanguageResponse>(null, cancellationToken);

    public async Task<LanguageDetailsResponse> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var language = await _unitOfWork.Language.GetByIdOrDefaultAsync<LanguageDetailsResponse>(id, cancellationToken);
        if (language is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        return language;
    }

    public async Task<LanguageResponse> CreateAsync(LanguageRequest request, CancellationToken cancellationToken = default)
    {
        if (await _unitOfWork.Language.GetByIdOrDefaultAsync(request.Id, cancellationToken) is not null)
            throw new CxException(CxHttpStatusCode.Conflict, $"Language '{request.Id}' already exists.");

        var language = _mapper.Map<Language>(request);
        var createdLanguage = await _unitOfWork.Language.AddAsync(language, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _languageCatalogProvider.RefreshAsync(cancellationToken);
        return _mapper.Map<LanguageResponse>(createdLanguage);
    }

    public async Task<LanguageResponse> UpdateAsync(
        string id,
        UpdateLanguageRequest request,
        CancellationToken cancellationToken = default)
    {
        var language = await _unitOfWork.Language.GetByIdOrDefaultAsync(id, cancellationToken);
        if (language is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        _mapper.Map(request, language);
        var updatedLanguage = await _unitOfWork.Language.UpdateAsync(language, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _languageCatalogProvider.RefreshAsync(cancellationToken);
        return _mapper.Map<LanguageResponse>(updatedLanguage);
    }

    public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
    {
        var language = await _unitOfWork.Language.GetByIdOrDefaultAsync(id, cancellationToken);
        if (language is null)
            throw new CxException(CxHttpStatusCode.NotFound);

        await _unitOfWork.Language.RemoveAsync(language, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _languageCatalogProvider.RefreshAsync(cancellationToken);
    }
}
