using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TExFileReferenceConfig : IEntityTypeConfiguration<TExFileReference>
    {
        public virtual void Configure(EntityTypeBuilder<TExFileReference> builder)
        {
            builder.HasKey(e => e.FileNumber)
                    .HasName("PK_FileReference");

            builder.ToTable("FileReference");

            builder.Property(e => e.FileNumber)
                .HasDefaultValueSql("(newid())");

            builder.Property(e => e.FileCategory)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.FileNameExtention)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.ModulId).HasDefaultValueSql("(newid())");

            builder.Property(e => e.ModulName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Timestamp).HasColumnType("datetime");
        }
    }
}
