using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class RequestPurchaseOrder
    {
        public string ContractNo { get; set; }
        public string Rpono { get; set; }
        public string Rposubject { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime Rostart { get; set; }
        public DateTime Roend { get; set; }
        public decimal TotalRpo { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Contract ContractNoNavigation { get; set; }
    }
}
