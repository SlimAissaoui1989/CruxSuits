namespace CruxSuits.Shared.DTOs.ReferenceDatas;

public sealed record UpdateCountryRequest
{
    public string Alpha3Code { get; set; } = null!;
    public string NumericCode { get; set; } = null!;
    public string? PhoneCode { get; set; }
    public string CurrencyId { get; set; } = null!;
    public ICollection<long>? Names { get; set; }
}
