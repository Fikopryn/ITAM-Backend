using Core.Models.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfigs.Views
{
    internal class VwExPersonConfig : IEntityTypeConfiguration<VwExPerson>
    {
        public virtual void Configure(EntityTypeBuilder<VwExPerson> builder)
        {
            builder.ToView("VwExPerson");
            builder.HasKey(e => e.Id);
        }
    }
}
