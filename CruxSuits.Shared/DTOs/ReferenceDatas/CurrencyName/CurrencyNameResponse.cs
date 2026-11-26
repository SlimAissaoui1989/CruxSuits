namespace CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;

public sealed record CurrencyNameResponse
{
    public string LanguageId { get; set; } = null!;
    public string LanguageName { get; set; } = null!;
    public string Name { get; set; } = null!;
}
