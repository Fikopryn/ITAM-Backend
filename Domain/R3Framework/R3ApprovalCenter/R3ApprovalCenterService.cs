using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.R3Framework.R3User;
using Domain.R3Framework.R3Workflow;
using FluentResults;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace Domain.R3Framework.R3ApprovalCenter
{
    public class R3ApprovalCenterService : IR3ApprovalCenterService
    {
        private readonly R3tinaConfig _r3Config;
        private readonly R3OAuthConfig _r3OAuthConfig;
        private readonly WorkflowConfig _wfConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IR3WorkflowService _r3WfSvc;

        public R3ApprovalCenterService(
            IOptions<R3tinaConfig> r3Config,
            IOptions<R3OAuthConfig> r3OAuthConfig,
            IOptions<WorkflowConfig> wfConfig,
            IHttpClientFactory httpClientFactory,
            IR3WorkflowService r3WfSvc)
        {
            _r3Config = r3Config.Value;
            _r3OAuthConfig = r3OAuthConfig.Value;
            _wfConfig = wfConfig.Value;
            _httpClientFactory = httpClientFactory;
            _r3WfSvc = r3WfSvc;
        }
        public async Task<Result<ApprovalListResponse>> getList(R3UserSession r3UserSession, string userid, int page, int size, string filter)
        {
            var url = _r3Config.R3Workflow + $"getOutstandingComp/{userid}/{_r3Config.AppName}/{_wfConfig.OrgId}/{page}/{size}" + (filter == null ? "" : "?filter=" + filter);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", r3UserSession.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);


                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<WFCheckApproval>(responseString);
                
                List<ApprovalList> approvalLists = new List<ApprovalList>();
                List<TotalApproval> totalApprovals = new List<TotalApproval>();

                if (retResp != null)
                {
                    foreach (var item in retResp.payloadApprovals)
                    {
                        string documentNumber = _r3Config.AppName + "(" + item.approvalId + ")";
                        string subjectName = "Outstanding " + _r3Config.AppName + " dari " + item.fullname;
                        Subject subject = new Subject(documentNumber, subjectName);
                        Requestor requestor = new Requestor(item.requestor, item.fullname, item.posname, item.function);
                        approvalLists.Add(new ApprovalList(item.approvalId, subject, "[transaction date]", requestor, "[transaction status]", "[transaction module]", _r3Config.AppName));
                    }
                    foreach (var item in retResp.countApprovals)
                    {
                        string modulecode = "[transaction module]";
                        totalApprovals.Add(new TotalApproval(_r3Config.AppName, modulecode, item.count));
                    }
                }
                ApprovalListResponse approvalListResponse = new ApprovalListResponse(approvalLists, totalApprovals);
                return Result.Ok(approvalListResponse);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(r3UserSession.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ApprovalPreviewResponse>> getPreview(R3UserSession r3UserSession, string userid, string approvalid, string modulecode, string applicationcode)
        {
            try
            {
                var emailNotification = new WorkflowEmailConfiguration
                {
                    Subject = _wfConfig.EmailNotificationSubject,
                    TemplateName = _wfConfig.EmailNotificationTemplateName,
                    ContentEmail = new ContentEmail()
                    {
                        Name = "Test R3 Workflow"
                    }
                };
                var wfParam = new WorkflowParameter() 
                {
                    UserAction = userid,
                    Action = "",
                    StatusAction = "",
                    Remarks = ""
                };

                var runflowResponse = await _r3WfSvc.RunWorkflow(r3UserSession, approvalid, wfParam, emailNotification);
                var actions = JsonConvert.DeserializeObject<Dictionary<string, string>>(runflowResponse.Value.Message);
                List<Action> actionList = new List<Action>();
                foreach (KeyValuePair<string, string> entry in actions)
                {
                    actionList.Add(new Action(entry.Key, entry.Value, "button", true));
                }

                // add all transaction data based on approvalid parameter per previews object into rowPreviews
                List<RowPreview> rowPreviews = new List<RowPreview>();
                List<Preview> previews = new List<Preview>();
                previews.Add(new Preview("[transaction column / component name]", "[transaction column / component value]", "[transaction column / component label]", "text", null, true));
                rowPreviews.Add(new RowPreview(previews));
                ApprovalPreview approvalPreview = new ApprovalPreview(rowPreviews, actionList, approvalid, userid, modulecode, applicationcode);
                ApprovalPreviewResponse approvalPreviewResponse = new ApprovalPreviewResponse(approvalPreview);
                return Result.Ok(approvalPreviewResponse);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(r3UserSession.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage()), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<ApprovalActionResponse>> postAction(R3UserSession r3UserSession, ApprovalActionRequest approvalActionRequest)
        {
            try
            {
                // get transaction data first here
                var emailNotification = new WorkflowEmailConfiguration
                {
                    Subject = _wfConfig.EmailNotificationSubject,
                    TemplateName = _wfConfig.EmailNotificationTemplateName,
                    ContentEmail = new ContentEmail()
                    {
                        Name = "Test R3 Workflow"
                    }
                };
                var wfParam = new WorkflowParameter()
                {
                    UserAction = approvalActionRequest.userId,
                    Action = approvalActionRequest.actionString,
                    StatusAction = "",
                    Remarks = approvalActionRequest.comments
                };

                var runflowResponse = await _r3WfSvc.RunWorkflow(r3UserSession, approvalActionRequest.id, wfParam, emailNotification);
                var status = "";

                if (runflowResponse.Value.Type.ToLower().Equals("action"))
                {
                    // you can get transaction data here

                    status = runflowResponse.Value.NodeName;
                    wfParam.StatusAction = "OK";
                    runflowResponse = await _r3WfSvc.RunWorkflow(r3UserSession, approvalActionRequest.id, wfParam, emailNotification);

                    // you can save transaction status here, data from 'runflowResponse.Value.NodeName;'
                }

                if (runflowResponse.Value.Type.ToLower().Equals("email"))
                {
                    wfParam.UserAction = "";
                    wfParam.Action = "";
                    wfParam.StatusAction = "SEND";
                    wfParam.Remarks = "";
                    await _r3WfSvc.RunWorkflow(r3UserSession, approvalActionRequest.id, wfParam, emailNotification); //to trigger mail notification from flow3r
                }

                wfParam.StatusAction = "OK";
                runflowResponse = await _r3WfSvc.RunWorkflow(r3UserSession, approvalActionRequest.id, wfParam, emailNotification);
                ApprovalActionResponse approvalActionResponse = new ApprovalActionResponse(status, runflowResponse.Value.Message, approvalActionRequest.id, approvalActionRequest.userId, approvalActionRequest.moduleCode, approvalActionRequest.applicationCode);

                return Result.Ok(approvalActionResponse);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(r3UserSession.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage()), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
