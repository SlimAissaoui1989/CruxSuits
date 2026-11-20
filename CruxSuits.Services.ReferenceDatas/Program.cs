using CruxIT.Library.API;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Profiles;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Services.Shared.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args)
    .AddCruxSuitsServiceDefaults(serviceScope: "reference-data.api");

builder.Services.AddDbContext<ReferenceDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryNameRepository, CountryNameRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ILocalizedNameRepository, LocalizedNameRepository>();
builder.Services.AddScoped<IReferenceDataUnitOfWork, ReferenceDataUnitOfWork>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICountryNameService, CountryNameService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<CountryProfile>();
    cfg.AddProfile<CountryNameProfile>();
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
