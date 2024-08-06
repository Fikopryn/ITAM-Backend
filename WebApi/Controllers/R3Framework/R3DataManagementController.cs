using Core.Helpers;
using Domain.R3Framework.R3DataManagement;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.R3Framework
{
    [Route("api/r3/datamanagement")]
    [ApiController]
    public class R3DataManagementController : CustomControllerBase
    {
        private readonly IR3DataManagementService _r3DmSvc;

        public R3DataManagementController(
            IR3DataManagementService r3DmSvc)
        {
            _r3DmSvc = r3DmSvc;
        }

        [HttpGet("appuserdata")]
        [SwaggerOperation(Summary = "Get Current User Data on Application")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetUserData()
        {
            var ret = await _r3DmSvc.GetUserAppData(UserAction, null, "");

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

        [HttpGet("employee/{userId}")]
        [SwaggerOperation(Summary = "Get Employee Information")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetEmployeeInfo(string userId)
        {
            var ret = await _r3DmSvc.GetEmployee(UserAction, userId);

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

        [HttpGet("structural/{userId}/{structuralType}")]
        [SwaggerOperation(Summary = "Get Structural Information")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetStructuralInfo(string userId, StructuralType structuralType)
        {
            var ret = await _r3DmSvc.GetStructural(UserAction, userId, structuralType);

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

        [HttpPost("globalquery")]
        [SwaggerOperation(Summary = "Get R3tina Infomation by GlobalQuery")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        [Produces("application/json")]
        public async Task<IActionResult> GetGlobalQuery(QueryParam queryParam)
        {
            var ret = await _r3DmSvc.GetGlobalQuery(UserAction, queryParam);

            if (ret.IsSuccess)
            {
                return Ok(ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(200, resp);
            }
        }

        [HttpGet("cascaderhierarchy")]
        [SwaggerOperation(Summary = "Get Cascader Hierarchy")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetCascaderHierarchy([FromQuery] CascaderFilter cascaderFilter)
        {
            var ret = await _r3DmSvc.GetCascaderHierarchy(UserAction, cascaderFilter);

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

        [HttpGet("cascaderraw")]
        [SwaggerOperation(Summary = "Get Cascader Raw")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetCascaderRaw()
        {
            var ret = await _r3DmSvc.GetCascaderRaw(UserAction);

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

        [HttpGet("cascaderfullstructure")]
        [SwaggerOperation(Summary = "Get Cascader Full Structure (mode => 0: value from id; 1: value from name)")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetCascaderFullStructure([FromQuery] int maxLvl, [FromQuery] int mode, [FromQuery] string? tenantParam = null)
        {
            var ret = await _r3DmSvc.GetCascaderFullStructure(UserAction, maxLvl, mode, tenantParam);

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
