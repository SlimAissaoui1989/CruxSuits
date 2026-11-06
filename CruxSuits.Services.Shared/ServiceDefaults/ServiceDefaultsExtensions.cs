using CruxIT.Library.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CruxSuits.Services.Shared.ServiceDefaults;

public static class ServiceDefaultsExtensions
{
    public static WebApplicationBuilder AddCruxSuitsServiceDefaults(
        this WebApplicationBuilder builder,
        string jwtAuthorityConfigurationKey = "Jwt:Authority",
        string? serviceScope = null)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options => ConfigureJson(options.JsonSerializerOptions));

        builder.Services.ConfigureHttpJsonOptions(options =>
            ConfigureJson(options.SerializerOptions));

        builder.AddCorpsDefaultPolicyAllowAny();
        builder.IgnoreCyclesJsonAndSavePropertyNamingPolicy();
        builder.UseGC();

        var authority = builder.Configuration[jwtAuthorityConfigurationKey]
            ?? throw new InvalidOperationException(
                $"Configuration value '{jwtAuthorityConfigurationKey}' is required.");
        var audience = builder.Configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Configuration value 'Jwt:Audience' is required.");

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
            });

        builder.Services.AddAuthorization(options =>
        {
            if (!string.IsNullOrWhiteSpace(serviceScope))
            {
                options.AddPolicy("ServiceToService", policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim("token_use", "service")
                    .RequireClaim("scope", serviceScope));
            }
        });

        return builder;
    }

    private static void ConfigureJson(JsonSerializerOptions options)
    {
        options.PropertyNamingPolicy = null;
        options.PropertyNameCaseInsensitive = false;
        options.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
    }
}
