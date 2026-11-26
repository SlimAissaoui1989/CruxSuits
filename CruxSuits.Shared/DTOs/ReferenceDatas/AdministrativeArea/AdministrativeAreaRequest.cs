using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;

public sealed record AdministrativeAreaRequest
{
    [Required, RegularExpression("[A-Z]{2,3}")]
    public string Code { get; set; } = null!;
}
