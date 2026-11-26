namespace CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeAreaName;

public sealed record AdministrativeAreaNameResponse
{
    public string LanguageId { get; set; } = null!;
    public string LanguageName { get; set; } = null!;
    public string Name { get; set; } = null!;
}
