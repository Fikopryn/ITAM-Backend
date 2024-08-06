using System;
using System.Collections.Generic;

namespace Core.test
{
    public partial class Employee
    {
        public Employee()
        {
            AssetGeneralInfos = new HashSet<AssetGeneralInfo>();
        }

        public string Company { get; set; }
        public string UserId { get; set; }
        public string EmpNum { get; set; }
        public string PersNo { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Idtype { get; set; }
        public string Idnum { get; set; }
        public string Sex { get; set; }
        public string AssignmentType { get; set; }
        public bool? IsActive { get; set; }
        public string Ccid { get; set; }
        public string Ccname { get; set; }
        public string Kbo { get; set; }
        public string Kboname { get; set; }
        public string PosId { get; set; }
        public string PosName { get; set; }
        public string ParentPosId { get; set; }
        public string ParentPosName { get; set; }
        public string ParentUserId { get; set; }
        public string Fungsi { get; set; }
        public string SubFunction { get; set; }
        public string SectionId { get; set; }
        public string Section { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string LocCategory { get; set; }
        public string LocGroup { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string SubAreaId { get; set; }
        public string SubAreaName { get; set; }
        public string BackToBack { get; set; }

        public virtual ICollection<AssetGeneralInfo> AssetGeneralInfos { get; set; }
    }
}
