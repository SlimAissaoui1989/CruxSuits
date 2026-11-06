using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Shared.DTOs.Identities
{
    public record LoginRequest
    {
        public string Identifier { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
