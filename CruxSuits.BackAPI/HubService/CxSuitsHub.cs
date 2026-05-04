using CruxIT.Library.API;
using CruxSuits.BackAPI.Models;
using CruxSuits.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace CruxSuits.BackAPI.HubService
{
    public class CxSuitsHub : Hub<ICxSuitsHub>
    {
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            HubConnectionInfo? hubConnectionInfo = VolatileDataStore.RefreshTokenConnections
                .SelectMany(s=>s.Connections)
                .FirstOrDefault(c=>c.ConnectionId == Context.ConnectionId);
            if (hubConnectionInfo != null)
            {
                hubConnectionInfo.RefreshTokenConnection.Connections.Remove(hubConnectionInfo);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
