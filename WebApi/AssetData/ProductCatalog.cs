using System;
using System.Collections.Generic;

namespace WebApi.AssetData
{
    public partial class ProductCatalog
    {
        public ProductCatalog()
        {
            AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
        }

        public int ProductCatalogId { get; set; }
        public int CompanyId { get; set; }
        public string ProdCatTier1 { get; set; } = null!;
        public string ProdCatTier2 { get; set; } = null!;
        public string ProdCatTier3 { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public string ModelVersion { get; set; } = null!;
        public string? ProductNumber { get; set; }
        public string? HyperlinkDataSheet { get; set; }
        public string? AdditionalInfo { get; set; }
        public bool ProdCatStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
    }
}
