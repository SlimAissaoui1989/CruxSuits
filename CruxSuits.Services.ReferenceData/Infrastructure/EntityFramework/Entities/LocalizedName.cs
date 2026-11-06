using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities
{
    public class LocalizedName : EntityBase<long>
    {
        [MaxLength(10)]
        public string LanguageId { get; set; } = null!;

        [MaxLength(100)]
        public string ResourceType { get; set; } = null!;

        [MaxLength(100)]
        public string ResourceId { get; set; } = null!;

        [MaxLength(250)]
        public string Name { get; set; } = null!;

        public Language? Language { get; set; }
    }
}
