using CruxSuits.Services.Identities.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.Identities;
using System.Net;

namespace CruxSuits.Services.Identities.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request, IPAddress? ip, Guid deviceId);
        string GetAccessToken(User user);
    }
}
