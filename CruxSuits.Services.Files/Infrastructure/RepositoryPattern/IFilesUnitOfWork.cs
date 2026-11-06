using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern
{
    public interface IFilesUnitOfWork : IUnitOfWork<FilesContext>
    {
        IUploadedFileRepository UploadedFile { get; }
        IUploadedFileTraceRepository UploadedFileTrace { get; }
    }
}
