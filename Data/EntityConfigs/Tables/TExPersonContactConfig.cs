using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TExPersonContactConfig : IEntityTypeConfiguration<TExPersonContact>
    {
        public virtual void Configure(EntityTypeBuilder<TExPersonContact> builder)
        {
            builder.ToTable("ExPersonContact");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.PersonId).IsRequired();

            builder.HasOne(d => d.Person).WithOne(p => p.PersonContact).HasForeignKey<TExPersonContact>(d => d.PersonId);
        }
    }
}
