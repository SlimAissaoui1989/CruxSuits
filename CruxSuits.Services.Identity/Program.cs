using CruxIT.Library.API;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Identity.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.Identity.Endpoints;
using CruxSuits.Services.Identity.Services;
using CruxSuits.Services.Shared.ServiceDefaults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args)
    .AddCruxSuitsServiceDefaults(jwtAuthorityConfigurationKey: "Jwt:Issuer");

builder.Services.AddSingleton<IdentitySigningKey>();
builder.Services.AddSingleton<ServiceClientTokenIssuer>();

//My Services Injections
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<IdentityContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserLoginIdentifierRepository, UserLoginIdentifierRepository>();

builder.Services.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();

builder.Services.AddAutoMapper(cfg =>
{
    //cfg.AddProfile<UserProfile>();
});

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapServiceClientTokenEndpoint();

app.MapGet("/.well-known/openid-configuration", (IdentitySigningKey signingKey) =>
    Results.Json(signingKey.CreateDiscoveryDocument()));
app.MapGet("/.well-known/jwks.json", (IdentitySigningKey signingKey) =>
    Results.Json(signingKey.CreateJsonWebKeySet()));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.LogOnRequest();

app.LogOnExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
