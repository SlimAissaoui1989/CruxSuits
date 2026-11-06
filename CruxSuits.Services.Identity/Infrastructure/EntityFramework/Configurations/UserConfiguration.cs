using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User
            {
                Id = CruxSuits.Services.Shared.SharedValues.SuperAdminUserId,
                //Password = "a491daf75263600280ccf6e3a56a576c93784039a45166f86905442334ef2888", //P@$$w0rdAdm1n
                Password = "AQAAAAIAAYagAAAAEHOpgt2zWhrtYNe+9jrFC0EBR2MwSIKvIeWexi9HBJeEH8jmCJIa5nP8QpJC49kK7g==", //P@$$w0rdAdm1n
                RoleId = 1,
                IsEnabled = true,
                UserName = CruxSuits.Services.Shared.SharedValues.SuperAdminUserName,
            });
        }
    }
}
