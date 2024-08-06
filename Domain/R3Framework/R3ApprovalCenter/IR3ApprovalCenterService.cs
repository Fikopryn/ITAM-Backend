using Domain.R3Framework.R3User;
using FluentResults;

namespace Domain.R3Framework.R3ApprovalCenter
{
    public interface IR3ApprovalCenterService
    {
        Task<Result<ApprovalListResponse>> getList(R3UserSession r3UserSession, string userid, int page, int size, string filter);
        Task<Result<ApprovalPreviewResponse>> getPreview(R3UserSession r3UserSession, string userid, string approvalid, string modulecode, string applicationcode);
        Task<Result<ApprovalActionResponse>> postAction(R3UserSession r3UserSession, ApprovalActionRequest approvalActionRequest);
    }
}
