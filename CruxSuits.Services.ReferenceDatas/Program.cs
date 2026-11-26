using CruxIT.Library.API;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Localization;
using CruxSuits.Services.ReferenceDatas.Profiles;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Services.Shared.Localization;
using CruxSuits.Services.Shared.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args)
    .AddCruxSuitsServiceDefaults(serviceScope: "reference-data.api")
    .AddCruxSuitsLanguageCatalogCache();

builder.Services.AddDbContext<ReferenceDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<IAdministrativeAreaRepository, AdministrativeAreaRepository>();
builder.Services.AddScoped<IAdministrativeAreaNameRepository, AdministrativeAreaNameRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryNameRepository, CountryNameRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ICurrencyNameRepository, CurrencyNameRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ILocalityRepository, LocalityRepository>();
builder.Services.AddScoped<ILocalityNameRepository, LocalityNameRepository>();
builder.Services.AddScoped<IReferenceDataUnitOfWork, ReferenceDataUnitOfWork>();

//Services
builder.Services.AddScoped<IAdministrativeAreaService, AdministrativeAreaService>();
builder.Services.AddScoped<IAdministrativeAreaNameService, AdministrativeAreaNameService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICountryNameService, CountryNameService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ICurrencyNameService, CurrencyNameService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ILocalityService, LocalityService>();
builder.Services.AddScoped<ILocalityNameService, LocalityNameService>();

//Localizations
builder.Services.AddScoped<ILanguageCatalogProvider, LanguageCatalogProvider>();
builder.Services.AddHostedService<LanguageCatalogRefreshWorker>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AdministrativeAreaProfile>();
    cfg.AddProfile<AdministrativeAreaNameProfile>();
    cfg.AddProfile<CountryProfile>();
    cfg.AddProfile<CountryNameProfile>();
    cfg.AddProfile<CurrencyProfile>();
    cfg.AddProfile<CurrencyNameProfile>();
    cfg.AddProfile<LanguageProfile>();
    cfg.AddProfile<LocalityProfile>();
    cfg.AddProfile<LocalityNameProfile>();
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCruxSuitsServiceDefaults();

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
