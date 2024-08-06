using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TExMasterLovConfig : IEntityTypeConfiguration<TExMasterLov>
    {
        public virtual void Configure(EntityTypeBuilder<TExMasterLov> builder)
        {
            builder.HasKey(e => e.LovId);

            builder.ToTable("ExMasterLov");

            builder.Property(e => e.LovId)
                .HasColumnType("numeric(18, 0)")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.LovAttr1)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.LovAttr2)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.LovAttr3)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.LovCategory)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.LovDescriptions)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.LovKey)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LovName)
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(e => e.LovParentKey)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.LovSequence).HasColumnType("numeric(20, 0)");

            builder.Property(e => e.LovValue)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
