using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Shared.DTOs.Identities
{
    public record LoginResponse
    {
        public string AccessToken { get; set; } = null!;
    }
}
