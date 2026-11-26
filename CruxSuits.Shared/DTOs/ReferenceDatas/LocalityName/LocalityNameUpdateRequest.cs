using CruxSuits.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;

public sealed record LocalityNameUpdateRequest
{
    [Required, MaxLength(250), LettersAndSpaces]
    public string Name { get; set; } = null!;
}
