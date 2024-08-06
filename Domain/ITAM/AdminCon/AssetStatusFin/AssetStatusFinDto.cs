using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.AssetStatusFin
{
    public class AssetStatusFinDto
    {
        public int AssetStatusFinId { get; set; }
        public string AssetStatusFin1 { get; set; } = null!;
        public string AssetStatusFinDesc { get; set; } = null!;
        public bool? AssociationStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
