using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TExAuditTrailActivityConfig : IEntityTypeConfiguration<TExAuditTrailActivity>
    {
        public virtual void Configure(EntityTypeBuilder<TExAuditTrailActivity> builder)
        {
            builder.HasKey(e => e.ActivityNumber)
                    .HasName("PK_AuditTrailActivity");

            builder.ToTable("ExAuditTrailActivity");

            builder.Property(e => e.ActivityNumber)
                .HasColumnType("numeric(18, 0)")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ModulName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.Timestamp).HasColumnType("datetime");

            builder.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.UserName)
                .HasMaxLength(250)
                .IsUnicode(false);
        }
    }
}
