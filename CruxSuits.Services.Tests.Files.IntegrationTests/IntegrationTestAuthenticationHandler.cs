using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CruxSuits.Services.Files.Tests.IntegrationTests;

internal sealed class IntegrationTestAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "IntegrationTest";
    public const string AuthenticationModeHeader = "X-Integration-Test-Auth";

    public IntegrationTestAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authenticationMode = Request.Headers[AuthenticationModeHeader].ToString();
        if (string.Equals(authenticationMode, "anonymous", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var userId = Guid.Parse("8fb1bfe9-f8e8-45ef-99ec-1264762162dd");
        var claims = new List<Claim>
        {
            new Claim("user_id", userId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim("sub", userId.ToString()),
            new Claim("user_name", "integration-test-user"),
            new Claim(ClaimTypes.Name, "integration-test-user"),
            new Claim("name", "integration-test-user"),
            new Claim("preferred_username", "integration-test-user"),
            new Claim("role_id", "10"),
            new Claim("role_name", "integration-test-role")
        };

        if (!string.Equals(authenticationMode, "user", StringComparison.OrdinalIgnoreCase))
        {
            claims.Add(new Claim("token_use", "service"));
            claims.Add(new Claim("scope", "files.api"));
        }

        var identity = new ClaimsIdentity(claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
