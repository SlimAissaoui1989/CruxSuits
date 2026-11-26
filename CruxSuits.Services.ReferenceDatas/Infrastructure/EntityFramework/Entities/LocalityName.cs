using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class LocalityName : EntityBase<int>
    {
        [MaxLength(11)]
        public string LocalityId { get; set; } = null!;

        [MaxLength(10)]
        public string LanguageId { get; set; } = null!;

        [MaxLength(250)]
        public string Name { get; set; } = null!;

        public Locality? Locality { get; set; }
        public Language? Language { get; set; }
    }
}
