using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations
{
    public class CurrencyName : EntityBase<string>
    {
        [Key, MaxLength(10)]
        public override string Id { get; set; } = null!;
        public string LanguageId { get; set; } = null!;

        public string CurrencyId { get; set; } = null!;

        [MaxLength(50)]
        public string Name { get; set; } = null!;


        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        [ForeignKey(nameof(CurrencyId))]
        public Currency? Currency { get; set; }
    }
}