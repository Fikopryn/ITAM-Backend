namespace Domain.R3Framework.R3ApprovalCenter
{
    #region action
    public class ApprovalActionResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string id { get; set; }
        public string userId { get; set; }
        public string moduleCode { get; set; }
        public string applicationCode { get; set; }

        public ApprovalActionResponse(string status, string message, string id, string userId, string moduleCode,
                string applicationCode)
        {
            this.status = status;
            this.message = message;
            this.id = id;
            this.userId = userId;
            this.moduleCode = moduleCode;
            this.applicationCode = applicationCode;
        }
    }
    public class ApprovalActionRequest
    {
        public string id { get; set; }
        public string userId { get; set; }
        public string actionString { get; set; }
        public string comments { get; set; }
        public string moduleCode { get; set; }
        public string applicationCode { get; set; }
        public ResponsePreview responsePreview { get; set; }
        public ApprovalActionRequest() { }

        public ApprovalActionRequest(string id, string userId, string actionString, string comments, string moduleCode,
                string applicationCode, ResponsePreview responsePreview)
        {
            this.id = id;
            this.userId = userId;
            this.actionString = actionString;
            this.comments = comments;
            this.moduleCode = moduleCode;
            this.applicationCode = applicationCode;
            this.responsePreview = responsePreview;
        }
    }
    public class ResponsePreview
    {
        public List<RowPreview> rowPreview { get; set; }

        public ResponsePreview()
        {
            rowPreview = null;
        }
    }
    #endregion
    #region preview
    public class ApprovalPreviewResponse
    {
        public ApprovalPreview approvalPreview { get; set; }

        public ApprovalPreviewResponse()
        {
            approvalPreview = new ApprovalPreview();
        }

        public ApprovalPreviewResponse(ApprovalPreview approvalPreview)
        {
            this.approvalPreview = approvalPreview;
        }
    }
    public class ApprovalPreview
    {
        public List<RowPreview> rowPreview { get; set; }
        public List<Action> action { get; set; }
        public string id { get; set; }
        public string userId { get; set; }
        public string moduleCode { get; set; }
        public string applicationCode { get; set; }

        public ApprovalPreview()
        {
            rowPreview = null;
            action = null;
        }

        public ApprovalPreview(List<RowPreview> rowPreview, List<Action> action, string id, string userId,
                string moduleCode, string applicationCode)
        {
            this.rowPreview = rowPreview;
            this.action = action;
            this.id = id;
            this.userId = userId;
            this.moduleCode = moduleCode;
            this.applicationCode = applicationCode;
        }
    }
    public class Action
    {
        public string stringAction { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public bool enabledComment { get; set; }

        public Action(string stringAction, string description, string type, bool enabledComment)
        {
            this.stringAction = stringAction;
            this.description = description;
            this.type = type;
            this.enabledComment = enabledComment;
        }
    }
    public class RowPreview
    {
        public List<Preview> preview { get; set; }

        public RowPreview()
        {
            preview = null;
        }

        public RowPreview(List<Preview> preview)
        {
            this.preview = preview;
        }
    }
    public class Preview
    {
        public string name { get; set; }
        public string value { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public List<Option> options { get; set; }
        public bool disabled { get; set; }
        public Preview(string name, string value, string label, string type, List<Option> options, bool disabled)
        {
            this.name = name;
            this.value = value;
            this.label = label;
            this.type = type;
            this.options = options;
            this.disabled = disabled;
        }
        public Preview()
        {
            options = null;
        }
    }
    public class Option
    {
        public string value { get; set; }
        public string text { get; set; }

        public Option() { }

        public Option(string value, string text)
        {
            this.value = value;
            this.text = text;
        }
    }
    #endregion
    #region list
    public class ApprovalListResponse
    {
        public List<ApprovalList> approvalList { get; set; }
        public List<TotalApproval> totalApproval { get; set; }

        public ApprovalListResponse() {
            approvalList = null;
            totalApproval = null;
        }

        public ApprovalListResponse(List<ApprovalList> approvalList, List<TotalApproval> totalApproval)
        {
            this.approvalList = approvalList;
            this.totalApproval = totalApproval;
        }
    }
    public class ApprovalList
    {
        public string id { get; set; }
        public Subject subject { get; set; }
        public string documentDate { get; set; }
        public Requestor requestor { get; set; }
        public string status { get; set; }
        public string moduleCode { get; set; }
        public string applicationCode { get; set; }

        public ApprovalList(string id, Subject subject, string documentDate, Requestor requestor, string status,
                string moduleCode, string applicationCode)
        {
            this.id = id;
            this.subject = subject;
            this.documentDate = documentDate;
            this.requestor = requestor;
            this.status = status;
            this.moduleCode = moduleCode;
            this.applicationCode = applicationCode;
        }
    }
    public class Requestor
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string positionName { get; set; }
        public string function { get; set; }

        public Requestor(string userid, string username, string positionName, string function)
        {
            this.userid = userid;
            this.username = username;
            this.positionName = positionName;
            this.function = function;
        }
    }
    public class Subject
    {
        public string documentNumber { get; set; }
        public string subject { get; set; }
        public Subject(string documentNumber, string subject)
        {
            this.documentNumber = documentNumber;
            this.subject = subject;
        }
    }
    public class TotalApproval
    {
        public string applicationCode { get; set; }
        public string moduleCode { get; set; }
        public int totalApproval { get; set; }
        public TotalApproval() { }
        public TotalApproval(string applicationCode, string moduleCode, int totalApproval)
        {
            this.applicationCode = applicationCode;
            this.moduleCode = moduleCode;
            this.totalApproval = totalApproval;
        }
    }
    #endregion
    #region wf response
    public class WFCheckApproval
    {
        public string ownerapproval { get; set; }
        public List<CountApproval> countApprovals { get; set; }
        public List<PayloadApproval> payloadApprovals { get; set; }
        public string appname { get; set; }
        public string orgid { get; set; }
        
        public WFCheckApproval()
        {
            countApprovals = null;
            payloadApprovals = null;
        }
    }
    public class PayloadApproval
    {
        public string processId { get; set; }
        public string approvalId { get; set; }
        public string requestor { get; set; }
        public string posname { get; set; }
        public string function { get; set; }
        public string fullname { get; set; }
        public string requestDate { get; set; }
    }
    public class CountApproval
    {
        public string processId { get; set; }
        public int count { get; set; }
}
    #endregion
}
