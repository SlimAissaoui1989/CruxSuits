using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;

public sealed record CountryNameRequest
{
    [Required, MaxLength(10)]
    public string LanguageId { get; set; } = null!;

    [Required, MaxLength(250)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(250)]
    public string Nationality { get; set; } = null!;
}
