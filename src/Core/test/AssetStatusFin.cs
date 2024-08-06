using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class AssetStatusFin
    {
        public AssetStatusFin()
        {
            AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
        }

        public int AssetStatusFinId { get; set; }
        public string AssetStatusFin1 { get; set; }
        public string AssetStatusFinDesc { get; set; }
        public bool? AssociationStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
    }
}
