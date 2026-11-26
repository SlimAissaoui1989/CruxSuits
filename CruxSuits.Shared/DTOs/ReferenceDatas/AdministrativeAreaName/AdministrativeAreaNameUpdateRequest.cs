using CruxSuits.Shared.Validation;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeAreaName;

public sealed record AdministrativeAreaNameUpdateRequest
{
    [Required, MaxLength(250), LettersAndSpaces]
    public string Name { get; set; } = null!;
}
