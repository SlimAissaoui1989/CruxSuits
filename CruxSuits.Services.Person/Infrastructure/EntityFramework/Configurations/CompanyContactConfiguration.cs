using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class CompanyContactConfiguration : IEntityTypeConfiguration<CompanyContact>
    {
        public void Configure(EntityTypeBuilder<CompanyContact> builder)
        {
            builder.HasIndex(x => new { x.PersonId, x.CompanyId });
        }
    }
}
