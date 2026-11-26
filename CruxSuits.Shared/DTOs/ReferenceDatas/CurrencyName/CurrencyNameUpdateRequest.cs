using CruxSuits.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;

public sealed record CurrencyNameUpdateRequest
{
    [Required, MaxLength(250)]
    [LettersAndSpaces]
    public string Name { get; set; } = null!;
}
