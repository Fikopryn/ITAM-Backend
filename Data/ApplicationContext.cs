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
        public DbSet<TAssetStatus> AssetStatus { get; set; }
        public DbSet<TAssetStatusFin> AssetStatusFin { get; set; }
        public DbSet<TCompany> Company { get; set; }
        public DbSet<TProductCatalog> ProductCatalog { get; set; }
        public DbSet<TEmployee> Employee { get; set; }
        public DbSet<TMasterRole> MasterRole { get; set; }
        public DbSet<TContract> Contract { get; set; }
        public DbSet<TRequestPurchaseOrder> RequestPurchaseOrder { get; set; }

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
            builder.ApplyConfiguration(new TAssetStatusConfig());
            builder.ApplyConfiguration(new TAssetStatusFinConfig());
            builder.ApplyConfiguration(new TCompanyConfig());
            builder.ApplyConfiguration(new TProductCatalogConfig());
            builder.ApplyConfiguration(new TMasterRoleConfig());
            builder.ApplyConfiguration(new TEmployeeConfig());
            builder.ApplyConfiguration(new TContractConfig());
            builder.ApplyConfiguration(new TRequestPurchaseOrderConfig());
        }
    }
}
