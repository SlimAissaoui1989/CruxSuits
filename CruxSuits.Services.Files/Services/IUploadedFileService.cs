using CruxSuits.Services.Files.DTOs;
using CruxSuits.Shared.DTOs.Files;

namespace CruxSuits.Services.Files.Services
{
    public interface IUploadedFileService
    {
        Task<UploadFileResponse> UploadAsync(UploadFileRequest request, CancellationToken cancellationToken = default);
        Task<UploadFileResponse> UpdateAsync(Guid id, UpdateFileRequest request, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        Task<FileDownloadResult> DownloadAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
