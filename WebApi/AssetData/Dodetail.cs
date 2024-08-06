using System;
using System.Collections.Generic;

namespace WebApi.AssetData
{
    public partial class Dodetail
    {
        public string DeliveryOrderNo { get; set; } = null!;
        public string DodetailIno { get; set; } = null!;
        public string? DescriptionItem { get; set; }
        public int Quatity { get; set; }
        public string Unit { get; set; } = null!;
        public int ProductCatalogId { get; set; }
        public string PurchaseType { get; set; } = null!;
        public decimal? AcquisitionPrice { get; set; }
        public decimal? LeasePrice { get; set; }
        public string? Remark { get; set; }
        public DateTime LastModiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual DeliveryOrder DeliveryOrderNoNavigation { get; set; } = null!;
    }
}
