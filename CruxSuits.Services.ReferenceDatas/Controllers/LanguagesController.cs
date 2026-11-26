using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.ReferenceDatas.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public sealed class LanguagesController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
{
    private readonly ILanguageService _languageService;

    public LanguagesController(
        IConfiguration configuration,
        ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        ILanguageService languageService)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _languageService = languageService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LanguageResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
        Ok(await _languageService.GetAllAsync(cancellationToken));

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LanguageDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken) =>
        Ok(await _languageService.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] LanguageRequest request, CancellationToken cancellationToken)
    {
        var response = await _languageService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateLanguageRequest request, CancellationToken cancellationToken) =>
        Ok(await _languageService.UpdateAsync(id, request, cancellationToken));

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        await _languageService.RemoveAsync(id, cancellationToken);
        return NoContent();
    }
}
