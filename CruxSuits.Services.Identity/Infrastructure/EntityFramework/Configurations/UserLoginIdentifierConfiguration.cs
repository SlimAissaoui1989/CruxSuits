using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework.Configurations
{
    internal class UserLoginIdentifierConfiguration : IEntityTypeConfiguration<UserLoginIdentifier>
    {
        public void Configure(EntityTypeBuilder<UserLoginIdentifier> builder)
        {
            builder.HasIndex(i => i.NormalizedValue).IsUnique();
            builder.HasData(new UserLoginIdentifier()
            {
                CreatedDate = CruxSuits.Services.Shared.SharedValues.DefaultCreatedDate,
                Id = 1,
                NormalizedValue = CruxSuits.Services.Shared.SharedValues.SuperAdminUserName,
                Type = CruxSuits.Services.Shared.LoginIdentifierType.Username.ToString(),
                UserId = CruxSuits.Services.Shared.SharedValues.SuperAdminUserId,
            });
        }
    }
}
