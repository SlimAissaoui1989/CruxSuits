using Microsoft.AspNetCore.Http;

namespace CruxSuits.Shared.DTOs.Files
{
    public record UploadFileRequest
    {
        public IFormFile File { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsSavedInDatabase { get; set; }

        public bool IsPublic { get; set; }
    }
}
