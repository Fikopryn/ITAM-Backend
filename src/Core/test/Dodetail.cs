using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class Dodetail
    {
        public string DeliveryOrderNo { get; set; }
        public string DodetailIno { get; set; }
        public string DescriptionItem { get; set; }
        public int Quatity { get; set; }
        public string Unit { get; set; }
        public int ProductCatalogId { get; set; }
        public string PurchaseType { get; set; }
        public decimal? AcquisitionPrice { get; set; }
        public decimal? LeasePrice { get; set; }
        public string Remark { get; set; }
        public DateTime LastModiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual DeliveryOrder DeliveryOrderNoNavigation { get; set; }
    }
}
