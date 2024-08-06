using Domain.R3Framework.R3User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain._Example.AuditTrailActivity
{
    public interface IAuditTrailActivityService
    {
        Task<Result<List<AuditTrailActivityDto>>> GetAuditTrailActivityList(string moduleName, Guid moduleId);
        Task<Result<AuditTrailActivityDto>> CreateAuditTrailActivity(R3UserSession userSession, string action, string moduleName, Guid moduleId, string remarks);
        Task<Result<bool>> DeleteAuditTrailActivity(string moduleName, Guid moduleId);
    }
}
