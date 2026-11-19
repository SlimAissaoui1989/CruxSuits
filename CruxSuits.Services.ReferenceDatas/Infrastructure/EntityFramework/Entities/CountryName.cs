using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class CountryName : EntityBase<long>
    {
        [MaxLength(2)]
        public string CountryId { get; set; } = null!;

        [MaxLength(10)]
        public string LanguageId { get; set; } = null!;

        [MaxLength(250)]
        public string Name { get; set; } = null!;

        [MaxLength(250)]
        public string Nationality { get; set; } = null!;

        public Country? Country { get; set; }

        public Language? Language { get; set; }
    }
}
