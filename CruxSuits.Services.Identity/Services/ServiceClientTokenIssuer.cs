using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CruxSuits.Services.Identity.Services;

public sealed class ServiceClientTokenIssuer(IConfiguration configuration, IdentitySigningKey signingKey)
{
    public string? IssueToken(string clientId, string clientSecret, IEnumerable<string> requestedScopes)
    {
        var clients = configuration.GetSection("ServiceClients").GetChildren();
        foreach (var client in clients)
        {
            var configuredId = client["ClientId"];
            var configuredSecret = client["ClientSecret"];
            if (string.IsNullOrWhiteSpace(configuredId) || string.IsNullOrEmpty(configuredSecret))
                continue;

            if (!FixedTimeEquals(configuredId, clientId) || !FixedTimeEquals(configuredSecret, clientSecret))
                continue;

            var allowedScopes = client.GetSection("Scopes").Get<string[]>() ?? [];
            var grantedScopes = requestedScopes
                .Where(scope => allowedScopes.Contains(scope, StringComparer.Ordinal))
                .Distinct(StringComparer.Ordinal)
                .ToArray();

            if (grantedScopes.Length == 0)
                return null;

            var claims = new List<Claim>
            {
                new("sub", configuredId),
                new("client_id", configuredId),
                new("token_use", "service"),
            };
            claims.AddRange(grantedScopes.Select(scope => new Claim("scope", scope)));

            var token = new JwtSecurityToken(
                issuer: signingKey.Issuer,
                audience: signingKey.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(signingKey.SigningKey, SecurityAlgorithms.RsaSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        return null;
    }

    private static bool FixedTimeEquals(string left, string right)
    {
        var leftBytes = System.Text.Encoding.UTF8.GetBytes(left);
        var rightBytes = System.Text.Encoding.UTF8.GetBytes(right);
        return leftBytes.Length == rightBytes.Length &&
               System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(leftBytes, rightBytes);
    }
}
