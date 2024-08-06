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
    internal class TAssetStatusFinConfig : IEntityTypeConfiguration<TAssetStatusFin>
    {
        public virtual void Configure(EntityTypeBuilder<TAssetStatusFin> builder)
        {
            builder.HasKey(e => e.AssetStatusFinId);
            builder.ToTable("AssetStatusFin");

            builder.Property(e => e.AssetStatusFinId).HasColumnName("AssetStatusFinID");

            builder.Property(e => e.AssetStatusFin1)
                .HasMaxLength(50)
                .HasColumnName("AssetStatusFin");

            builder.Property(e => e.AssetStatusFinDesc).HasMaxLength(200);

            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);
        }

    }
}
