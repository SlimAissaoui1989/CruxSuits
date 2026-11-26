using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/Countries/{countryId}/AdministrativeAreas/{areaCode}/Localities")]
public sealed class LocalitiesController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
{
    private readonly ILocalityService _service;

    public LocalitiesController(
        IConfiguration configuration,
        ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        ILocalityService service)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _service = service;
    }

    [HttpGet("/api/v1/Localities/Autocomplete")]
    [ProducesResponseType(typeof(List<LocalitySuggestionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    public async Task<IActionResult> Autocomplete(
        [FromQuery, Required, StringLength(100, MinimumLength = 2)] string q,
        [FromServices] ILanguageRequestResolver languageRequestResolver,
        CancellationToken cancellationToken)
    {
        var languageId = await languageRequestResolver.ResolveAsync(Request, cancellationToken);
        return Ok(await _service.AutocompleteAsync(q, languageId, cancellationToken));
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LocalityDetailsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll(string countryId, string areaCode, CancellationToken cancellationToken) =>
        Ok(await _service.GetAllAsync(countryId, areaCode, cancellationToken));

    [HttpGet("{localityCode}")]
    [ProducesResponseType(typeof(LocalityDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCode(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken) =>
        Ok(await _service.GetByCodeAsync(countryId, areaCode, localityCode, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(LocalityDetailsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        string countryId, string areaCode, [FromBody] LocalityRequest request, CancellationToken cancellationToken)
    {
        var area = await _service.CreateAsync(countryId, areaCode, request, cancellationToken);
        return CreatedAtAction(nameof(GetByCode), new { countryId, areaCode, localityCode = area.Code }, area);
    }

    [HttpDelete("{localityCode}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string countryId, string areaCode, string localityCode, CancellationToken cancellationToken)
    {
        await _service.RemoveAsync(countryId, areaCode, localityCode, cancellationToken);
        return NoContent();
    }
}
