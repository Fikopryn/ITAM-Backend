using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.AssetStatus
{
    public class AssetStatusDto
    {
        public int AssetStatusId { get; set; }
        public string AssetStatus1 { get; set; } = null!;
        public string? AssetStatusDescription { get; set; }
        public bool AssociationStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
