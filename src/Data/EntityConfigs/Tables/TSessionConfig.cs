using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TSessionConfig : IEntityTypeConfiguration<TSession>
    {
        public virtual void Configure(EntityTypeBuilder<TSession> builder)
        {
            builder.HasKey(e => e.IpAddress)
                    .HasName("Session_PK");

            builder.ToTable("Session");

            builder.Property(e => e.ExpiredDate).HasColumnType("date");

            builder.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.RefreshToken)
                .HasMaxLength(1200)
                .IsUnicode(false);
        }
    }
}
