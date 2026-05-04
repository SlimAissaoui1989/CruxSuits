using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources
{
    public class UiResourceTypeName : EntityBase<string>
    {
        [Key, MaxLength(15)]
        public override string Id
        {
            get => $"{UiResourceTypeId}_{LanguageId}";
            set => _ = value;
        }

        public string UiResourceTypeId { get; set; } = null!;

        public string LanguageId { get; set; } = null!;

        [MaxLength(50)]
        public string Name { get; set; } = null!;


        public UiResource? UiResource { get; set; }
        public Language? Language { get; set; }

    }
}