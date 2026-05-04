using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations
{
    public class Currency : SoftDeleteEntity<string>
    {
        [Key, MaxLength(3)]
        public override string Id { get; set; } = null!;

        [MaxLength(3)]
        public string Num { get; set; } = null!;

        [MaxLength(5)]
        public string? Symbol { get; set; }

        public int DecimalSeparator { get; set; }

        public ICollection<CurrencyName>? Names { get; set; }
    }
}