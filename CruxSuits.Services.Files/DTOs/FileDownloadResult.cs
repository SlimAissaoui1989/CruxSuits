namespace CruxSuits.Services.Files.DTOs
{
    public sealed record FileDownloadResult(Stream Content, string ContentType, string FileName);
}
