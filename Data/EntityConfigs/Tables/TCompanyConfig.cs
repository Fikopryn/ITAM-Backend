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
    internal class TCompanyConfig : IEntityTypeConfiguration<TCompany>
    {
        public virtual void Configure(EntityTypeBuilder<TCompany> builder)
        {
            builder.HasKey(e => e.CompanyId)
                .HasName("PK_Company");

            builder.ToTable("Company");

            builder.Property(e => e.CompanyId).HasColumnName("CompanyID");

            builder.Property(e => e.Abbreviation)
                .HasMaxLength(10)
                .IsFixedLength();

            builder.Property(e => e.CompanyName).HasMaxLength(200);

            builder.Property(e => e.CompanyType)
                .HasMaxLength(200)
                .IsFixedLength();

            builder.Property(e => e.LastModifiedDate).HasColumnType("datetime");

            builder.Property(e => e.ModifiedBy).HasMaxLength(100);

            builder.Property(e => e.Website).HasMaxLength(200);
        }

    }
}
