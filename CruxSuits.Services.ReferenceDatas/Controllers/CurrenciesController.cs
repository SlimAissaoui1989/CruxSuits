using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.ReferenceDatas.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public sealed class CurrenciesController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
{
    private readonly ICurrencyService _currencyService;
    private readonly ILanguageRequestResolver _languageRequestResolver;

    public CurrenciesController(
        IConfiguration configuration,
        ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
        IReferenceDataUnitOfWork unitOfWork,
        IMapper mapper,
        ICurrencyService currencyService,
        ILanguageRequestResolver languageRequestResolver)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _currencyService = currencyService;
        _languageRequestResolver = languageRequestResolver;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CurrencyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        string languageId = await _languageRequestResolver.ResolveAsync(Request, cancellationToken);
        return Ok(await _currencyService.GetAllAsync(languageId, cancellationToken));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CurrencyDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken) =>
        Ok(await _currencyService.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    [ProducesResponseType(typeof(CurrencyResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CurrencyRequest request, CancellationToken cancellationToken)
    {
        var response = await _currencyService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CurrencyDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        string id,
        [FromBody] UpdateCurrencyRequest request,
        CancellationToken cancellationToken) =>
        Ok(await _currencyService.UpdateAsync(id, request, cancellationToken));

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(string id, CancellationToken cancellationToken)
    {
        await _currencyService.RemoveAsync(id, cancellationToken);
        return NoContent();
    }
}
