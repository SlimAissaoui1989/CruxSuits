namespace CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;

public sealed record LocalityNameResponse
{
    public string LanguageId { get; set; } = null!;
    public string LanguageName { get; set; } = null!;
    public string Name { get; set; } = null!;
}
