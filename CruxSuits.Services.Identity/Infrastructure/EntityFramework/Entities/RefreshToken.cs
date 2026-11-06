using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities
{
    public class RefreshToken : EntityBase<long>
    {
        public Guid UserId { get; set; }

        [MaxLength(511)]
        public string AcessToken { get; set; } = null!;

        [MaxLength(255)]
        public string RefreshTokenValue { get; set; } = null!;

        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [MaxLength(150)]
        public string Password { get; set; } = null!;

        public Guid DeviceId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;


        public User? User { get; set; }
    }
}
