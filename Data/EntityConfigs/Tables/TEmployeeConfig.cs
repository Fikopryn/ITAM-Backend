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
    internal class TEmployeeConfig : IEntityTypeConfiguration<TEmployee>
    {
        public virtual void Configure(EntityTypeBuilder<TEmployee> builder)
        {
            builder.HasKey(e => e.UserId)
                .HasName("PK_Employee");

            builder.ToTable("Employee");

            builder.Property(e => e.UserId).HasMaxLength(100);

            builder.Property(e => e.AreaId)
                .HasMaxLength(10)
                .HasColumnName("AreaID")
                .IsFixedLength();

            builder.Property(e => e.AreaName).HasMaxLength(50);

            builder.Property(e => e.AssignmentType)
                .HasMaxLength(10)
                .IsFixedLength();

            builder.Property(e => e.BackToBack).HasMaxLength(500);

            builder.Property(e => e.Ccid)
                .HasMaxLength(50)
                .HasColumnName("CCID");

            builder.Property(e => e.Ccname)
                .HasMaxLength(50)
                .HasColumnName("CCName");

            builder.Property(e => e.Company).HasMaxLength(100);

            builder.Property(e => e.Email).HasMaxLength(50);

            builder.Property(e => e.EmpNum).HasMaxLength(20);

            builder.Property(e => e.Fungsi).HasMaxLength(50);

            builder.Property(e => e.Idnum)
                .HasMaxLength(50)
                .HasColumnName("IDNum");

            builder.Property(e => e.Idtype)
                .HasMaxLength(10)
                .HasColumnName("IDType")
                .IsFixedLength();

            builder.Property(e => e.Kbo)
                .HasMaxLength(10)
                .HasColumnName("KBO")
                .IsFixedLength();

            builder.Property(e => e.Kboname)
                .HasMaxLength(50)
                .HasColumnName("KBOName");

            builder.Property(e => e.LocCategory).HasMaxLength(50);

            builder.Property(e => e.LocGroup).HasMaxLength(50);

            builder.Property(e => e.ParentPosId)
                .HasMaxLength(50)
                .HasColumnName("ParentPosID");

            builder.Property(e => e.ParentPosName).HasMaxLength(50);

            builder.Property(e => e.ParentUserId)
                .HasMaxLength(50)
                .HasColumnName("ParentUserID");

            builder.Property(e => e.PersNo).HasMaxLength(50);

            builder.Property(e => e.PosId)
                .HasMaxLength(50)
                .HasColumnName("PosID");

            builder.Property(e => e.PosName).HasMaxLength(50);

            builder.Property(e => e.Section).HasMaxLength(50);

            builder.Property(e => e.SectionId)
                .HasMaxLength(10)
                .HasColumnName("SectionID")
                .IsFixedLength();

            builder.Property(e => e.Sex)
                .HasMaxLength(10)
                .IsFixedLength();

            builder.Property(e => e.SubAreaId)
                .HasMaxLength(10)
                .HasColumnName("SubAreaID")
                .IsFixedLength();

            builder.Property(e => e.SubAreaName).HasMaxLength(50);

            builder.Property(e => e.SubFunction).HasMaxLength(50);

            builder.Property(e => e.UnitId)
                .HasMaxLength(10)
                .HasColumnName("UnitID")
                .IsFixedLength();

            builder.Property(e => e.UnitName).HasMaxLength(50);

            builder.Property(e => e.UserName).HasMaxLength(50);
        }
    }
}
