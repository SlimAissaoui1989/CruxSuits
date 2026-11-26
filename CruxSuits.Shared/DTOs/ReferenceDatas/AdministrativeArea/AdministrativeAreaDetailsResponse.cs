namespace CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;

public sealed record AdministrativeAreaDetailsResponse
{
    public string Id { get; set; } = null!;
    public string CountryId { get; set; } = null!;
    public string Code { get; set; } = null!;
}
