using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class Contract
    {
        public Contract()
        {
            AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
            RequestPurchaseOrders = new HashSet<RequestPurchaseOrder>();
        }

        public int CompanyId { get; set; }
        public string ContractOwner { get; set; }
        public string ContractNo { get; set; }
        public string Subject { get; set; }
        public int SupplierId { get; set; }
        public DateTime? StartContract { get; set; }
        public DateTime? EndContract { get; set; }
        public decimal? ContractValue { get; set; }
        public byte[] ContractDoc { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Company Company { get; set; }
        public virtual Company Supplier { get; set; }
        public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
        public virtual ICollection<RequestPurchaseOrder> RequestPurchaseOrders { get; set; }
    }
}
