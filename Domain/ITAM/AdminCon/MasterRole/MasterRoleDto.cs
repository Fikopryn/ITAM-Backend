using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.MasterRole
{
    public class MasterRoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public bool? AssociationStatus { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
