using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class Locality : SoftDeleteEntity<string>
    {
        [Key, MaxLength(11)]
        public override string Id { get => base.Id; set => base.Id = value; }

        [MaxLength(4)]
        public string Code { get; set; } = null!;

        [MaxLength(6)]
        public string AdministrativeAreaId { get; set; } = null!;

        public AdministrativeArea? AdministrativeArea { get; set; }

        public ICollection<LocalityName> Names { get; set; } = new List<LocalityName>();
    }
}
