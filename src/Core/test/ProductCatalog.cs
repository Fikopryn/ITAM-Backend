using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class ProductCatalog
    {
        public ProductCatalog()
        {
            AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
        }

        public int ProductCatalogId { get; set; }
        public int CompanyId { get; set; }
        public string ProdCatTier1 { get; set; }
        public string ProdCatTier2 { get; set; }
        public string ProdCatTier3 { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public string ModelVersion { get; set; }
        public string ProductNumber { get; set; }
        public string HyperlinkDataSheet { get; set; }
        public string AdditionalInfo { get; set; }
        public bool ProdCatStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
    }
}
