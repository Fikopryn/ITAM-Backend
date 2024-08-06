namespace Domain.R3Framework.R3Workflow
{
    public class WorkflowParameter
    {
        public string UserAction { get; set; }
        public string Action { get; set; }
        public string StatusAction { get; set; }
        public string Remarks { get; set; }
    }

    public class WorkflowPayload
    {
        public string ProcessId { get; set; }
        public string ApprovalId { get; set; }
        public string AppName { get; set; }
        public WorkflowUser User { get; set; }
        public string Action { get; set; }
        public string StatusAction { get; set; }
        public WorkflowEmailConfiguration EmailConfiguration { get; set; }
        public IDictionary<string, string> AliasApprover { get; set; }
        public List<WorkflowPayloadParameter> Parameters { get; set; }
        public string Remarks { get; set; }
        public bool RunFuture { get; set; }
        public string Orgid { get; set; }
    }

    public class WorkflowPayloadParameter
    {
        public int Id { get; set; }
        public string Variable { get; set; }
        public string Value { get; set; }
    }

    public class WorkflowUser
    {
        public string Alias { get; set; }
    }

    public class WorkflowEmailConfiguration
    {
        public string Subject { get; set; }
        public string TemplateName { get; set; }
        public ContentEmail ContentEmail { get; set; }
    }

    #region Response Payload
    public class RunWorkflowResponse
    {
        public string Type { get; set; }
        public string NodeName { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> AliasApprover { get; set; }
        public string ClassName { get; set; }
    }

    public class CurrentApprovalResponse
    {
        public int RowStamp { get; set; }
        public string ApprovalId { get; set; }
        public DateTime CreationDate { get; set; }
        public string OwnerApproval { get; set; }
        public int ProcessRev { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public int WfAppId { get; set; }
        public int WfAssignId { get; set; }
        public int WfProcessId { get; set; }
        public string OwnerType { get; set; }
        public int Doa { get; set; }
        public string OrgId { get; set; }
        public string PosName { get; set; }
        public int WfNodeId { get; set; }
    }

    public class HistoryApprovalResponse
    {
        public int RowStamp { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public DateTime ActionDate { get; set; }
        public string ApprovalId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string Flag { get; set; }
        public string Remarks { get; set; }
        public string OwnerApproval { get; set; }
        public int ProcessRev { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public int WfAppId { get; set; }
        public int WfAssignId { get; set; }
        public int WfProcessId { get; set; }
        public string OwnerType { get; set; }
        public string OrgId { get; set; }
        public string NodeId { get; set; }
        public string PosName { get; set; }
        public string WfNodeId { get; set; }
    }

    public class OutstandingApprovalResponse
    {
        public int RowStamp { get; set; }
        public string ApprovalId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public string OwnerApproval { get; set; }
        public int ProcessRev { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public int WfAppId { get; set; }
        public int WfAssignId { get; set; }
        public int WfProcessId { get; set; }
        public string OwnerType { get; set; }
        public bool Doa { get; set; }
        public string OrgId { get; set; }
        public string PosName { get; set; }
        public string WfNodeId { get; set; }
    }
    #endregion

    #region Custom Dto Just For This App
    // Change this property class as r3tina email template needs
    public class ContentEmail
    {
        public string Name { get; set; }
    }
    #endregion
}
