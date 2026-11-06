using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Services;
using CruxSuits.Shared.DTOs.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.Files.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class FilesController : CxControllerBase<FilesContext, IFilesUnitOfWork>
    {
        private readonly IUploadedFileService _uploadedFileService;

        public FilesController(
            IConfiguration configuration,
            ILogger<CxControllerBase<FilesContext, IFilesUnitOfWork>> logger,
            IFilesUnitOfWork unitOfWork,
            IMapper mapper,
            IUploadedFileService uploadedFileService)
            : base(configuration, logger, unitOfWork, mapper)
        {
            _uploadedFileService = uploadedFileService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Upload([FromForm] UploadFileRequest request, CancellationToken cancellationToken)
        {
            if (request.File is null || request.File.Length == 0)
            {
                return BadRequest("A non-empty file is required.");
            }

            var response = await _uploadedFileService.UploadAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpGet("{id:guid}/download")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {
            var download = await _uploadedFileService.DownloadAsync(id, cancellationToken);

            return File(download.Content, download.ContentType, download.FileName);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UploadFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFileRequest request, CancellationToken cancellationToken)
        {
            var response = await _uploadedFileService.UpdateAsync(id, request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
        {
            await _uploadedFileService.RemoveAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
