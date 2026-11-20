namespace CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;

public sealed record CountryNameResponse
{
    public string LanguageId { get; set; } = null!;
    public string LanguageName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Nationality { get; set; } = null!;
}
