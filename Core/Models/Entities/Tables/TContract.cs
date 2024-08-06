﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entities.Tables
{
    public partial class TContract
    {
        public TContract()
        {
            //AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
            //RequestPurchaseOrders = new HashSet<RequestPurchaseOrder>();
        }

        public int CompanyId { get; set; }
        public string ContractOwner { get; set; } = null!;
        public string ContractNo { get; set; } = null!;
        public string? Subject { get; set; }
        public int SupplierId { get; set; }
        public DateTime? StartContract { get; set; }
        public DateTime? EndContract { get; set; }
        public decimal? ContractValue { get; set; }
        public byte[]? ContractDoc { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;

        //public virtual Company Company { get; set; } = null!;
        //public virtual Company Supplier { get; set; } = null!;
        //public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
        //public virtual ICollection<RequestPurchaseOrder> RequestPurchaseOrders { get; set; }
    }
}