using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.ReferenceDatas.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/Countries/{countryId}/AdministrativeAreas")]
public sealed class AdministrativeAreasController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
{
    private readonly IAdministrativeAreaService _service;

    public AdministrativeAreasController(
        IConfiguration configuration,
        ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        IAdministrativeAreaService service)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<AdministrativeAreaDetailsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll(string countryId, CancellationToken cancellationToken) =>
        Ok(await _service.GetAllAsync(countryId, cancellationToken));

    [HttpGet("{areaCode}")]
    [ProducesResponseType(typeof(AdministrativeAreaDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCode(string countryId, string areaCode, CancellationToken cancellationToken) =>
        Ok(await _service.GetByCodeAsync(countryId, areaCode, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(AdministrativeAreaDetailsResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        string countryId, [FromBody] AdministrativeAreaRequest request, CancellationToken cancellationToken)
    {
        var area = await _service.CreateAsync(countryId, request, cancellationToken);
        return CreatedAtAction(nameof(GetByCode), new { countryId, areaCode = area.Code }, area);
    }

    [HttpDelete("{areaCode}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string countryId, string areaCode, CancellationToken cancellationToken)
    {
        await _service.RemoveAsync(countryId, areaCode, cancellationToken);
        return NoContent();
    }
}
