using Core.Models.Entities.Tables;
using Core.Models.Entities.Views;
using Data.EntityConfigs.Tables;
using Data.EntityConfigs.Views;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<TExPerson> ExPersons { get; set; }
        public DbSet<TExPersonContact> ExPersonContacts { get; set; }
        public DbSet<TExPersonIdentification> ExPersonIdentifications { get; set; }
        public DbSet<VwExPerson> VwExPersons { get; set; }
        public DbSet<TExFileReference> ExFileReference { get; set; }
        public DbSet<TExAuditTrailActivity> ExAuditTrailActivity { get; set; }
        public DbSet<TExMasterLov> ExMasterLov { get; set; }
        public DbSet<TSession> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new TExPersonConfig());
            builder.ApplyConfiguration(new TExPersonContactConfig());
            builder.ApplyConfiguration(new TExPersonIdentificationConfig());
            builder.ApplyConfiguration(new VwExPersonConfig());
            builder.ApplyConfiguration(new TExFileReferenceConfig());
            builder.ApplyConfiguration(new TExAuditTrailActivityConfig());
            builder.ApplyConfiguration(new TExMasterLovConfig());
            builder.ApplyConfiguration(new TSessionConfig());
        }
    }
}
