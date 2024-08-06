using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models.Entities.Tables;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain._Example.AuditTrailActivity
{
    public class AuditTrailActivityService : IAuditTrailActivityService
    {
        //private IMemoryCache _cache;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        //private readonly IMasterLovService _masterLovService;
        public AuditTrailActivityService(IUnitOfWork uow, IMapper mapper/*, MasterLovService masterLovService*/)
        {
            _uow = uow;
            _mapper = mapper;
            //_cache = cache;
            //_masterLovService = masterLovService;
        }
        public async Task<Result<List<AuditTrailActivityDto>>> GetAuditTrailActivityList(string moduleName, Guid moduleId)
        {
            try
            {
                List<TExAuditTrailActivity> auditTrailActivities = new List<TExAuditTrailActivity>();
                auditTrailActivities = await _uow.ExAuditTrailActivity.Set().Where(a => a.ModulName.Equals(moduleName) && a.ModulId.Equals(moduleId)).OrderByDescending(b => b.Timestamp).ToListAsync();

                var result = _mapper.Map<List<AuditTrailActivityDto>>(auditTrailActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAuditTrailActivityList");
            }
        }
        public async Task<Result<AuditTrailActivityDto>> CreateAuditTrailActivity(R3UserSession userSession, string action, string moduleName, Guid moduleId, string remarks)
        {
            try
            {
                TExAuditTrailActivity auditTrailActivity = new TExAuditTrailActivity();
                auditTrailActivity.ModulName = moduleName;
                auditTrailActivity.ModulId = moduleId;
                auditTrailActivity.UserId = userSession.Username;
                auditTrailActivity.UserName = userSession.name;
                auditTrailActivity.Action = action;
                auditTrailActivity.Timestamp = DateTime.Now;
                auditTrailActivity.Remarks = remarks;
                await _uow.ExAuditTrailActivity.Add(auditTrailActivity);

                await _uow.CompleteAsync();

                var result = _mapper.Map<AuditTrailActivityDto>(auditTrailActivity);

                //ClearCache();

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when CreateAuditTrailActivity");
            }
        }

        public async Task<Result<bool>> DeleteAuditTrailActivity(string moduleName, Guid moduleId)
        {
            try
            {
                _uow.ExAuditTrailActivity.RemoveRange(_uow.ExAuditTrailActivity.Set().Where(a => a.ModulName.Equals(moduleName) && a.ModulId.Equals(moduleId)).ToListAsync().Result);

                await _uow.CompleteAsync();

                return Result.Ok(true);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when DeleteAuditTrailActivity");
            }
        }

        //public void ClearCache()
        //{
        //    try
        //    {
        //        _cache.Dispose();
        //        _cache = new MemoryCache(new MemoryCacheOptions());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ClearCache: " + ex.Message);
        //    }
        //}
    }
}
