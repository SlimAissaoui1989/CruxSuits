namespace CruxSuits.Shared.DTOs.ReferenceDatas.Country;

public sealed record CountryPhoneCodeResponse
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? PhoneCode { get; set; }
}
