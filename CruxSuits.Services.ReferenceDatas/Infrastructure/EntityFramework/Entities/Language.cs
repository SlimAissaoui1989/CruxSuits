using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class Language : SoftDeleteEntity<string>
    {
        [Key, MaxLength(10)]
        public override string Id { get; set; } = null!;

        [MaxLength(100)]
        public string EnglishName { get; set; } = null!;

        [MaxLength(100)]
        public string NativeName { get; set; } = null!;

        public bool IsRightToLeft { get; set; }
    }
}
