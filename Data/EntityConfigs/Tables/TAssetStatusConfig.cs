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
    internal class TAssetStatusConfig : IEntityTypeConfiguration<TAssetStatus>
    {
        public virtual void Configure(EntityTypeBuilder<TAssetStatus> builder)
        {
            builder.HasKey(e => e.AssetStatusId)
                .HasName("PK_AssetStatus");

            builder.ToTable("AssetStatus");

            builder.Property(e => e.AssetStatusId).HasColumnName("AssetStatusID");
            builder.Property(e => e.AssetStatusId).ValueGeneratedOnAdd();
            builder.Property(e => e.AssetStatus1)
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnName("AssetStatus");

            builder.Property(e => e.AssetStatusDescription).HasMaxLength(500);

            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);
        }
        
    }
}
