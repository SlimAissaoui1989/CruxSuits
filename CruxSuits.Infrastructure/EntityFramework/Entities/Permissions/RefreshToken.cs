using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions
{
    public class RefreshToken : EntityIdentityIntKeyBase
    {
        public int UserId { get; set; }

        [MaxLength(511)]
        public string AcessToken{ get; set; } = null!;

        [MaxLength(255)]
        public string RefreshTokenValue{ get; set; } = null!;

        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [MaxLength(150)]
        public string Password { get; set; } = null!;

        public Guid DeviceId { get; set; }

        public DateTime Expires { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;


        public User? User { get; set; }
    }
}
