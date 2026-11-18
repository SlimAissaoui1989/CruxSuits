using AutoMapper;
using CruxIT.Library;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Services;
using CruxSuits.Services.Files.DTOs;
using CruxSuits.Shared.DTOs.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.Files.Controllers
{
    [ApiController]
    [Authorize]
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
        [Authorize(Policy = "ServiceToService")]
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
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
        {
            var download = await _uploadedFileService.DownloadAsync(id, cancellationToken);

            return File(download.Content, download.ContentType, download.FileName);
        }

        [HttpGet("{id:guid}/preview")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        public async Task<IActionResult> Preview(Guid id, CancellationToken cancellationToken)
        {
            var preview = await _uploadedFileService.DownloadAsync(id, cancellationToken);
            return CreatePreviewResult(preview);
        }

        [HttpGet("~/internal/files/{id:guid}/download")]
        [Authorize(Policy = "ServiceToService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DownloadPrivate(Guid id, CancellationToken cancellationToken)
        {
            var download = await _uploadedFileService.DownloadPrivateAsync(id, cancellationToken);
            return File(download.Content, download.ContentType, download.FileName);
        }

        [HttpGet("~/internal/files/{id:guid}/preview")]
        [Authorize(Policy = "ServiceToService")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PreviewPrivate(Guid id, CancellationToken cancellationToken)
        {
            var preview = await _uploadedFileService.DownloadPrivateAsync(id, cancellationToken);
            return CreatePreviewResult(preview);
        }

        private IActionResult CreatePreviewResult(FileDownloadResult preview)
        {
            if (!CxMimeTypeHelper.CanVisualizeInBrowser(preview.ContentType))
            {
                preview.Content.Dispose();
                return StatusCode(
                    StatusCodes.Status415UnsupportedMediaType,
                    "This file type cannot be previewed in a browser.");
            }

            Response.Headers["Content-Disposition"] = "inline";
            Response.Headers["X-Content-Type-Options"] = "nosniff";
            Response.Headers["Content-Security-Policy"] = "sandbox; default-src 'none'; img-src data: blob:; style-src 'unsafe-inline'";

            return File(preview.Content, preview.ContentType, enableRangeProcessing: true);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "ServiceToService")]
        [ProducesResponseType(typeof(UploadFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFileRequest request, CancellationToken cancellationToken)
        {
            var response = await _uploadedFileService.UpdateAsync(id, request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "ServiceToService")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
        {
            await _uploadedFileService.RemoveAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
