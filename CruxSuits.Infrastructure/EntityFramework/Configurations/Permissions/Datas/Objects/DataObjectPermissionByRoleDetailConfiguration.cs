using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Permissions.Datas.Objects
{
    internal class DataObjectPermissionByRoleDetailConfiguration : IEntityTypeConfiguration<DataObjectPermissionByRoleDetail>
    {
        public void Configure(EntityTypeBuilder<DataObjectPermissionByRoleDetail> builder)
        {
            builder.HasIndex(i => new { i.DataObjectPermissionByRoleId, i.RecordId }).IsUnique();
        }
    }
}
