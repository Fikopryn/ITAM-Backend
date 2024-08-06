using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models.Entities.Tables;
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.ITAM.AdminCon.Company;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.MasterRole
{
    public class MasterRoleService : IMasterRoleService
    {
        //private IMemoryCache _cache;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        //private readonly IMasterLovService _masterLovService;
        public MasterRoleService(IUnitOfWork uow, IMapper mapper/*, MasterLovService masterLovService*/)
        {
            _uow = uow;
            _mapper = mapper;
            //_cache = cache;
            //_masterLovService = masterLovService;
        }
        //Pengambilan Data
        public async Task<Result<List<MasterRoleDto>>> GetAllMasterRole()
        {
            try
            {
                var repoResult = await _uow.MasterRole.Set().OrderByDescending(m => m.RoleId).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<MasterRoleDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        public async Task<Result<List<MasterRoleDto>>> GetMasterRoleList(int roleId)
        {
            try
            {
                List<TMasterRole> MasterRoleActivities = new List<TMasterRole>();
                MasterRoleActivities = await _uow.MasterRole.Set().Where(a => a.RoleId.Equals(roleId)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<MasterRoleDto>>(MasterRoleActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusList");
            }
        }

        public async Task<Result<List<MasterRoleDto>>> GetMasterRoleByName(string roleName)
        {
            try
            {
                List<TMasterRole> MasterRoleActivities = new List<TMasterRole>();
                MasterRoleActivities = await _uow.MasterRole.Set().Where(a => a.RoleName.Equals(roleName)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<MasterRoleDto>>(MasterRoleActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusList");
            }
        }
        public async Task<Result<List<MasterRoleDto>>> GetMasterRoleByDesc(string roleDescription)
        {
            try
            {
                List<TMasterRole> MasterRoleActivities = new List<TMasterRole>();
                MasterRoleActivities = await _uow.MasterRole.Set().Where(a => a.RoleDescription.Equals(roleDescription)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<MasterRoleDto>>(MasterRoleActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusList");
            }
        }
        //Update Data
        public async Task<Result<MasterRoleDto>> Update (MasterRoleDto updateParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.MasterRole.Set().FirstOrDefaultAsync(r => r.RoleId == updateParam.RoleId);
                if (repoCheck == null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record not found!");
                }

                repoCheck.RoleId = updateParam.RoleId;
                repoCheck.RoleName = updateParam.RoleName;
                repoCheck.RoleDescription = updateParam.RoleDescription;
                repoCheck.AssociationStatus = updateParam.AssociationStatus;
                repoCheck.LastModifiedDate = DateTime.Now;
                repoCheck.ModifiedBy = userSession.Username;

                _uow.MasterRole.Update(repoCheck);
                await _uow.CompleteAsync();

                var result = _mapper.Map<MasterRoleDto>(repoCheck);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        //Create Data
        public async Task<Result<MasterRoleDto>> CreateMasterRole(MasterRoleDto createParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.MasterRole.Set().FirstOrDefaultAsync(r => r.RoleId == createParam.RoleId);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TMasterRole>(createParam);

                //dataToAdd.RoleId = 0;

                dataToAdd.ModifiedBy = userSession.Username;

                dataToAdd.LastModifiedDate = DateTime.Now;

                await _uow.MasterRole.Add(dataToAdd);

                await _uow.CompleteAsync();

                var result = _mapper.Map<MasterRoleDto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //Delete 
        public async Task<Result<MasterRoleDto>> Delete(int roleId)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.MasterRole.Set()
                    .FirstOrDefaultAsync(m => m.RoleId == roleId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.MasterRole.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<MasterRoleDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
