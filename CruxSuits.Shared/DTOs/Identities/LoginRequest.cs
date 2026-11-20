using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Shared.DTOs.Identities
{
    public record LoginRequest
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Identifier { get; set; } = null!;

        [System.ComponentModel.DataAnnotations.Required]
        public string Password { get; set; } = null!;
    }
}
