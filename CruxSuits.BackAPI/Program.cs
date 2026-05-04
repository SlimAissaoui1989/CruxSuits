using AutoMapper;
using CruxIT.Library.API;
using CruxSuits.BackAPI;
using CruxSuits.BackAPI.HubService;
using CruxSuits.BackAPI.Profiles;
using CruxSuits.BackAPI.Services.Permissions.UiResources;
using CruxSuits.BackAPI.Services.Permissions.Users;
using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR().AddJsonProtocol(options =>
{
    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
});

//My config
builder.AddCorpsDefaultPolicyAllowAny();
builder.AddJWTAuthentication(ConstsValues.AuthTokenCookieTag);
builder.AddSwaggerGenWithBearer();
builder.IgnoreCyclesJsonAndSavePropertyNamingPolicy();
builder.UseGC();

//my Services
builder.Services.AddScoped<ICruxSuitsUnitOfWork, CruxSuitsUnitOfWork>(imp =>
    new CruxSuitsUnitOfWork(new CruxSuitsContext(builder.Configuration.GetConnectionString("DefaultConnection")!))
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUiResourceService, UiResourceService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<UiResourcePermissionProfile>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(so =>
    {
        so.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}
else
{
    app.LogOnExceptionHandler();
}

app.MapHub<CxSuitsHub>("/suits-hub");

app.LogOnRequest();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
