using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations
{
    public class CountryName : EntityBase<string>
    {
        [Key, MaxLength(5)]
        public override string Id { get; set; } = null!;

        public string LanguageId { get; set; } = null!;

        public string CountryId { get; set; } = null!;

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string Nationality { get; set; } = null!;


        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        public Country? Country { get; set; }
    }
}