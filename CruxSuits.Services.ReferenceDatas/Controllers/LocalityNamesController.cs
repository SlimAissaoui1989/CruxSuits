using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.ReferenceDatas.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/Countries/{countryId}/AdministrativeAreas/{areaCode}/Localities/names/{localityCode}")]
public sealed class LocalityNamesController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
{
    private readonly ILocalityNameService _localityNameService;

    public LocalityNamesController(
        IConfiguration configuration,
        ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        ILocalityNameService localityNameService)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _localityNameService = localityNameService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LocalityNameResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken) =>
        Ok(await _localityNameService.GetAllAsync(countryId, areaCode, localityCode, cancellationToken));

    [HttpGet("{languageId}")]
    [ProducesResponseType(typeof(LocalityNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByLanguage(
        string countryId, string areaCode, string localityCode,
        string languageId,
        CancellationToken cancellationToken) =>
        Ok(await _localityNameService.GetByLanguageAsync(countryId, areaCode, localityCode, languageId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(LocalityNameResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        string countryId, string areaCode, string localityCode,
        [FromBody] LocalityNameRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _localityNameService.CreateAsync(countryId, areaCode, localityCode, request, cancellationToken);
        return CreatedAtAction(
            nameof(GetByLanguage),
            new { countryId, areaCode, localityCode, languageId = response.LanguageId },
            response);
    }

    [HttpPut("{languageId}")]
    [ProducesResponseType(typeof(LocalityNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        string countryId, string areaCode, string localityCode,
        string languageId,
        [FromBody] LocalityNameUpdateRequest request,
        CancellationToken cancellationToken) =>
        Ok(await _localityNameService.UpdateAsync(countryId, areaCode, localityCode, languageId, request, cancellationToken));

    [HttpDelete("{languageId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        string countryId, string areaCode, string localityCode,
        string languageId,
        CancellationToken cancellationToken)
    {
        await _localityNameService.RemoveAsync(countryId, areaCode, localityCode, languageId, cancellationToken);
        return NoContent();
    }
}
