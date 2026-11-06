using CruxSuits.Services.Person.Infrastructure.EntityFramework;
using CruxSuits.Services.Person.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.Shared.ServiceDefaults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args)
    .AddCruxSuitsServiceDefaults(serviceScope: "person.api");

builder.Services.AddDbContext<PersonContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyContactRepository, CompanyContactRepository>();
builder.Services.AddScoped<ICompanyLegalIdentityRepository, CompanyLegalIdentityRepository>();
builder.Services.AddScoped<ICompanyLegalIdentityTypeRepository, CompanyLegalIdentityTypeRepository>();
builder.Services.AddScoped<IEMailRepository, EMailRepository>();
builder.Services.AddScoped<IGenderRepository, GenderRepository>();
builder.Services.AddScoped<IIBANRepository, IBANRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonIdentityRepository, PersonIdentityRepository>();
builder.Services.AddScoped<IPersonIdentityTypeRepository, PersonIdentityTypeRepository>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
builder.Services.AddScoped<IPhoneTypeRepository, PhoneTypeRepository>();
builder.Services.AddScoped<IPersonUnitOfWork, PersonUnitOfWork>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
