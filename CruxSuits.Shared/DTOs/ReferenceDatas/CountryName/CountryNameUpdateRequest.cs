using CruxSuits.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;

public sealed record CountryNameUpdateRequest
{
    [Required, MaxLength(250)]
    [LettersAndSpaces]
    public string Name { get; set; } = null!;

    [Required, MaxLength(250)]
    [LettersAndSpaces]
    public string Nationality { get; set; } = null!;
}
