using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class AssetStatus
    {
        public AssetStatus()
        {
            AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
        }

        public int AssetStatusId { get; set; }
        public string AssetStatus1 { get; set; }
        public string AssetStatusDescription { get; set; }
        public bool AssociationStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
    }
}
