using CruxIT.Library;
using CruxIT.Library.API;
using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxIT.Library.Exceptions;
using CruxIT.Library.GeoLocations;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Identity.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Shared.DTOs.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using UAParser;

namespace CruxSuits.Services.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IIdentityUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthService> _logger;
        private readonly IdentitySigningKey _identitySigningKey;

        public AuthService(IIdentityUnitOfWork unitOfWork,
            IPasswordHasher<User> passwordHasher,
            ILogger<AuthService> logger,
            IdentitySigningKey identitySigningKey)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _identitySigningKey = identitySigningKey;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request, IPAddress? ip, Guid deviceId)
        {
            if (CxStringHelper.IsPhoneNumber(request.Identifier))
            {
                string phone = request.Identifier;
                if (ip != null)
                {
                    CxIPLocation? iPLocation = await CxIPLocation.GetGeoLocationByIp(ip);
                    if (iPLocation != null)
                    {
                        phone = CxPhoneHelper.GetPhoneNumberInterNationalFormat(phone, iPLocation.CountryCode);
                    }
                }
                request.Identifier = phone;
            }

            UserLoginIdentifier? identifier = await _unitOfWork.UserLoginIdentifier
                .Include(i => i.User!.Role)
                .GetFirstOrDefaultAsync(c => c.NormalizedValue == request.Identifier);

            User? user = identifier?.User;

            if (user?.IsEnabled ?? false)
            {
                var verification = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

                if (verification == PasswordVerificationResult.Failed)
                    user = null;
                else if (verification == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    user.Password = _passwordHasher.HashPassword(user, request.Password);
                    await _unitOfWork.User.UpdateAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            if (!(user?.IsEnabled ?? false))
                throw new CxException(CxExceptionTypes.Unauthorized);

            string accessToken = GetAccessToken(user);
            string refreshToken = await GetRefreshTokenAsync(user, accessToken, deviceId);

            return new LoginResponse { AccessToken = accessToken };
        }

        public string GetAccessToken(User user)
        {
            var now = DateTime.UtcNow;
            var claims = new[]
            {
                new Claim(CxLibraryApiConstValues.UserIdTag, user.Id.ToString()),
                new Claim(CxLibraryApiConstValues.UserNameTag, user.UserName.ToString()),
                new Claim(CxLibraryApiConstValues.RoleIdTag, user.RoleId.ToString()),
                new Claim(CxLibraryApiConstValues.RoleNameTag, user.Role!.Name),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _identitySigningKey.Issuer,
                audience: _identitySigningKey.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(15),
                signingCredentials: new SigningCredentials(
                    _identitySigningKey.SigningKey,
                    SecurityAlgorithms.RsaSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                ExpirationDate = DateTime.UtcNow.AddDays(7),
                UserName = user.UserName,
                IsRevoked = false
            };

            await _unitOfWork.RefreshToken.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return token;
        }

    }
}
