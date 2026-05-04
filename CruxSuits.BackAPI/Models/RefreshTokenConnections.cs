using System.Collections.Concurrent;

namespace CruxSuits.BackAPI.Models
{
    public class RefreshTokenConnections
    {
        public string RefreshToken { get; set; } = null!;
        public List<HubConnectionInfo> Connections { get; set; } = new();
    }
}
