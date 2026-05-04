using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources
{
    public class UiResourceName : EntityBase<string>
    {
        [Key, MaxLength(15)]
        public override string Id
        {
            get => $"{UiResourceId}_{LanguageId}";
            set => _ = value;
        }

        public int UiResourceId { get; set; }

        public string LanguageId { get; set; } = null!;

        [MaxLength(100)]
        public string Name { get; set; } = null!;


        public UiResource? Resource { get; set; }

        public Language? Language { get; set; }
    }
}