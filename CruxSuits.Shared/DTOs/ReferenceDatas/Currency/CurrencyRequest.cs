using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.Currency;

public sealed record CurrencyRequest
{
    [Required, MaxLength(3)]
    public string Id { get; set; } = null!;

    [Required, MaxLength(3)]
    public string NumericCode { get; set; } = null!;

    [MaxLength(10)]
    public string? Symbol { get; set; }

    [AllowedValues('.', ',', ErrorMessage = "The decimal separator must be a period or a comma.")]
    public char DecimalSeparator { get; set; }

    public byte? MinorUnit { get; set; }
}
