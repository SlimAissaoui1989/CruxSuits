using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Files.Infrastructure.EntityFramework.Configurations
{
    internal class UploadedFileConfiguration : IEntityTypeConfiguration<UploadedFile>
    {
        public void Configure(EntityTypeBuilder<UploadedFile> builder)
        {
            builder.Property(file => file.Content).IsRequired(false);
        }
    }
}
