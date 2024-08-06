using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.EntityConfigs.Tables
{
    internal class TMasterRoleConfig : IEntityTypeConfiguration<TMasterRole>
    {
        public virtual void Configure(EntityTypeBuilder<TMasterRole> builder)
        {

            builder.HasKey(e => e.RoleId)
                .HasName("PK_MasterRole");

            builder.ToTable("MasterRole");

            builder.Property(e => e.RoleId).HasColumnName("RoleID");

            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);

            builder.Property(e => e.RoleDescription).HasMaxLength(200);

            builder.Property(e => e.RoleName).HasMaxLength(100);
        }

    }
}
