namespace CruxSuits.Shared.DTOs.ReferenceDatas.Country;

public sealed record CountryResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; } 
    public string Alpha3Code { get; set; } = null!;
    public string NumericCode { get; set; } = null!;
    public string? PhoneCode { get; set; }
    public string CurrencyId { get; set; } = null!;
}
