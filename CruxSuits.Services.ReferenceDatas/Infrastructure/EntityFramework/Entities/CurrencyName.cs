using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class CurrencyName : EntityBase<int>
    {
        [MaxLength(3)]
        public string CurrencyId { get; set; } = null!;

        [MaxLength(10)]
        public string LanguageId { get; set; } = null!;

        [MaxLength(250)]
        public string Name { get; set; } = null!;

        public Currency? Currency { get; set; }

        public Language? Language { get; set; }
    }
}
