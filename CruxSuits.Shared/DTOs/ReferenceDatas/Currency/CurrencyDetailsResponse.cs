namespace CruxSuits.Shared.DTOs.ReferenceDatas.Currency;

public sealed record CurrencyDetailsResponse
{
    public string Id { get; set; } = null!;
    public string NumericCode { get; set; } = null!;
    public string? Symbol { get; set; }
    public char DecimalSeparator { get; set; }
    public byte? MinorUnit { get; set; }
}
