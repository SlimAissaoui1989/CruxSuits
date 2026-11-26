using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class Currency : SoftDeleteEntity<string>
    {
        [Key, MaxLength(3)]
        public override string Id { get; set; } = null!;

        [MaxLength(3)]
        public string NumericCode { get; set; } = null!;

        [MaxLength(10)]
        public string? Symbol { get; set; } = null!;

        public char DecimalSeparator { get; set; } = '.';

        public byte? MinorUnit { get; set; }


        public ICollection<CurrencyName> Names { get; set; } = new List<CurrencyName>();
    }
}
