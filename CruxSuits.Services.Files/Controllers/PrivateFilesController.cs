using AutoMapper;
using CruxIT.Library;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.Files.DTOs;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.Files.Controllers;

[ApiController]
[Authorize(Policy = "ServiceToService")]
[Route("internal/files")]
public sealed class PrivateFilesController : CxControllerBase<FilesContext, IFilesUnitOfWork>
{
    private readonly IUploadedFileService _uploadedFileService;

    public PrivateFilesController(
        IConfiguration configuration,
        ILogger<CxControllerBase<FilesContext, IFilesUnitOfWork>> logger,
        IFilesUnitOfWork unitOfWork,
        IMapper mapper,
        IUploadedFileService uploadedFileService)
        : base(configuration, logger, unitOfWork, mapper)
    {
        _uploadedFileService = uploadedFileService;
    }

    [HttpGet("{id:guid}/download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DownloadPrivate(Guid id, CancellationToken cancellationToken)
    {
        var download = await _uploadedFileService.DownloadPrivateAsync(id, cancellationToken);
        return File(download.Content, download.ContentType, download.FileName);
    }

    [HttpGet("{id:guid}/preview")]
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
}
