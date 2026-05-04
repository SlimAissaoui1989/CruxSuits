using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Permissions
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(i => i.Name).IsUnique();
            builder.HasIndex(i => i.Level).IsUnique();
            builder.HasData(new Role
            {
                Id = 1,
                Name = "Supper Admin",
                Level = string.Empty,
            });
        }
    }
}
