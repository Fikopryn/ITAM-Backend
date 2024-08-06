using Core;
using Core.Helpers;
using Core.Models;
using Domain.R3Framework.R3Workflow;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.R3Framework
{
    [Route("api/r3/workflow")]
    [ApiController]
    public class R3WorkflowController : CustomControllerBase
    {
        private readonly WorkflowConfig _wfConfig;
        private readonly IR3WorkflowService _r3WfSvc;

        public R3WorkflowController(
            IOptions<WorkflowConfig> wfConfig,
            IR3WorkflowService r3WfSvc)
        {
            _wfConfig = wfConfig.Value;
            _r3WfSvc = r3WfSvc;
        }

        [HttpPost("run")]
        [SwaggerOperation(Summary = "Run Workflow")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> RunWorkflow(string appId, WorkflowParameter wfParam)
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

            var ret = await _r3WfSvc.RunWorkflow(UserAction, appId, wfParam, emailNotification);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpGet("currentapproval/{appId}")]
        [SwaggerOperation(Summary = "Get Current Workflow Approval")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetCurrentApproval(string appId)
        {
            var ret = await _r3WfSvc.GetCurrentApproval(UserAction, appId);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpGet("historyapproval/{appId}")]
        [SwaggerOperation(Summary = "Get History Workflow Approval")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetHistoryApproval(string appId)
        {
            var ret = await _r3WfSvc.GetHistoryApproval(UserAction, appId);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpGet("outstanding")]
        [SwaggerOperation(Summary = "Get Outstanding Application Approval for Current User")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetOutstandingApproval()
        {
            var ret = await _r3WfSvc.GetOutstandingApproval(UserAction, UserAction.ImpersonateAs == null? UserAction.Username : UserAction.ImpersonateAs);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpGet("useraction/{appId}/{user}")]
        [SwaggerOperation(Summary = "Get User Action")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetUserAction(string appId, string user)
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

            var wfParam = new WorkflowParameter
            {
                UserAction = user,
                Action = "",
                StatusAction = "",
                Remarks = ""
            };

            var ret = await _r3WfSvc.RunWorkflow(UserAction, appId, wfParam, emailNotification);

            if (ret.IsSuccess)
            {
                var result = new List<string>();

                if (ret.Value.Type == R3WorkflowType.INPUT)
                {
                    var dict = JsonConvert.DeserializeObject<IDictionary<string, string>>(ret.Value.Message);
                    if (dict != null) result = dict.Select(m => m.Key).ToList();
                }

                return Ok(result);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }
    }
}
