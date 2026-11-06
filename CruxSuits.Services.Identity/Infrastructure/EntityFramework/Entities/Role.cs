using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities
{
    public class Role : EntityIdentityIntKey
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public int Level { get; set; }

        public int? ParentRoleId { get; set; }


        [ForeignKey(nameof(ParentRoleId))]
        public Role? ParentRole { get; set; }

    }
}