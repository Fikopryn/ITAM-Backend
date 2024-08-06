using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.test
{
    public partial class ICTAssetMgmtContext : DbContext
    {
        public ICTAssetMgmtContext()
        {
        }

        public ICTAssetMgmtContext(DbContextOptions<ICTAssetMgmtContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssetGeneralInfo> AssetGeneralInfos { get; set; }
        public virtual DbSet<AssetStatus> AssetStatuses { get; set; }
        public virtual DbSet<AssetStatusFin> AssetStatusFins { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public virtual DbSet<Dodetail> Dodetails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<MasterRole> MasterRoles { get; set; }
        public virtual DbSet<ProductCatalog> ProductCatalogs { get; set; }
        public virtual DbSet<RequestPurchaseOrder> RequestPurchaseOrders { get; set; }
        public virtual DbSet<RoleMapping> RoleMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=phmsqldev01.pertamina.com\\DEVSQL01;Database=ICTAssetMgmt;User Id=ictassetusr;Password=!ctAsset;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetGeneralInfo>(entity =>
            {
                entity.HasKey(e => e.SerialNumber);

                entity.ToTable("AssetGeneralInfo");

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.AdditionalInfo).HasColumnType("ntext");

                entity.Property(e => e.AreaId)
                    .HasMaxLength(10)
                    .HasColumnName("AreaID")
                    .IsFixedLength();

                entity.Property(e => e.AssetBuilding)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.AssetFloorRoom).HasMaxLength(100);

                entity.Property(e => e.AssetFunction).HasMaxLength(100);

                entity.Property(e => e.AssetName).HasMaxLength(100);

                entity.Property(e => e.AssetNo).HasMaxLength(50);

                entity.Property(e => e.AssetStatusFinId).HasColumnName("AssetStatusFinID");

                entity.Property(e => e.AssetStatusId).HasColumnName("AssetStatusID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ContractNo).HasMaxLength(50);

                entity.Property(e => e.Dono)
                    .HasMaxLength(50)
                    .HasColumnName("DONo");

                entity.Property(e => e.InstallationDate).HasColumnType("datetime");

                entity.Property(e => e.LastInventoryDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.OwnedBy).HasMaxLength(50);

                entity.Property(e => e.ProductCatalogId).HasColumnName("ProductCatalogID");

                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");

                entity.Property(e => e.SubAreaId)
                    .HasMaxLength(50)
                    .HasColumnName("SubAreaID");

                entity.Property(e => e.UsedBy).HasMaxLength(100);

                entity.HasOne(d => d.AssetStatusFin)
                    .WithMany(p => p.AssetGeneralInfos)
                    .HasForeignKey(d => d.AssetStatusFinId)
                    .HasConstraintName("FK_AssetGeneralInfo_AssetStatusFin");

                entity.HasOne(d => d.AssetStatus)
                    .WithMany(p => p.AssetGeneralInfos)
                    .HasForeignKey(d => d.AssetStatusId)
                    .HasConstraintName("FK_AssetGeneralInfo_AssetStatus");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.AssetGeneralInfos)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_AssetGeneralInfo_Company");

                entity.HasOne(d => d.ContractNoNavigation)
                    .WithMany(p => p.AssetGeneralInfos)
                    .HasForeignKey(d => d.ContractNo)
                    .HasConstraintName("FK_AssetGeneralInfo_AssetGeneralInfo");

                entity.HasOne(d => d.ProductCatalog)
                    .WithMany(p => p.AssetGeneralInfos)
                    .HasForeignKey(d => d.ProductCatalogId)
                    .HasConstraintName("FK_AssetGeneralInfo_ProductCatalog");

                entity.HasOne(d => d.UsedByNavigation)
                    .WithMany(p => p.AssetGeneralInfos)
                    .HasForeignKey(d => d.UsedBy)
                    .HasConstraintName("FK_Employee_AssetGeneralInfo");
            });

            modelBuilder.Entity<AssetStatus>(entity =>
            {
                entity.ToTable("AssetStatus");

                entity.Property(e => e.AssetStatusId).HasColumnName("AssetStatusID");

                entity.Property(e => e.AssetStatus1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("AssetStatus");

                entity.Property(e => e.AssetStatusDescription).HasMaxLength(500);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AssetStatusFin>(entity =>
            {
                entity.ToTable("AssetStatusFin");

                entity.Property(e => e.AssetStatusFinId).HasColumnName("AssetStatusFinID");

                entity.Property(e => e.AssetStatusFin1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("AssetStatusFin");

                entity.Property(e => e.AssetStatusFinDesc)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CompanyName).HasMaxLength(200);

                entity.Property(e => e.CompanyType)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Website).HasMaxLength(200);
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.ContractNo);

                entity.ToTable("Contract");

                entity.Property(e => e.ContractNo).HasMaxLength(50);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ContractOwner)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContractValue).HasColumnType("money");

                entity.Property(e => e.EndContract).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartContract).HasColumnType("datetime");

                entity.Property(e => e.Subject).HasMaxLength(500);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ContractCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Company");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ContractSuppliers)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Company1");
            });

            modelBuilder.Entity<DeliveryOrder>(entity =>
            {
                entity.HasKey(e => e.DeliveryOderNo)
                    .HasName("PK_OrderReceived");

                entity.ToTable("DeliveryOrder");

                entity.Property(e => e.DeliveryOderNo).HasMaxLength(50);

                entity.Property(e => e.ContractNo).HasMaxLength(100);

                entity.Property(e => e.DeliverOrderDate).HasColumnType("datetime");

                entity.Property(e => e.Dodoc).HasColumnName("DODoc");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ReceivedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CompanyNavigation)
                    .WithMany(p => p.DeliveryOrderCompanyNavigations)
                    .HasForeignKey(d => d.Company)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryOrder_Company1");

                entity.HasOne(d => d.SupplierNameNavigation)
                    .WithMany(p => p.DeliveryOrderSupplierNameNavigations)
                    .HasForeignKey(d => d.SupplierName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryOrder_Company");
            });

            modelBuilder.Entity<Dodetail>(entity =>
            {
                entity.HasKey(e => e.DodetailIno);

                entity.ToTable("DODetails");

                entity.Property(e => e.DodetailIno)
                    .HasMaxLength(50)
                    .HasColumnName("DODetailINo");

                entity.Property(e => e.AcquisitionPrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.DeliveryOrderNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DescriptionItem).HasMaxLength(200);

                entity.Property(e => e.LastModiedDate).HasColumnType("datetime");

                entity.Property(e => e.LeasePrice).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductCatalogId).HasColumnName("ProductCatalogID");

                entity.Property(e => e.PurchaseType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasColumnType("ntext");

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DeliveryOrderNoNavigation)
                    .WithMany(p => p.Dodetails)
                    .HasForeignKey(d => d.DeliveryOrderNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DODetails_DeliveryOrder1");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Employee");

                entity.Property(e => e.UserId).HasMaxLength(100);

                entity.Property(e => e.AreaId)
                    .HasMaxLength(10)
                    .HasColumnName("AreaID")
                    .IsFixedLength();

                entity.Property(e => e.AreaName).HasMaxLength(50);

                entity.Property(e => e.AssignmentType)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.BackToBack).HasMaxLength(500);

                entity.Property(e => e.Ccid)
                    .HasMaxLength(50)
                    .HasColumnName("CCID");

                entity.Property(e => e.Ccname)
                    .HasMaxLength(50)
                    .HasColumnName("CCName");

                entity.Property(e => e.Company).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EmpNum).HasMaxLength(20);

                entity.Property(e => e.Fungsi).HasMaxLength(50);

                entity.Property(e => e.Idnum)
                    .HasMaxLength(50)
                    .HasColumnName("IDNum");

                entity.Property(e => e.Idtype)
                    .HasMaxLength(10)
                    .HasColumnName("IDType")
                    .IsFixedLength();

                entity.Property(e => e.Kbo)
                    .HasMaxLength(10)
                    .HasColumnName("KBO")
                    .IsFixedLength();

                entity.Property(e => e.Kboname)
                    .HasMaxLength(50)
                    .HasColumnName("KBOName");

                entity.Property(e => e.LocCategory).HasMaxLength(50);

                entity.Property(e => e.LocGroup).HasMaxLength(50);

                entity.Property(e => e.ParentPosId)
                    .HasMaxLength(50)
                    .HasColumnName("ParentPosID");

                entity.Property(e => e.ParentPosName).HasMaxLength(50);

                entity.Property(e => e.ParentUserId)
                    .HasMaxLength(50)
                    .HasColumnName("ParentUserID");

                entity.Property(e => e.PersNo).HasMaxLength(50);

                entity.Property(e => e.PosId)
                    .HasMaxLength(50)
                    .HasColumnName("PosID");

                entity.Property(e => e.PosName).HasMaxLength(50);

                entity.Property(e => e.Section).HasMaxLength(50);

                entity.Property(e => e.SectionId)
                    .HasMaxLength(10)
                    .HasColumnName("SectionID")
                    .IsFixedLength();

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SubAreaId)
                    .HasMaxLength(10)
                    .HasColumnName("SubAreaID")
                    .IsFixedLength();

                entity.Property(e => e.SubAreaName).HasMaxLength(50);

                entity.Property(e => e.SubFunction).HasMaxLength(50);

                entity.Property(e => e.UnitId)
                    .HasMaxLength(10)
                    .HasColumnName("UnitID")
                    .IsFixedLength();

                entity.Property(e => e.UnitName).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<MasterRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("MasterRole");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoleDescription).HasMaxLength(200);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProductCatalog>(entity =>
            {
                entity.ToTable("ProductCatalog");

                entity.Property(e => e.ProductCatalogId).HasColumnName("ProductCatalogID");

                entity.Property(e => e.AdditionalInfo).HasColumnType("ntext");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.HyperlinkDataSheet).HasMaxLength(200);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModelVersion)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProdCatTier1)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProdCatTier2)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProdCatTier3)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ProductNumber).HasMaxLength(200);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ProductCatalogs)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCatalog_Company");
            });

            modelBuilder.Entity<RequestPurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.Rpono);

                entity.ToTable("RequestPurchaseOrder");

                entity.Property(e => e.Rpono)
                    .HasMaxLength(50)
                    .HasColumnName("RPONo");

                entity.Property(e => e.AdditionalInfo).HasColumnType("ntext");

                entity.Property(e => e.ContractNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Roend)
                    .HasColumnType("datetime")
                    .HasColumnName("ROEnd");

                entity.Property(e => e.Rostart)
                    .HasColumnType("datetime")
                    .HasColumnName("ROStart");

                entity.Property(e => e.Rposubject)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("RPOSubject");

                entity.Property(e => e.TotalRpo)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("TotalRPO");

                entity.HasOne(d => d.ContractNoNavigation)
                    .WithMany(p => p.RequestPurchaseOrders)
                    .HasForeignKey(d => d.ContractNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestPurchaseOrder_Contract");
            });

            modelBuilder.Entity<RoleMapping>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RoleMapping");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleMapping_MasterRole");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleMapping_Employee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
