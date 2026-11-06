var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    Service = "CruxSuits Gateway",
    Status = "Running",
    Routes = new Dictionary<string, string>
    {
        ["Identity"] = "/identity/{service-path}",
        ["Person"] = "/person/{service-path}",
        ["Files"] = "/files/{service-path}",
        ["ReferenceData"] = "/reference-data/{service-path}",
    },
}));

app.MapGet("/health", () => Results.Ok(new { Status = "Healthy" }));
app.MapReverseProxy();

app.Run();
