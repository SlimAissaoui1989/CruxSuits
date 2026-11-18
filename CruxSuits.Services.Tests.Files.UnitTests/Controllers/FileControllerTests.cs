using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.Files.Controllers;
using CruxSuits.Services.Files.DTOs;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CruxSuits.Services.Files.Tests.UnitTests.Controllers;

public sealed class FileControllerTests
{
    [Fact]
    public async Task PublicPreview_ReturnsInlineStreamForVisualizableContent()
    {
        var stream = new MemoryStream([1, 2, 3]);
        var service = new Mock<IUploadedFileService>();
        service.Setup(fileService => fileService.DownloadAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FileDownloadResult(stream, "image/jpeg", "photo.jpg"));
        var controller = CreatePublicController(service.Object);

        var result = await controller.Preview(Guid.NewGuid(), CancellationToken.None);

        var fileResult = Assert.IsType<FileStreamResult>(result);
        Assert.Equal("image/jpeg", fileResult.ContentType);
        Assert.Same(stream, fileResult.FileStream);
        Assert.Equal("inline", controller.Response.Headers["Content-Disposition"]);
        Assert.Equal("nosniff", controller.Response.Headers["X-Content-Type-Options"]);
    }

    [Fact]
    public async Task PublicPreview_ReturnsUnsupportedMediaTypeForNonVisualizableContent()
    {
        var stream = new MemoryStream([1]);
        var service = new Mock<IUploadedFileService>();
        service.Setup(fileService => fileService.DownloadAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FileDownloadResult(stream, "application/octet-stream", "archive.bin"));
        var controller = CreatePublicController(service.Object);

        var result = await controller.Preview(Guid.NewGuid(), CancellationToken.None);

        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status415UnsupportedMediaType, statusResult.StatusCode);
        Assert.False(stream.CanRead);
    }

    [Fact]
    public void PublicPreview_AllowsAnonymousAccess()
    {
        var method = typeof(FilesController).GetMethod(nameof(FilesController.Preview));

        Assert.NotNull(method);
        Assert.NotNull(method.GetCustomAttributes(typeof(AllowAnonymousAttribute), inherit: true).SingleOrDefault());
    }

    [Fact]
    public void ProtectedFileActions_RequireServiceToServicePolicy()
    {
        var protectedActions = new[]
        {
            nameof(FilesController.Upload),
            nameof(FilesController.Update),
            nameof(FilesController.Remove),
            nameof(FilesController.DownloadPrivate),
            nameof(FilesController.PreviewPrivate)
        };

        foreach (var actionName in protectedActions)
        {
            var method = typeof(FilesController).GetMethod(actionName);
            Assert.NotNull(method);
            var authorization = Assert.Single(method.GetCustomAttributes(typeof(AuthorizeAttribute), inherit: true)
                .Cast<AuthorizeAttribute>());
            Assert.Equal("ServiceToService", authorization.Policy);
        }
    }

    [Fact]
    public async Task PrivatePreview_UsesPrivateFileServiceMethod()
    {
        var stream = new MemoryStream([1, 2]);
        var service = new Mock<IUploadedFileService>();
        service.Setup(fileService => fileService.DownloadPrivateAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FileDownloadResult(stream, "image/png", "private.png"));
        var controller = CreatePublicController(service.Object);

        var result = await controller.PreviewPrivate(Guid.NewGuid(), CancellationToken.None);

        Assert.IsType<FileStreamResult>(result);
        service.Verify(fileService => fileService.DownloadPrivateAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        service.Verify(fileService => fileService.DownloadAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    private static FilesController CreatePublicController(IUploadedFileService service)
    {
        var controller = new FilesController(
            new ConfigurationBuilder().Build(),
            NullLogger<CxControllerBase<FilesContext, IFilesUnitOfWork>>.Instance,
            Mock.Of<IFilesUnitOfWork>(),
            Mock.Of<IMapper>(),
            service);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        return controller;
    }

}
