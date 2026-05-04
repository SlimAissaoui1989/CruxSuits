using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Shared.Permissions.Users
{
    public class LoginResponse
    {
        public UserResponse User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
