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
    internal class DataObjectPermissionByUserDetailConfiguration : IEntityTypeConfiguration<DataObjectPermissionByUserDetail>
    {
        public void Configure(EntityTypeBuilder<DataObjectPermissionByUserDetail> builder)
        {
            builder.HasIndex(i => new { i.DataObjectPermissionByUserId, i.RecordId }).IsUnique();
        }
    }
}
