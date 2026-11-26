namespace CruxSuits.Shared.DTOs.ReferenceDatas.Currency;

public sealed record CurrencyResponse
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public string NumericCode { get; set; } = null!;
}
