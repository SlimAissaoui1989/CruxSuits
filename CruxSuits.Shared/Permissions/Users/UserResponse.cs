using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Shared.Permissions.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? ProfilePictureSrc { get; set; }
    }
}
