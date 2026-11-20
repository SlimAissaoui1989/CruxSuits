using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.ReferenceDatas.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/Countries/{countryId}/names")]
public sealed class CountryNamesController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
{
    private readonly ICountryNameService _countryNameService;

    public CountryNamesController(
        IConfiguration configuration,
        ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        ICountryNameService countryNameService)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _countryNameService = countryNameService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CountryNameResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(string countryId, CancellationToken cancellationToken) =>
        Ok(await _countryNameService.GetAllAsync(countryId, cancellationToken));

    [HttpGet("{languageId}")]
    [ProducesResponseType(typeof(CountryNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByLanguage(
        string countryId,
        string languageId,
        CancellationToken cancellationToken) =>
        Ok(await _countryNameService.GetByLanguageAsync(countryId, languageId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(CountryNameResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        string countryId,
        [FromBody] CountryNameRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _countryNameService.CreateAsync(countryId, request, cancellationToken);
        return CreatedAtAction(
            nameof(GetByLanguage),
            new { countryId, languageId = response.LanguageId },
            response);
    }

    [HttpPut("{languageId}")]
    [ProducesResponseType(typeof(CountryNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        string countryId,
        string languageId,
        [FromBody] CountryNameUpdateRequest request,
        CancellationToken cancellationToken) =>
        Ok(await _countryNameService.UpdateAsync(countryId, languageId, request, cancellationToken));

    [HttpDelete("{languageId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        string countryId,
        string languageId,
        CancellationToken cancellationToken)
    {
        await _countryNameService.RemoveAsync(countryId, languageId, cancellationToken);
        return NoContent();
    }
}
