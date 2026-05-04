using CruxSuits.BackAPI.Models;
using System.Collections.Concurrent;

namespace CruxSuits.BackAPI
{
    public static class VolatileDataStore
    {
        public static List<RefreshTokenConnections> RefreshTokenConnections { get; set; } = new();
        public static readonly object RefreshTokenConnectionsLock = new();
    }
}
