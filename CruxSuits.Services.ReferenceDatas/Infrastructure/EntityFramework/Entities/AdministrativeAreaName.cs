using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities
{
    public class AdministrativeAreaName : EntityBase<int>
    {
        [MaxLength(6)]
        public string AdministrativeAreaId { get; set; } = null!;

        [MaxLength(10)]
        public string LanguageId { get; set; } = null!;

        [MaxLength(250)]
        public string Name { get; set; } = null!;

        public AdministrativeArea? AdministrativeArea { get; set; }
        public Language? Language { get; set; }
    }
}
