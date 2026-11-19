using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.ReferenceDatas.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public sealed class CountriesController : CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>
    {
        private readonly ICountryService _countryService;

        public CountriesController(
            IConfiguration configuration,
            ILogger<CxControllerBase<ReferenceDataContext, IReferenceDataUnitOfWork>> logger,
            IReferenceDataUnitOfWork unitOfWork,
            IMapper mapper,
            ICountryService countryService)
            : base(configuration, logger, unitOfWork, mapper)
        {
            _countryService = countryService;
        }

        [HttpGet("~/api/v1/{languageId}/[controller]")]
        [ProducesResponseType(typeof(List<CountryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(string languageId,CancellationToken cancellationToken) =>
            Ok(await _countryService.GetAllAsync(languageId, cancellationToken));

        [HttpGet("{languageId}/phone-code-options")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CountryPhoneCodeResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPhoneCodeOptions(string languageId, CancellationToken cancellationToken) =>
            Ok(await _countryService.GetPhoneCodeOptionsAsync(languageId, cancellationToken));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CountryDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken) =>
            Ok(await _countryService.GetByIdAsync(id, cancellationToken));

        [HttpPost]
        [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CountryRequest request, CancellationToken cancellationToken)
        {
            var response = await _countryService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CountryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCountryRequest request, CancellationToken cancellationToken) =>
            Ok(await _countryService.UpdateAsync(id, request, cancellationToken));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(string id, CancellationToken cancellationToken)
        {
            await _countryService.RemoveAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
