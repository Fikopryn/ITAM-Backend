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
    internal class TRequestPurchaseOrderConfig : IEntityTypeConfiguration<TRequestPurchaseOrder>
    {
        public virtual void Configure(EntityTypeBuilder<TRequestPurchaseOrder> builder)
        {
            builder.HasKey(e => e.Rpono)
                .HasName("PK_RequestPurchaseOrder");

            builder.ToTable("RequestPurchaseOrder");

            builder.Property(e => e.Rpono)
                .HasMaxLength(50)
                .HasColumnName("RPONo");

            builder.Property(e => e.AdditionalInfo).HasColumnType("ntext");

            builder.Property(e => e.ContractNo).HasMaxLength(50);

            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy).HasMaxLength(50);

            builder.Property(e => e.Roend)
                .HasColumnType("datetime")
                .HasColumnName("ROEnd");

            builder.Property(e => e.Rostart)
                .HasColumnType("datetime")
                .HasColumnName("ROStart");

            builder.Property(e => e.Rposubject)
                .HasMaxLength(50)
                .HasColumnName("RPOSubject");

            builder.Property(e => e.TotalRpo)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("TotalRPO");

            /*builder.HasOne(d => d.ContractNoNavigation)
                .WithMany(p => p.RequestPurchaseOrders)
                .HasForeignKey(d => d.ContractNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestPurchaseOrder_Contract");*/
        }
    }
}
