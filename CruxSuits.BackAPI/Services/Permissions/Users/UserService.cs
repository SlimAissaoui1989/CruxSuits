using Azure;
using Azure.Core;
using CruxIT.Library;
using CruxIT.Library.API;
using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxIT.Library.Exceptions;
using CruxIT.Library.GeoLocations;
using CruxSuits.BackAPI.Models;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Shared;
using CruxSuits.Shared.Permissions.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using PhoneNumbers;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CruxSuits.BackAPI.Services.Permissions.Users
{
    public class UserService : IUserService
    {
        private readonly ICruxSuitsUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(ICruxSuitsUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, ILogger<UserService> logger)
        {
            this._unitOfWork = unitOfWork;
            this._passwordHasher = passwordHasher;
            this._logger = logger;
        }

        public async Task<User?> LoginAsync(LoginRequest request, IPAddress? ip)
        {
            User? user = null;

            if (CxStringHelper.IsEmail(request.LoginId))
            {
                user = await _unitOfWork.User.GetByEmailAsync(request.LoginId);
            }
            else if (CxStringHelper.IsPhoneNumber(request.LoginId))
            {
                string phone = request.LoginId;
                if (ip != null)
                {
                    CxIPLocation? iPLocation = await CxIPLocation.GetGeoLocationByIp(ip);
                    if (iPLocation != null)
                    {
                        phone = CxPhoneHelper.GetPhoneNumberInterNationalFormat(phone, iPLocation.CountryCode);
                    }
                }
                user = await _unitOfWork.User.GetByPhoneAsync(phone);
            }
            else
            {
                user = await _unitOfWork.User.GetByUserNameAsync(request.LoginId);
            }

            if (user == null) return null;

            var verification = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            switch (verification)
            {
                case PasswordVerificationResult.SuccessRehashNeeded:
                    user.Password = _passwordHasher.HashPassword(user, request.Password);
                    await _unitOfWork.User.UpdateAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                    break;
                case PasswordVerificationResult.Failed:
                    return null;
            }
            return user;
        }

        public string GetAccessToken(User user, string jwtKey, string issuer)
        {
            var claims = new[]
            {
                new Claim(CxLabraryApiConstValues.UserIdTag, user.Id.ToString()),
                new Claim(CxLabraryApiConstValues.UserNameTag, user.UserName.ToString()),
                new Claim(CxLabraryApiConstValues.RoleIdTag, user.RoleId.ToString()),
                new Claim(CxLabraryApiConstValues.RoleNameTag, user.Role!.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            return CxTokenHelper.GenerateJSONWebToken(claims, jwtKey, issuer, (int)ConstsValues.AccessTokenExpiredTime.TotalMinutes);
        }

        public async Task<string> GetRefreshTokenAsync(User user, string accessToken, Guid deviceId)
        {
            var randomBytes = new byte[128];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            string token = Convert.ToBase64String(randomBytes);

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                AcessToken = token,
                RefreshTokenValue = token,
                Password = user.Password,
                DeviceId = deviceId,
                Expires = DateTime.UtcNow.Add(ConstsValues.RefreshTokenExpiredTime),
                UserName = user.UserName,
                IsRevoked = false
            };

            await _unitOfWork.RefreshToken.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return token;
        }

        public async Task RevokeRefreshTokenAsync(HttpRequest request)
        {
            try
            {
                request.Cookies.TryGetValue(ConstsValues.AuthTokenCookieTag, out string? refreshToken);
                if (string.IsNullOrEmpty(refreshToken))
                    return;

                RefreshToken? refreshTokenEntity = await _unitOfWork.RefreshToken.GetFirstOrDefaultAsync(c => c.RefreshTokenValue == refreshToken);
                if (refreshTokenEntity == null) return;

                refreshTokenEntity.IsRevoked = true;

                await _unitOfWork.RefreshToken.UpdateAsync(refreshTokenEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch { }
        }

        public void AppendAuthTokenCookie(HttpResponse response, string refreshToken)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,                // true in prod (HTTPS)
                SameSite = SameSiteMode.None, // for cross-origin front/back
                Expires = DateTime.UtcNow.Add(ConstsValues.RefreshTokenExpiredTime),
            };

            response.Cookies.Append(ConstsValues.AuthTokenCookieTag, refreshToken, cookieOptions);
        }

        public void DeleteAuthTokenCookie(HttpResponse response)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,                // true in prod (HTTPS)
                SameSite = SameSiteMode.None, // for cross-origin front/back
                Expires = DateTime.UtcNow.Add(ConstsValues.RefreshTokenExpiredTime),
            };

            response.Cookies.Delete(ConstsValues.AuthTokenCookieTag, cookieOptions);
        }

        private CxException CxExceptionOnUnauthorized(string message)
        {
            _logger.LogWarning("Refresh token failed: {message}", message);
            return new CxException(CxExceptionTypes.Unauthorized);
        }

        protected (string connectionId, Guid deviceId, string accessToken, string oldToken) ExtractRequestDataOrThrow(HttpRequest request)
        {
            if (!request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out var connectionIdValue) ||
                string.IsNullOrEmpty(connectionIdValue.ToString()))
                throw CxExceptionOnUnauthorized("Missing ConnectionId");

            if (!request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out var deviceIdValue) ||
                string.IsNullOrEmpty(deviceIdValue.ToString()))
                throw CxExceptionOnUnauthorized("Missing DeviceId ");

            if (!request.Headers.TryGetValue(SharedConstValues.BearerTag, out var accessTokenValue) ||
                string.IsNullOrEmpty(accessTokenValue.ToString()))
                throw CxExceptionOnUnauthorized("Missing AccessTokenHeader");

            if (!request.Cookies.TryGetValue(ConstsValues.AuthTokenCookieTag, out var oldToken) ||
                string.IsNullOrEmpty(oldToken))
                throw CxExceptionOnUnauthorized("Missing RefreshTokenCookie");

            if (!Guid.TryParse(deviceIdValue.ToString(), out var deviceId))
                throw CxExceptionOnUnauthorized("Invalid DeviceId");

            return (
                connectionId: connectionIdValue.ToString(),
                deviceId: deviceId,
                accessToken: accessTokenValue.ToString(),
                oldToken: oldToken
            );
        }
        protected async Task<RefreshToken> GetValidExistingRefreshTokenOrThrowAsync(string oldToken, string accessToken)
        {
            var existingToken = await _unitOfWork.RefreshToken
                .GetFirstOrDefaultAsync(c =>
                    c.RefreshTokenValue == oldToken &&
                    c.AcessToken == accessToken);

            if (existingToken == null ||
                existingToken.IsRevoked ||
                existingToken.Expires < DateTime.UtcNow)
            {
                throw CxExceptionOnUnauthorized("Invalid RefreshToken");
            }

            return existingToken;
        }

        protected RefreshTokenConnections ValidateConnectionOrThrow(string oldToken, string connectionId, RefreshToken existingToken)
        {
            RefreshTokenConnections? refreshTokenConnection;
            HubConnectionInfo? hubConnectionInfo;

            lock (VolatileDataStore.RefreshTokenConnectionsLock)
            {
                refreshTokenConnection = VolatileDataStore.RefreshTokenConnections
                    .FirstOrDefault(c => c.RefreshToken == oldToken);

                if (refreshTokenConnection == null)
                    throw CxExceptionOnUnauthorized("Token NotFound");

                hubConnectionInfo = refreshTokenConnection.Connections
                    .FirstOrDefault(c => c.ConnectionId == connectionId);

                if (hubConnectionInfo == null)
                    throw CxExceptionOnUnauthorized("Connection NotFound");

                if (hubConnectionInfo.DeviceId != existingToken.DeviceId)
                    throw CxExceptionOnUnauthorized("Device Mismatch");
            }

            return refreshTokenConnection;
        }

        protected async Task<User> GetValidUserOrThrowAsync(RefreshToken existingToken)
        {
            var user = await _unitOfWork.User.GetByUserNameAsync(existingToken.UserName);
            if (user == null)
                throw CxExceptionOnUnauthorized("User NotFound");

            if (user.Password != existingToken.Password)
                throw CxExceptionOnUnauthorized("Invalid Password");

            return user;
        }

        public async Task<string> TryToRefreshTokenAsync(
            HttpRequest request,
            HttpResponse response,
            string jwtKey,
            string issuer)
        {
            var (connectionId, deviceId, accessToken, oldToken) = ExtractRequestDataOrThrow(request);

            var existingToken = await GetValidExistingRefreshTokenOrThrowAsync(oldToken, accessToken);

            var refreshTokenConnection =
                ValidateConnectionOrThrow(oldToken, connectionId, existingToken);

            var user = await GetValidUserOrThrowAsync(existingToken);

            existingToken.IsRevoked = true;
            string newAccessToken = GetAccessToken(user, jwtKey, issuer);
            string newRefreshToken = null!;

            try
            {
                newRefreshToken = await GetRefreshTokenAsync(user, newAccessToken, existingToken.DeviceId);

                lock (VolatileDataStore.RefreshTokenConnectionsLock)
                {
                    refreshTokenConnection.RefreshToken = newRefreshToken;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw CxExceptionOnUnauthorized("Concurrency Error");
            }

            AppendAuthTokenCookie(response, newRefreshToken);

            return newAccessToken;
        }

        public void AddNewConnection(string refreshToken, int userId, string connectionId, Guid deviceId)
        {
            RefreshTokenConnections? refreshTokenConnection = VolatileDataStore.RefreshTokenConnections.FirstOrDefault(c => c.RefreshToken == refreshToken);
            if (refreshTokenConnection == null)
            {
                refreshTokenConnection = new RefreshTokenConnections()
                {
                    RefreshToken = refreshToken,
                };
                VolatileDataStore.RefreshTokenConnections.Add(refreshTokenConnection);
            }
            HubConnectionInfo connectionInfo = new HubConnectionInfo()
            {
                ConnectionId = connectionId,
                UserId = userId,
                DeviceId = deviceId,
                RefreshTokenConnection = refreshTokenConnection

            };
            refreshTokenConnection.Connections.Add(connectionInfo);
        }

        public void RemoveConnection(string refreshToken)
        {
            RefreshTokenConnections? refreshTokenConnection = VolatileDataStore.RefreshTokenConnections.FirstOrDefault(c => c.RefreshToken == refreshToken);
            if (refreshTokenConnection == null) return;

            VolatileDataStore.RefreshTokenConnections.Remove(refreshTokenConnection);
        }
    }
}
