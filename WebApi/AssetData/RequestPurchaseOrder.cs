using System;
using System.Collections.Generic;

namespace WebApi.AssetData
{
    public partial class RequestPurchaseOrder
    {
        public string ContractNo { get; set; } = null!;
        public string Rpono { get; set; } = null!;
        public string Rposubject { get; set; } = null!;
        public string? AdditionalInfo { get; set; }
        public DateTime Rostart { get; set; }
        public DateTime Roend { get; set; }
        public decimal TotalRpo { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual Contract ContractNoNavigation { get; set; } = null!;
    }
}
