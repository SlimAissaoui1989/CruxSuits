using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions
{
    public class User : EntityBase<int>
    {
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [MaxLength(150)]
        public string Password { get; set; } = null!;

        public int RoleId { get; set; }

        public bool IsEnabled { get; set; }

        public Guid? ProfilePictureId {  get; set; }


        [ForeignKey(nameof(Id))]
        public Person? Person { get; set; }

        public Role? Role { get; set; }

        [ForeignKey(nameof(ProfilePictureId))]
        public DigitalFile? ProfilePicture { get; set; }
    }
}