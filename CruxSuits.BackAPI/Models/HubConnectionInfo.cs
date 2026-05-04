namespace CruxSuits.BackAPI.Models
{
    public class HubConnectionInfo
    {
        public string ConnectionId { get; set; } = null!;

        public Guid DeviceId { get; set; }

        public int UserId { get; set; }

        public RefreshTokenConnections RefreshTokenConnection { get; set; } = null!;
    }
}
