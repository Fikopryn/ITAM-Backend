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

namespace Domain.ITAM.AdminCon.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private IMemoryCache _cache;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        //private readonly IMasterLovService _masterLovService;
        public EmployeeService (IUnitOfWork uow, IMapper mapper/*, MasterLovService masterLovService*/)
        {
            _uow = uow;
            _mapper = mapper;
            //_cache = cache;
            //_masterLovService = masterLovService;
        }

        //get
        public async Task<Result<List<EmployeeDto>>> GetAllEmployee()
        {
            try
            {
                var repoResult = await _uow.Employee.Set().OrderBy(m => m.Company).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<EmployeeDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        public async Task<Result<List<EmployeeDto>>> GetEmployeeList(string userId)
        {
            try
            {
                List<TEmployee> employeeActivities = new List<TEmployee>();
                employeeActivities = await _uow.Employee.Set().Where(a => a.UserId.Equals(userId)).OrderByDescending(b => b.Company).ToListAsync();


                var result = _mapper.Map<List<EmployeeDto>>(employeeActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusList");
            }
        }

        //Put
        public async Task<Result<EmployeeDto>> Update(EmployeeDto updateParam)
        {
            try
            {
                var repoCheck = await _uow.Employee.Set().FirstOrDefaultAsync(r => r.Company == updateParam.Company);
                if (repoCheck == null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record not found!");
                }

                repoCheck.Company = updateParam.Company;
                repoCheck.UserId = updateParam.UserId;
                repoCheck.EmpNum = updateParam.EmpNum;
                repoCheck.PersNo = updateParam.PersNo;
                repoCheck.UserName = updateParam.UserName;
                repoCheck.Email = updateParam.Email;
                repoCheck.Idtype = updateParam.Idtype;
                repoCheck.Idnum = updateParam.Idnum;
                repoCheck.Sex = updateParam.Sex;
                repoCheck.AssignmentType = updateParam.AssignmentType;
                repoCheck.IsActive = updateParam.IsActive;
                repoCheck.Ccid = updateParam.Ccid;
                repoCheck.Ccname = updateParam.Ccname;
                repoCheck.Kbo = updateParam.Kbo;
                repoCheck.Kboname = updateParam.Kboname;
                repoCheck.PosId = updateParam.PosId;
                repoCheck.PosName = updateParam.PosName;
                repoCheck.ParentPosId = updateParam.ParentPosId;
                repoCheck.Fungsi = updateParam.Fungsi;
                repoCheck.SubFunction = updateParam.SubFunction;
                repoCheck.SectionId = updateParam.SectionId;
                repoCheck.Section = updateParam.Section;
                repoCheck.UnitId = updateParam.UnitId;
                repoCheck.UnitName = updateParam.UnitName;
                repoCheck.LocCategory = updateParam.LocCategory;
                repoCheck.LocGroup = updateParam.LocGroup;
                repoCheck.AreaId = updateParam.AreaId;
                repoCheck.AreaName = updateParam.AreaName;
                repoCheck.SubAreaId = updateParam.SubAreaId;
                repoCheck.SubAreaName = updateParam.SubAreaName;
                repoCheck.BackToBack = updateParam.BackToBack;


                _uow.Employee.Update(repoCheck);
                await _uow.CompleteAsync();

                var result = _mapper.Map<EmployeeDto>(repoCheck);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //post
        public async Task<Result<EmployeeDto>> CreateEmployee(EmployeeDto createParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.Employee.Set().FirstOrDefaultAsync(r => r.Company == createParam.Company);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TEmployee>(createParam);

                await _uow.Employee.Add(dataToAdd);

                await _uow.CompleteAsync();

                var result = _mapper.Map<EmployeeDto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        //Delete
        public async Task<Result<EmployeeDto>> Delete(string company)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.Employee.Set()
                    .FirstOrDefaultAsync(m => m.Company == company);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.Employee.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<EmployeeDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }

}
