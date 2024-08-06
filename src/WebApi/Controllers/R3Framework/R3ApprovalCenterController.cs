using Core.Helpers;
using Domain.R3Framework.R3ApprovalCenter;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.R3Framework
{
    [Route("api/r3/approvalcenter")]
    [ApiController]
    public class R3ApprovalCenterController : CustomControllerBase
    {
        private readonly IR3ApprovalCenterService _r3AprovalSvc;

        public R3ApprovalCenterController(
            IR3ApprovalCenterService r3AprovalSvc)
        {
            _r3AprovalSvc = r3AprovalSvc;
        }

        [HttpGet("getList/{userid}/{page}/{size}")]
        [SwaggerOperation(Summary = "Get Current Outstanding Approval List on Application")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetList(string userid, int page, int size, string? filter = null)
        {
            var ret = await _r3AprovalSvc.getList(UserAction, userid, page, size, filter);
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

        [HttpGet("getPreview/{userid}/{approvalid}/{modulecode}/{applicationcode}")]
        [SwaggerOperation(Summary = "Get Preview of Specific Outstanding Approval on Application")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetPreview(string userid, string approvalid, string modulecode, string applicationcode)
        {
            var ret = await _r3AprovalSvc.getPreview(UserAction, userid, approvalid, modulecode, applicationcode);
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

        [HttpPost("postAction")]
        [SwaggerOperation(Summary = "Set Action on Specific Outstanding Approval on Application")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> PostAction(ApprovalActionRequest approvalActionRequest)
        {
            var ret = await _r3AprovalSvc.postAction(UserAction, approvalActionRequest);
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
    }
}
