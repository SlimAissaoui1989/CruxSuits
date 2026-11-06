using CruxIT.Library.API;
using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.Shared.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args)
    .AddCruxSuitsServiceDefaults(serviceScope: "reference-data.api");

builder.Services.AddDbContext<ReferenceDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ILocalizedNameRepository, LocalizedNameRepository>();
builder.Services.AddScoped<IReferenceDataUnitOfWork, ReferenceDataUnitOfWork>();

builder.Services.AddOpenApi();

var app = builder.Build();

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
