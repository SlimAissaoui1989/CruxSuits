using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class Country : SoftDeleteEntity<string>
    {
        [Key, MaxLength(2)]
        public override string Id { get; set; } = null!;

        [MaxLength(3)]
        public string Alpha3Code { get; set; } = null!;

        [MaxLength(3)]
        public string NumericCode { get; set; } = null!;

        [MaxLength(10)]
        public string? PhoneCode { get; set; }

        public string CurrencyId { get; set; } = null!;


        public Currency? Currency { get; set; }

        public ICollection<CountryName> CountryNames { get; set; } = new List<CountryName>();
    }
}
