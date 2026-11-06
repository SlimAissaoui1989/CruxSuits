using CruxSuits.Services.Files.Infrastructure.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Interceptors;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Files.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.Files.Profiles;
using CruxSuits.Services.Files.Services;
using CruxSuits.Services.Shared.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args)
    .AddCruxSuitsServiceDefaults(serviceScope: "files.api");

builder.Services.AddScoped<TransactionLogInterceptor>();
builder.Services.AddScoped<PendingFileStorageOperations>();
builder.Services.AddScoped<FileStorageTransactionInterceptor>();

builder.Services.AddDbContext<FilesContext>((services, options) =>
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddInterceptors(
            services.GetRequiredService<TransactionLogInterceptor>(),
            services.GetRequiredService<FileStorageTransactionInterceptor>()));

builder.Services.AddScoped<IUploadedFileRepository, UploadedFileRepository>();
builder.Services.AddScoped<IUploadedFileTraceRepository, UploadedFileTraceRepository>();
builder.Services.AddScoped<IFilesUnitOfWork, FilesUnitOfWork>();
builder.Services.AddScoped<IUploadedFileService, UploadedFileService>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<UploadedFileProfile>());

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
