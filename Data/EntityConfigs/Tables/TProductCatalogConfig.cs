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
    internal class TProductCatalogConfig : IEntityTypeConfiguration<TProductCatalog>
    {
        public virtual void Configure(EntityTypeBuilder<TProductCatalog> builder)
        {
            builder.HasKey(e => e.ProductCatalogId)
                .HasName("PK_ProductCatalog");

            builder.ToTable("ProductCatalog");

            builder.Property(e => e.ProductCatalogId).HasColumnName("ProductCatalogID");

            builder.Property(e => e.AdditionalInfo).HasColumnType("ntext");

            builder.Property(e => e.CompanyId).HasColumnName("CompanyID");

            builder.Property(e => e.HyperlinkDataSheet).HasMaxLength(200);

            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.Property(e => e.Manufacturer).HasMaxLength(200);

            builder.Property(e => e.ModelVersion).HasMaxLength(200);

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);

            builder.Property(e => e.ProdCatTier1).HasMaxLength(200);

            builder.Property(e => e.ProdCatTier2).HasMaxLength(200);

            builder.Property(e => e.ProdCatTier3).HasMaxLength(200);

            builder.Property(e => e.ProductName).HasMaxLength(200);

            builder.Property(e => e.ProductNumber).HasMaxLength(200);

            //builder.HasOne(d => d.Company)
                //.WithMany(p => p.ProductCatalogs)
                //.HasForeignKey(d => d.CompanyId)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK_ProductCatalog_Company");
        }

    }
}
