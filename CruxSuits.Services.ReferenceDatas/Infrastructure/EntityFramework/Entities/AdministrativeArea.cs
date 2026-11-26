using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class AdministrativeArea : SoftDeleteEntity<string>
    {
        [Key, MaxLength(6)]
        public override string Id { get => base.Id; set => base.Id = value; }

        [MaxLength(3)]
        public string Code { get; set; } = null!;

        [MaxLength(2)]
        public string CountryId { get; set; } = null!;

        public Country? Country { get; set; }
        public ICollection<AdministrativeAreaName> Names { get; set; } = new List<AdministrativeAreaName>();
    }
}
