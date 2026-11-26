using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Shared.DTOs.ReferenceDatas.Language;

public sealed record UpdateLanguageRequest
{
    [Required, MaxLength(100)]
    public string EnglishName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string NativeName { get; set; } = null!;

    public bool IsRightToLeft { get; set; }
}
