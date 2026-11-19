namespace CruxSuits.Shared.DTOs.ReferenceDatas;

public sealed record CountryRequest
{
    public string Id { get; set; } = null!;
    public string Alpha3Code { get; set; } = null!;
    public string NumericCode { get; set; } = null!;
    public string? PhoneCode { get; set; }
    public string CurrencyId { get; set; } = null!;
}
