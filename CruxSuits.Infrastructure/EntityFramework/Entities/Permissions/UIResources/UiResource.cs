using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources
{
    public class UiResource : EntityIdentityIntKeyBase
    {
        [MaxLength(511)]
        public string Key { get; set; } = null!;

        public int? ParentUiResourceId { get; set; }

        public string UiResourceTypeId { get; set; } = null!;

        //public int SortOrder { get; set; }

        //public string? RelatedDTOName { get; set; }

        //public string? RelatedFieldName { get; set; }



        [ForeignKey(nameof(ParentUiResourceId))]
        public UiResource? Parent { get; set; }

        public UiResourceType? UiResourceType { get; set; }

        public ICollection<UiResource>? Children { get; set; }

        public ICollection<UiResourceName>? Names { get; set; }

    }
}