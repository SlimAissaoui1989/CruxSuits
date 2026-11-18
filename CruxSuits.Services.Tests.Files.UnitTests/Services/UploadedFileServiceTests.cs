using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.Files.DTOs;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.Files.Services;
using CruxSuits.Shared.DTOs.Files;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace CruxSuits.Services.Files.Tests.UnitTests.Services;

public sealed class UploadedFileServiceTests
{
    private readonly Mock<IFilesUnitOfWork> _unitOfWork = new();
    private readonly Mock<IUploadedFileRepository> _repository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly UploadedFileService _service;

    public UploadedFileServiceTests()
    {
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.UploadedFile).Returns(_repository.Object);
        _service = new UploadedFileService(_unitOfWork.Object, _mapper.Object);
    }

    [Fact]
    public async Task UploadAsync_CopiesFileContentAndSavesChanges()
    {
        var bytes = new byte[] { 1, 2, 3, 4 };
        var request = CreateUploadRequest(bytes);
        var entity = new UploadedFile();
        var response = new UploadFileResponse { OriginalFileName = "photo.jpg" };

        _mapper.Setup(mapper => mapper.Map<UploadedFile>(request)).Returns(entity);
        _repository.Setup(repository => repository.AddAsync(entity, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);
        _mapper.Setup(mapper => mapper.Map<UploadFileResponse>(entity)).Returns(response);

        var result = await _service.UploadAsync(request);

        Assert.Same(response, result);
        Assert.Equal(bytes, entity.Content);
        _repository.Verify(repository => repository.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DownloadAsync_ReturnsPublicFileStream()
    {
        var id = Guid.NewGuid();
        var entity = CreateFile(id, isPublic: true);
        _repository.Setup(repository => repository.GetPublicForDownloadAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        var result = await _service.DownloadAsync(id);

        Assert.Equal("image/jpeg", result.ContentType);
        Assert.Equal("photo.jpg", result.FileName);
        using var copy = new MemoryStream();
        await result.Content.CopyToAsync(copy);
        Assert.Equal(new byte[] { 4, 5, 6 }, copy.ToArray());
        _repository.Verify(repository => repository.GetPublicForDownloadAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DownloadAsync_ThrowsNotFoundWhenFileIsNotPublicOrMissing()
    {
        var id = Guid.NewGuid();
        _repository.Setup(repository => repository.GetPublicForDownloadAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UploadedFile?)null);

        await Assert.ThrowsAsync<CxException>(() => _service.DownloadAsync(id));
    }

    [Fact]
    public async Task DownloadPrivateAsync_ReturnsPrivateFileStream()
    {
        var id = Guid.NewGuid();
        var entity = CreateFile(id, isPublic: false);
        _repository.Setup(repository => repository.GetPrivateForDownloadAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        var result = await _service.DownloadPrivateAsync(id);

        Assert.Equal("application/pdf", result.ContentType);
        Assert.Equal("private.pdf", result.FileName);
        _repository.Verify(repository => repository.GetPrivateForDownloadAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        result.Content.Dispose();
    }

    [Fact]
    public async Task DownloadPrivateAsync_ThrowsNotFoundWhenFileIsPublicOrMissing()
    {
        var id = Guid.NewGuid();
        _repository.Setup(repository => repository.GetPrivateForDownloadAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UploadedFile?)null);

        await Assert.ThrowsAsync<CxException>(() => _service.DownloadPrivateAsync(id));
    }

    [Fact]
    public async Task UpdateAsync_MapsUpdatesAndPersists()
    {
        var id = Guid.NewGuid();
        var existing = CreateFile(id, isPublic: false);
        var request = new UpdateFileRequest { Description = "Updated description", IsPublic = true };
        var response = new UploadFileResponse { Id = id, Description = request.Description, IsPublic = true };

        _repository.Setup(repository => repository.GetByIdOrDefaultAsync(id))
            .ReturnsAsync(existing);
        _repository.Setup(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);
        _mapper.Setup(mapper => mapper.Map<UploadedFile>(request)).Returns(existing);
        _mapper.Setup(mapper => mapper.Map<UploadFileResponse>(existing)).Returns(response);

        var result = await _service.UpdateAsync(id, request);

        Assert.Same(response, result);
        _repository.Verify(repository => repository.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ThrowsNotFoundWhenFileDoesNotExist()
    {
        var id = Guid.NewGuid();
        _repository.Setup(repository => repository.GetByIdOrDefaultAsync(id))
            .ReturnsAsync((UploadedFile?)null);

        await Assert.ThrowsAsync<CxException>(() => _service.UpdateAsync(id, new UpdateFileRequest()));
        _repository.Verify(repository => repository.UpdateAsync(It.IsAny<UploadedFile>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RemoveAsync_RemovesAndSavesChanges()
    {
        var id = Guid.NewGuid();
        var entity = CreateFile(id, isPublic: false);
        _repository.Setup(repository => repository.GetByIdOrDefaultAsync(id)).ReturnsAsync(entity);
        _repository.Setup(repository => repository.RemoveAsync(entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        await _service.RemoveAsync(id);

        _repository.Verify(repository => repository.RemoveAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_ThrowsNotFoundWhenFileDoesNotExist()
    {
        var id = Guid.NewGuid();
        _repository.Setup(repository => repository.GetByIdOrDefaultAsync(id)).ReturnsAsync((UploadedFile?)null);

        await Assert.ThrowsAsync<CxException>(() => _service.RemoveAsync(id));
        _repository.Verify(repository => repository.RemoveAsync(It.IsAny<UploadedFile>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    private static UploadFileRequest CreateUploadRequest(byte[] bytes)
    {
        var stream = new MemoryStream(bytes);
        var formFile = new FormFile(stream, 0, bytes.Length, "File", "photo.jpg")
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg"
        };

        return new UploadFileRequest { File = formFile, IsPublic = true };
    }

    private static UploadedFile CreateFile(Guid id, bool isPublic) => new()
    {
        Id = id,
        OriginalFileName = isPublic ? "photo.jpg" : "private.pdf",
        SystemFileName = "stored-file",
        Extension = isPublic ? ".jpg" : ".pdf",
        FileType = isPublic ? "image/jpeg" : "application/pdf",
        Content = new byte[] { 4, 5, 6 },
        IsPublic = isPublic,
        IsSavedInDatabase = true
    };
}
