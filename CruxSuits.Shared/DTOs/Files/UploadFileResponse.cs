namespace CruxSuits.Shared.DTOs.Files
{
    public record UploadFileResponse
    {
        public Guid Id { get; set; }
        public string OriginalFileName { get; set; } = null!;
        public string SystemFileName { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public long SizeInBytes { get; set; }
        public bool IsSavedInDatabase { get; set; }
        public string? Description { get; set; }
        public bool IsPublic { get; set; }
    }
}
