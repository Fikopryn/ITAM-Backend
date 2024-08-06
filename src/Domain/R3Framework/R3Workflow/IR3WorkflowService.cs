using Domain.R3Framework.R3User;
using FluentResults;

namespace Domain.R3Framework.R3Workflow
{
    public interface IR3WorkflowService
    {
        Task<Result<RunWorkflowResponse>> RunWorkflow(R3UserSession userAction, string appId, WorkflowParameter wfParam, WorkflowEmailConfiguration emailNotification, IDictionary<string, string> aliasApprover = null);
        Task<Result<IEnumerable<CurrentApprovalResponse>>> GetCurrentApproval(R3UserSession userAction, string appId);
        Task<Result<IEnumerable<HistoryApprovalResponse>>> GetHistoryApproval(R3UserSession userAction, string appId);
        Task<Result<IEnumerable<OutstandingApprovalResponse>>> GetOutstandingApproval(R3UserSession userAction, string userId);
    }
}
