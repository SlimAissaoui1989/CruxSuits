using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(i => i.Name).IsUnique();
            builder.HasData(new Role
            {
                CreatedDate = CruxSuits.Services.Shared.SharedValues.DefaultCreatedDate,
                CreatedUserId = CruxSuits.Services.Shared.SharedValues.SuperAdminUserId,
                Id = 1,
                Level = 0,
                Name = "Super Admin",
            });
        }
    }
}
