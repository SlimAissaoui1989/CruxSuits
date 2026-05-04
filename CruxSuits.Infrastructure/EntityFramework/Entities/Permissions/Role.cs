using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions
{
    public class Role : EntityIdentityIntKeyBase
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string Level { get; set; } = null!;

        public int? ParentRoleId { get; set; }


        [ForeignKey(nameof(ParentRoleId))]
        public Role? ParentRole { get; set; }

    }
}