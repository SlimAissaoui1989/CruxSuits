using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities
{
    public class User : Entity<Guid>
    {
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [MaxLength(150)]
        public string Password { get; set; } = null!;

        public bool IsEnabled { get; set; }

        public int RoleId { get; set; }

        public Guid ProfilePictureId { get; set; }


        public Role? Role { get; set; }
    }
}
