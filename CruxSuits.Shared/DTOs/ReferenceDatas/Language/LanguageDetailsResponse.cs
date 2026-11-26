namespace CruxSuits.Shared.DTOs.ReferenceDatas.Language;

public sealed record LanguageDetailsResponse
{
    public string Id { get; set; } = null!;
    public string EnglishName { get; set; } = null!;
    public string NativeName { get; set; } = null!;
    public bool IsRightToLeft { get; set; }
}
