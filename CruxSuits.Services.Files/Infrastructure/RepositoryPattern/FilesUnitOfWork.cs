using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CruxSuits.Services.Files.Infrastructure.RepositoryPattern
{
    public class FilesUnitOfWork : UnitOfWork<FilesContext>, IFilesUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private IUploadedFileRepository? _uploadedFile;
        private IUploadedFileTraceRepository? _uploadedFileTrace;

        public FilesUnitOfWork(FilesContext context, IServiceProvider serviceProvider)
            : base(context)
        {
#if DEBUG
            context.Database.EnsureCreated();
#endif
            _serviceProvider = serviceProvider;
        }

        public IUploadedFileRepository UploadedFile =>
            _uploadedFile ??= _serviceProvider.GetRequiredService<IUploadedFileRepository>();

        public IUploadedFileTraceRepository UploadedFileTrace =>
            _uploadedFileTrace ??= _serviceProvider.GetRequiredService<IUploadedFileTraceRepository>();
    }
}
