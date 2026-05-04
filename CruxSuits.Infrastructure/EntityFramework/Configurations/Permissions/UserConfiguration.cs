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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(i=>i.UserName).IsUnique();
            builder.HasData(new User
            {
                Id = 1,
                UserName = "slim.aissaoui",
                //Password = "a491daf75263600280ccf6e3a56a576c93784039a45166f86905442334ef2888", //P@$$w0rdAdm1n
                Password = "AQAAAAIAAYagAAAAEHOpgt2zWhrtYNe+9jrFC0EBR2MwSIKvIeWexi9HBJeEH8jmCJIa5nP8QpJC49kK7g==", //P@$$w0rdAdm1n
                RoleId = 1,
                IsEnabled = true,
            });
        }
    }
}
