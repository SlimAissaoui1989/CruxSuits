using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.ReferenceDatas.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/Currencies/{currencyId}/names")]
public sealed class CurrencyNamesController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
{
    private readonly ICurrencyNameService _currencyNameService;

    public CurrencyNamesController(
        IConfiguration configuration,
        ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrencyNameService currencyNameService)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _currencyNameService = currencyNameService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CurrencyNameResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(string currencyId, CancellationToken cancellationToken) =>
        Ok(await _currencyNameService.GetAllAsync(currencyId, cancellationToken));

    [HttpGet("{languageId}")]
    [ProducesResponseType(typeof(CurrencyNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByLanguage(
        string currencyId,
        string languageId,
        CancellationToken cancellationToken) =>
        Ok(await _currencyNameService.GetByLanguageAsync(currencyId, languageId, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(CurrencyNameResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        string currencyId,
        [FromBody] CurrencyNameRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _currencyNameService.CreateAsync(currencyId, request, cancellationToken);
        return CreatedAtAction(
            nameof(GetByLanguage),
            new { currencyId, languageId = response.LanguageId },
            response);
    }

    [HttpPut("{languageId}")]
    [ProducesResponseType(typeof(CurrencyNameResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        string currencyId,
        string languageId,
        [FromBody] CurrencyNameUpdateRequest request,
        CancellationToken cancellationToken) =>
        Ok(await _currencyNameService.UpdateAsync(currencyId, languageId, request, cancellationToken));

    [HttpDelete("{languageId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        string currencyId,
        string languageId,
        CancellationToken cancellationToken)
    {
        await _currencyNameService.RemoveAsync(currencyId, languageId, cancellationToken);
        return NoContent();
    }
}
