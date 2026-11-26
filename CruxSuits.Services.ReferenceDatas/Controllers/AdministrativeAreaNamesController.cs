using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeAreaName;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.ReferenceDatas.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/Countries/{countryId}/AdministrativeAreas/names/{areaCode}")]
public sealed class AdministrativeAreaNamesController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
{
    private readonly IAdministrativeAreaNameService _administrativeAreaNameService;

    public AdministrativeAreaNamesController(
        IConfiguration configuration,
        ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        IAdministrativeAreaNameService administrativeAreaNameService)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _administrativeAreaNameService = administrativeAreaNameService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AdministrativeAreaNameResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(string countryId, string areaCode, CancellationToken cancellationToken) =>
        Ok(await _administrativeAreaNameService.GetAllAsync(countryId, areaCode, cancellationToken));

    [HttpGet("{languageId}")]
    [ProducesResponseType(typeof(AdministrativeAreaNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByLanguage(
        string countryId, string areaCode,
        string languageId,
        CancellationToken cancellationToken) =>
        Ok(await _administrativeAreaNameService.GetByLanguageAsync(countryId, areaCode, languageId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(AdministrativeAreaNameResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        string countryId, string areaCode,
        [FromBody] AdministrativeAreaNameRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _administrativeAreaNameService.CreateAsync(countryId, areaCode, request, cancellationToken);
        return CreatedAtAction(
            nameof(GetByLanguage),
            new { countryId, areaCode, languageId = response.LanguageId },
            response);
    }

    [HttpPut("{languageId}")]
    [ProducesResponseType(typeof(AdministrativeAreaNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        string countryId, string areaCode,
        string languageId,
        [FromBody] AdministrativeAreaNameUpdateRequest request,
        CancellationToken cancellationToken) =>
        Ok(await _administrativeAreaNameService.UpdateAsync(countryId, areaCode, languageId, request, cancellationToken));

    [HttpDelete("{languageId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        string countryId, string areaCode,
        string languageId,
        CancellationToken cancellationToken)
    {
        await _administrativeAreaNameService.RemoveAsync(countryId, areaCode, languageId, cancellationToken);
        return NoContent();
    }
}
