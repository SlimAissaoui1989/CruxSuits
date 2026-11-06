using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Files.Infrastructure.EntityFramework.Configurations
{
    internal class UploadedFileTraceConfiguration : IEntityTypeConfiguration<UploadedFileTrace>
    {
        public void Configure(EntityTypeBuilder<UploadedFileTrace> builder)
        {
            builder.HasIndex(trace => trace.DigitalFileId);
            builder.HasIndex(trace => trace.UserId);
            builder.HasIndex(trace => trace.ActionDate);

            builder.Property(trace => trace.ActionType)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
