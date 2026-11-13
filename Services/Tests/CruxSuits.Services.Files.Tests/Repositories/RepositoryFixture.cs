using System;
using System.Collections.Generic;
using System.Text;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Interceptors;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CruxSuits.Services.Files.Tests.Repositories;

internal sealed class RepositoryFixture : IDisposable
{
    private readonly Mock<IFilesUnitOfWork> _unitOfWork = new();
    private readonly Mock<IUploadedFileTraceRepository> _traceRepository = new();

    public RepositoryFixture()
    {
        RootPath = Path.Combine(Path.GetTempPath(), "CruxSuitsRepositoryTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(RootPath);

        var options = new DbContextOptionsBuilder<FilesContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        Context = new FilesContext(options);
        DbSet<UploadedFile> uploadedFilesDbSet = Context.UploadedFiles;

        PendingOperations = new PendingFileStorageOperations(NullLogger<PendingFileStorageOperations>.Instance);

        _unitOfWork
            .As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(unitOfWork => unitOfWork.DbContext)
            .Returns(Context);
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.DbContext).Returns(Context);
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.CurrentUserName).Returns("test-user");
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.CurrentUserId).Returns(Guid.NewGuid());
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.UploadedFileTrace).Returns(_traceRepository.Object);
        _unitOfWork.Setup(unitOfWork => unitOfWork.Set<UploadedFile>()).Returns(uploadedFilesDbSet);

        _traceRepository
            .Setup(repository => repository.AddAsync(It.IsAny<UploadedFileTrace>(), It.IsAny<CancellationToken>()))
            .Returns((UploadedFileTrace trace, CancellationToken _) => Task.FromResult(trace));

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["FileStorage:RootPath"] = RootPath
            })
            .Build();
        var environment = new Mock<IWebHostEnvironment>();
        environment.SetupGet(value => value.ContentRootPath).Returns(RootPath);

        Repository = new UploadedFileRepository(
            _unitOfWork.Object,
            configuration,
            environment.Object,
            PendingOperations);
    }

    public FilesContext Context { get; }
    public UploadedFileRepository Repository { get; }
    public PendingFileStorageOperations PendingOperations { get; }
    public string RootPath { get; }

    public UploadedFile CreateStoredDiskFile(byte[] content, bool isPublic)
    {
        var userDirectory = Path.Combine(RootPath, "test-user_test");
        Directory.CreateDirectory(userDirectory);
        var path = Path.Combine(userDirectory, $"{Guid.NewGuid():N}.jpg");
        File.WriteAllBytes(path, content);
        var entity = CreateEntity(null, isPublic, isSavedInDatabase: false);
        entity.AbsolutePath = path;
        return entity;
    }

    public static UploadedFile CreateEntity(byte[]? content, bool isPublic, bool isSavedInDatabase) => new()
    {
        OriginalFileName = "photo.jpg",
        SystemFileName = "photo.jpg",
        Extension = ".jpg",
        FileType = "image/jpeg",
        Description = "File description",
        SizeInBytes = content?.LongLength ?? 0,
        Content = content,
        IsPublic = isPublic,
        IsSavedInDatabase = isSavedInDatabase
    };

    public void Dispose()
    {
        Context.Dispose();
        if (Directory.Exists(RootPath))
            Directory.Delete(RootPath, recursive: true);
    }
}
