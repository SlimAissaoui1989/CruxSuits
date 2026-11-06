using CruxSuits.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.Identity.Endpoints;

public static class ServiceClientTokenEndpoint
{
    public static IEndpointRouteBuilder MapServiceClientTokenEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/v1/connect/token", IssueTokenAsync)
            .AllowAnonymous();

        return endpoints;
    }

    private static async Task<IResult> IssueTokenAsync(
        HttpRequest request,
        [FromServices] ServiceClientTokenIssuer tokenIssuer,
        CancellationToken cancellationToken)
    {
        if (!request.HasFormContentType)
        {
            return Results.BadRequest(new
            {
                error = "invalid_request",
                error_description = "Use application/x-www-form-urlencoded."
            });
        }

        var form = await request.ReadFormAsync(cancellationToken);
        if (!string.Equals(form["grant_type"], "client_credentials", StringComparison.Ordinal))
            return Results.BadRequest(new { error = "unsupported_grant_type" });

        var clientId = form["client_id"].ToString();
        var clientSecret = form["client_secret"].ToString();
        var authorization = request.Headers.Authorization.ToString();

        if (authorization.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                var encodedCredentials = authorization[6..].Trim();
                var decodedCredentials = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(encodedCredentials));
                var separatorIndex = decodedCredentials.IndexOf(':');

                if (separatorIndex <= 0)
                    return Results.Unauthorized();

                clientId = Uri.UnescapeDataString(
                    decodedCredentials[..separatorIndex].Replace("+", "%20"));
                clientSecret = Uri.UnescapeDataString(
                    decodedCredentials[(separatorIndex + 1)..].Replace("+", "%20"));
            }
            catch (FormatException)
            {
                return Results.Unauthorized();
            }
        }

        if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrEmpty(clientSecret))
            return Results.Unauthorized();

        var requestedScopes = form["scope"].ToString()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var accessToken = tokenIssuer.IssueToken(clientId, clientSecret, requestedScopes);

        if (accessToken is null)
            return Results.Unauthorized();

        return Results.Ok(new
        {
            access_token = accessToken,
            token_type = "Bearer",
            expires_in = 300
        });
    }
}
