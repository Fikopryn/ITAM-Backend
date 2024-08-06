using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Text;

namespace Domain.R3Framework.R3Workflow
{
    public class R3WorkflowService : IR3WorkflowService
    {
        private readonly R3tinaConfig _r3Config;
        private readonly R3OAuthConfig _r3OAuthConfig;
        private readonly WorkflowConfig _wfConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public R3WorkflowService(
            IOptions<R3tinaConfig> r3Config,
            IOptions<R3OAuthConfig> r3OAuthConfig,
            IOptions<WorkflowConfig> wfConfig,
            IHttpClientFactory httpClientFactory)
        {
            _r3Config = r3Config.Value;
            _r3OAuthConfig = r3OAuthConfig.Value;
            _wfConfig = wfConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<RunWorkflowResponse>> RunWorkflow(R3UserSession userAction, string appId, WorkflowParameter wfParam, WorkflowEmailConfiguration emailNotification, IDictionary<string, string> aliasApprover = null)
        {
            var url = _r3Config.R3Workflow + "runFlow";
            var payload = GeneratePayload(appId.ToLower(), wfParam, emailNotification, aliasApprover);

            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(payload, serializerSettings), Encoding.UTF8, "application/json")
                };

                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<RunWorkflowResponse>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url, payload), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":Run Workflow Error. " + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<CurrentApprovalResponse>>> GetCurrentApproval(R3UserSession userAction, string appId)
        {
            var url = _r3Config.R3Workflow + $"getCurrentApproval/{_r3Config.AppName}/{appId.ToLower()}/{_wfConfig.ProcessId}/{_wfConfig.OrgId}";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<IEnumerable<CurrentApprovalResponse>>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<HistoryApprovalResponse>>> GetHistoryApproval(R3UserSession userAction, string appId)
        {
            var url = _r3Config.R3Workflow + $"getHistoryApproval/{_r3Config.AppName}/{appId.ToLower()}/{_wfConfig.ProcessId}/{_wfConfig.OrgId}";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<IEnumerable<HistoryApprovalResponse>>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<OutstandingApprovalResponse>>> GetOutstandingApproval(R3UserSession userAction, string userId)
        {
            var url = _r3Config.R3Workflow + $"getOutstanding/{userId}/{_r3Config.AppName}/{_wfConfig.OrgId}";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<IEnumerable<OutstandingApprovalResponse>>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        private WorkflowPayload GeneratePayload(string appId, WorkflowParameter wfParam, WorkflowEmailConfiguration emailNotification, IDictionary<string, string> aliasApprover = null)
        {
            var wfUser = new WorkflowUser
            {
                Alias = wfParam.UserAction
            };

            if (aliasApprover == null)
            {
                aliasApprover = new Dictionary<string, string>() { { "1", "-" } };
            }

            //depend on workflow scheme when using decision component
            var wfPayloadParam = new WorkflowPayloadParameter
            {
                Id = 6,
                Variable = "testParam",
                Value = "#value"
            };

            List<WorkflowPayloadParameter> parameters = new List<WorkflowPayloadParameter>();
            parameters.Add(wfPayloadParam);

            var payload = new WorkflowPayload
            {
                ProcessId = _wfConfig.ProcessId,
                ApprovalId = appId,
                AppName = _r3Config.AppName,
                User = wfUser,
                Action = wfParam.Action,
                StatusAction = wfParam.StatusAction,
                EmailConfiguration = emailNotification,
                AliasApprover = aliasApprover,
                Parameters = parameters,
                Remarks = wfParam.Remarks,
                RunFuture = false,
                Orgid = _wfConfig.OrgId
            };

            return payload;
        }
    }
}
