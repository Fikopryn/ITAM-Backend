using System;
using System.Collections.Generic;

namespace WebApi.AssetData
{
    public partial class Company
    {
        public Company()
        {
            AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
            ContractCompanies = new HashSet<Contract>();
            ContractSuppliers = new HashSet<Contract>();
            DeliveryOrderCompanyNavigations = new HashSet<DeliveryOrder>();
            DeliveryOrderSupplierNameNavigations = new HashSet<DeliveryOrder>();
            ProductCatalogs = new HashSet<ProductCatalog>();
        }

        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyType { get; set; }
        public string Abbreviation { get; set; } = null!;
        public string? Website { get; set; }
        public bool? CompanyStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
        public virtual ICollection<Contract> ContractCompanies { get; set; }
        public virtual ICollection<Contract> ContractSuppliers { get; set; }
        public virtual ICollection<DeliveryOrder> DeliveryOrderCompanyNavigations { get; set; }
        public virtual ICollection<DeliveryOrder> DeliveryOrderSupplierNameNavigations { get; set; }
        public virtual ICollection<ProductCatalog> ProductCatalogs { get; set; }
    }
}
