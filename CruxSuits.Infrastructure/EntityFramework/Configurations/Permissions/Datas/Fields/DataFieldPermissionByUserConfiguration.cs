using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Permissions.Datas.Fields
{
    internal class DataFieldPermissionByUserConfiguration : IEntityTypeConfiguration<DataFieldPermissionByUser>
    {
        public void Configure(EntityTypeBuilder<DataFieldPermissionByUser> builder)
        {
            builder.HasIndex(i => new { i.DataFieldGeneralPermissionId, i.UserId }).IsUnique();
        }
    }
}
