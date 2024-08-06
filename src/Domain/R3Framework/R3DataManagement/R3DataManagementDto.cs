namespace Domain.R3Framework.R3DataManagement
{
    #region User Application Data
    public class R3AppUserData
    {
        public R3AppUserInfo UserInformation { get; set; }
        public R3DoaInfo DoaInformation { get; set; }
        public R3AppRoleInfo RoleInformation { get; set; }
    }

    public class R3AppUserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public object EmpNum { get; set; }
        public string Email { get; set; }
        public string PosId { get; set; }
        public string PosName { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string CoyCode { get; set; }
        public string CoyName { get; set; }
        public string AreaId { get; set; }
        public string AreaName { get; set; }
        public string SubAreaId { get; set; }
        public string SubAreaName { get; set; }
        public string Section { get; set; }
        public string CcId { get; set; }
        public string CcName { get; set; }
        public object BackToBack { get; set; }
        public object PosCategory { get; set; }
        public string AuthServer1 { get; set; }
        public object AuthServer2 { get; set; }
        public string Kbo { get; set; }
        public string UidType { get; set; }
        public string ParentUserId { get; set; }
        public string LocGroup { get; set; }
        public string LocCategory { get; set; }
        public string SubFungsi { get; set; }
        public string Fungsi { get; set; }
    }

    public class R3DoaInfo
    {
        public List<R3DelegationOfAuthority> Doa { get; set; }
    }

    public class R3DelegationOfAuthority
    {
        public string UserId { get; set; }
        public string PosName { get; set; }
        public int ParentPosId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DoaType { get; set; }
    }

    public class R3AppRoleInfo
    {
        public List<R3AppRole> Role { get; set; }

        public R3AppRoleInfo()
        {
            Role = new List<R3AppRole>();
        }
    }

    public class R3AppRole
    {
        public string SysName { get; set; }
        public string SysRole { get; set; }
        public List<R3AccessControlList> Acl { get; set; }
        public string Company { get; set; }
        public string Seqrole { get; set; }
    }

    public class R3AccessControlList
    {
        public string ComponentId { get; set; }
        public string UrlPath { get; set; }
        public bool Hidden { get; set; }
        public bool Readonly { get; set; }
        public bool Disabled { get; set; }
        public string Page { get; set; }
    }
    #endregion

    #region Employee
    public class R3Employees
    {
        public List<R3EmployeeInfo> Employee { get; set; }
    }

    public class R3EmployeeInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int PosId { get; set; }
        public string PosName { get; set; }
        public int ParentPosId { get; set; }
        public string ParentPosName { get; set; }
        public string UidType { get; set; }
        public object EmpNum { get; set; }
        public string Email { get; set; }
        public string AssignmentType { get; set; }
        public object BackToBack { get; set; }
        public string ParentUserId { get; set; }
        public string Function { get; set; }
        public string Location { get; set; }
        public string SubFunction { get; set; }
        public string LocationGroup { get; set; }
    }
    #endregion

    #region Structural
    public enum StructuralType
    {
        UP,
        DOWN
    }

    public class R3StructuralRequest
    {
        public string USERID { get; set; }
        public string STRUCTURALTYPE { get; set; }
    }

    public class R3Structurals
    {
        public List<R3Structural> Structural { get; set; }
    }

    public class R3Structural
    {
        public string Lvl { get; set; }
        public int PosId { get; set; }
        public string PosName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ParentPosId { get; set; }
        public string ParentPosName { get; set; }
        public object BackToBack { get; set; }
        public string Path { get; set; }
        public string Email { get; set; }
        public string PosCategory { get; set; }
        public object DoaNum { get; set; }
        public object ActorUserId { get; set; }
        public object DoaType { get; set; }
        public object Attachment { get; set; }
        public object StartDate { get; set; }
        public object EndDate { get; set; }
        public string Function { get; set; }
        public string Location { get; set; }
        public string SubFunction { get; set; }
        public string LocationGroup { get; set; }
    }
    #endregion

    #region globalquery
    public class QueryParam
    {
        public string QUERYID { get; set; }
        public List<QueryParamDetail> PARAM { get; set; }
    }

    public class QueryParamDetail
    {
        public int PARAMNUMBER { get; set; }
        public string VALUE { get; set; }
        public string TYPE { get; set; }
    }
    public class QueryResult
    {
        public List<QueryResultWrapper> resultData { get; set; }
    }
    public class QueryResultWrapper
    {
        public QueryResultRow ROW { get; set; }
    }
    public class QueryResultRow
    {
        public string @num { get; set; }
        //attribute bellow are DEPENDS ON YOUR GLOBALQUERY SELECT ATTRIBUTE
        public string USERID { get; set; }
        public string USERNAME { get; set; }
        public string EMAIL { get; set; }
    }
    #endregion

    #region r3tina api
    public class CascaderFilter
    {
        public string Division { get; set; }
        public string Subdivision { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public int Level { get; set; }
    }
    public class CascaderResult
    {
        public string Status { get; set; }
        public List<Cascader> data { get; set; }
    }
    public class Cascader
    {
        public string Value { get; set; }
        public string Label { get; set; }
        public bool Leaf { get; set; }
        public int Level { get; set; }
    }
    public class CascaderRawResult
    {
        public string Status { get; set; }
        public List<CascaderRaw> data { get; set; }
    }
    public class CascaderRaw
    {
        public string? Divisionid { get; set; }
        public string Division { get; set; }
        public string? Subdivisionid { get; set; }
        public string Subdivision { get; set; }
        public string? Departmentid { get; set; }
        public string Department { get; set; }
        public string? Sectionid { get; set; }
        public string Section { get; set; }
    }
    public class DynamicCascaderResult
    {
        public string Status { get; set; }
        public List<Dictionary<string, object>> data { get; set; }
        #endregion
    }
}
