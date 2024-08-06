using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TExPersonIdentificationConfig : IEntityTypeConfiguration<TExPersonIdentification>
    {
        public virtual void Configure(EntityTypeBuilder<TExPersonIdentification> builder)
        {
            builder.ToTable("ExPersonIdentification");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.PersonContactId).IsRequired();

            builder.HasOne(d => d.PersonContact).WithMany(p => p.PersonIds).HasForeignKey(d => d.PersonContactId);
        }
    }
}
