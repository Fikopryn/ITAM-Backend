using Core.Models.Entities.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Tables
{
    internal class TExPersonConfig : IEntityTypeConfiguration<TExPerson>
    {
        public virtual void Configure(EntityTypeBuilder<TExPerson> builder)
        {
            builder.ToTable("ExPerson");
            builder.HasKey(e => e.Id);
        }
    }
}
