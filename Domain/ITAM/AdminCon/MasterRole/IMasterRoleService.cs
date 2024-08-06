using Domain.R3Framework.R3User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.ITAM.AdminCon.MasterRole
{
    public interface IMasterRoleService
    {
        Task<Result<List<MasterRoleDto>>> GetAllMasterRole();
        Task<Result<List<MasterRoleDto>>> GetMasterRoleList(int roleId);
        Task<Result<List<MasterRoleDto>>> GetMasterRoleByName(string roleName);
        Task<Result<List<MasterRoleDto>>> GetMasterRoleByDesc(string roleDescription);
        Task<Result<MasterRoleDto>> Update (MasterRoleDto updateParam, R3UserSession userSession);
        Task<Result<MasterRoleDto>> CreateMasterRole(MasterRoleDto createParam, R3UserSession userSession);
        Task<Result<MasterRoleDto>> Delete(int roleId);
    }
}
