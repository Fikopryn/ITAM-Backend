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
    internal class TContractConfig : IEntityTypeConfiguration <TContract>
    {
        public virtual void Configure(EntityTypeBuilder<TContract> builder)
        {
            builder.HasKey(e => e.ContractNo)
                .HasName("PK_Contract");

            builder.ToTable("Contract");

            builder.Property(e => e.ContractNo).HasMaxLength(50);

            builder.Property(e => e.CompanyId).HasColumnName("CompanyID");

            builder.Property(e => e.ContractOwner).HasMaxLength(50);

            builder.Property(e => e.ContractValue).HasColumnType("money");

            builder.Property(e => e.EndContract).HasColumnType("datetime");

            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);

            builder.Property(e => e.StartContract).HasColumnType("datetime");

            builder.Property(e => e.Subject).HasMaxLength(500);

            builder.Property(e => e.SupplierId).HasColumnName("SupplierID");

            /*builder.HasOne(d => d.Company)
                .WithMany(p => p.ContractCompanies)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contract_Company");*/

            /*builder.HasOne(d => d.Supplier)
                .WithMany(p => p.ContractSuppliers)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contract_Company1");*/
        }
    }
}
