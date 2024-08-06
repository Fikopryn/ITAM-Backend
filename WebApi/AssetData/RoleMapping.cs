using System;
using System.Collections.Generic;

namespace WebApi.AssetData
{
    public partial class RoleMapping
    {
        public string UserId { get; set; } = null!;
        public int RoleId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;

        public virtual MasterRole Role { get; set; } = null!;
        public virtual Employee User { get; set; } = null!;
    }
}
