using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations
{
    public class Country : SoftDeleteEntity<string>
    {
        [Key, MaxLength(2)]
        public override string Id { get; set; } = null!;

        [MaxLength(3)]
        public string Id3Digit { get; set; } = null!;

        [MaxLength(5)]
        public string PhoneCode { get; set; } = null!;

        public string CurrencyId { get; set; } = null!;


        [ForeignKey(nameof(CurrencyId))]
        public Currency? Currency { get; set; }

        public ICollection<CountryName>? Names { get; set; }
    }
}
