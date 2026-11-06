using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasIndex(i => i.RefreshTokenValue).IsUnique();
        }
    }
}
