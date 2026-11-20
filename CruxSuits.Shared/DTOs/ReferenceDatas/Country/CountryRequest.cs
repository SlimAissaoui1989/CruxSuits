using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.Country;

public sealed record CountryRequest
{
    [Required, MaxLength(2)]
    public string Id { get; set; } = null!;

    [Required, MaxLength(3)]
    public string Alpha3Code { get; set; } = null!;

    [Required, MaxLength(3)]
    public string NumericCode { get; set; } = null!;

    [MaxLength(10)]
    public string? PhoneCode { get; set; }

    [Required, MaxLength(3)]
    public string CurrencyId { get; set; } = null!;
}
