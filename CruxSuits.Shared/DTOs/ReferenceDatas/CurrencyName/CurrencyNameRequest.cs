using CruxSuits.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;

public sealed record CurrencyNameRequest
{
    [Required, MaxLength(10)]
    public string LanguageId { get; set; } = null!;

    [Required, MaxLength(250)]
    [LettersAndSpaces]
    public string Name { get; set; } = null!;
}
