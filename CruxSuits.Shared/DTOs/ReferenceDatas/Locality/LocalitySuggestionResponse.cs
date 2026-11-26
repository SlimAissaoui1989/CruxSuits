namespace CruxSuits.Shared.DTOs.ReferenceDatas.Locality;

public sealed record LocalitySuggestionResponse
{
    public string Id { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string AdministrativeAreaId { get; set; } = null!;
    public string AdministrativeAreaCode { get; set; } = null!;
    public string AdministrativeAreaName { get; set; } = null!;
    public string CountryId { get; set; } = null!;
    public string CountryName { get; set; } = null!;
    public string LocalityId => Id;
    public string DisplayLabel => $"{Name}, {AdministrativeAreaName}, {CountryName}";
}
