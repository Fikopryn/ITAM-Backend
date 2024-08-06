using System;
using System.Collections.Generic;

namespace WebApi.AssetData
{
    public partial class AssetStatusFin
    {
        public AssetStatusFin()
        {
            AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
        }

        public int AssetStatusFinId { get; set; }
        public string AssetStatusFin1 { get; set; } = null!;
        public string AssetStatusFinDesc { get; set; } = null!;
        public bool? AssociationStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
    }
}
