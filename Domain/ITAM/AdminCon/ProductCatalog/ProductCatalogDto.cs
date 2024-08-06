using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.ProductCatalog
{
    public class ProductCatalogDto
    {
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

    }
}
