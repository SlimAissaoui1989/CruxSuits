using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.Files
{
    public record UploadFileRequest
    {
        [Required]
        public IFormFile File { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsSavedInDatabase { get; set; }

        public bool IsPublic { get; set; }
    }
}
