using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class RoleMapping
    {
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual MasterRole Role { get; set; }
        public virtual Employee User { get; set; }
    }
}
