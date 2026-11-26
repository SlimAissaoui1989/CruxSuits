namespace CruxSuits.Shared.DTOs.ReferenceDatas.Locality;

public sealed record LocalityDetailsResponse
{
    public string Id { get; set; } = null!;
    public string AdministrativeAreaId { get; set; } = null!;
    public string Code { get; set; } = null!;
}
