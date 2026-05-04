using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Shared.Permissions.Users;
using System.Net;

namespace CruxSuits.BackAPI.Services.Permissions.Users
{
    public interface IUserService
    {
        Task<User?> LoginAsync(LoginRequest request, IPAddress? ip);
        string GetAccessToken(User user, string jwtKey, string issuer);
        Task<string> GetRefreshTokenAsync(User user, string accessToken, Guid deviceId);
        public Task RevokeRefreshTokenAsync(HttpRequest request);
        void AppendAuthTokenCookie(HttpResponse response, string refreshToken);
        void DeleteAuthTokenCookie(HttpResponse response);
        Task<string> TryToRefreshTokenAsync(HttpRequest request, HttpResponse response, string jwtKey, string issuer);
        void AddNewConnection(string refreshToken, int userId, string connectionId, Guid deviceId);
        void RemoveConnection(string refreshToken);
    }
}
