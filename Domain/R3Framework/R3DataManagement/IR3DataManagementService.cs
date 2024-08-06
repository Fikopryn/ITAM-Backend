using Domain.R3Framework.R3User;
using FluentResults;

namespace Domain.R3Framework.R3DataManagement
{
    public interface IR3DataManagementService
    {
        Task<Result<R3AppUserData>> GetUserAppData(R3UserSession userAction, string userId, string roles = "");
        bool GetImpersonateCapabilty(R3AppUserData r3AppUserData);
        //Task<Result<R3AppUserData>> GetUserImpersonate(R3UserSession userAction, string userId);
        Task<Result<R3EmployeeInfo>> GetEmployee(R3UserSession userAction, string userId, string roles = "");
        Task<Result<R3Structurals>> GetStructural(R3UserSession userAction, string userId, StructuralType structuralType, string roles = "");
        Task<Result<List<Dictionary<string, string>>>> GetGlobalQuery(R3UserSession userAction, QueryParam queryParam);
        Task<Result<CascaderResult>> GetCascaderHierarchy(R3UserSession userAction, CascaderFilter cascaderFilter);
        Task<Result<CascaderRawResult>> GetCascaderRaw(R3UserSession userAction);
        Task<Result<string>> GetCascaderFullStructure(R3UserSession userAction, int maxLvl, int mode, string tenantParam);
    }
}
