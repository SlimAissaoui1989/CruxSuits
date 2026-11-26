using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.Locality;

public sealed record LocalityRequest
{
    [Required, RegularExpression("[A-Z]{3,4}")]
    public string Code { get; set; } = null!;
}
