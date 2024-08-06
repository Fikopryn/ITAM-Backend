using System;
using System.Collections.Generic;

namespace WebApi.AssetData
{
    public partial class DeliveryOrder
    {
        public DeliveryOrder()
        {
            Dodetails = new HashSet<Dodetail>();
        }

        public int Company { get; set; }
        public string? ContractNo { get; set; }
        public string DeliveryOderNo { get; set; } = null!;
        public DateTime DeliverOrderDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public int SupplierName { get; set; }
        public byte[]? Dodoc { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual Company CompanyNavigation { get; set; } = null!;
        public virtual Company SupplierNameNavigation { get; set; } = null!;
        public virtual ICollection<Dodetail> Dodetails { get; set; }
    }
}
