namespace CruxSuits.Shared.DTOs.Files;

public sealed record UpdateFileRequest
{
    public string? Description { get; set; }

    public bool IsPublic { get; set; }

    public bool IsSavedInDatabase { get; set; }
}
