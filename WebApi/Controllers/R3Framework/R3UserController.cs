using Core.Helpers;
using Domain.R3Framework.R3DataManagement;
using Domain.R3Framework.R3User;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.R3Framework
{
    [Route("api/r3/user")]
    [ApiController]
    public class R3UserController : CustomControllerBase
    {
        private readonly IR3UserService _r3UserSvc;
        private readonly IR3DataManagementService _r3DmSvc;

        public R3UserController(
            IR3UserService r3UserSvc,
            IR3DataManagementService r3DmSvc)
        {
            _r3UserSvc = r3UserSvc;
            _r3DmSvc = r3DmSvc;
        }

        //[HttpPost("login")]
        //[SwaggerOperation(Summary = "User Login")]
        //[SwaggerResponse(200, "Success")]
        //[SwaggerResponse(500, "Internal Server Error")]
        //public async Task<IActionResult> Login(R3UserLogin data)
        //{
        //    var ret = await _r3UserSvc.AuthLogin(data);

        //    if (ret.IsSuccess)
        //    {
        //        return Ok(ret.Value);
        //    }
        //    else
        //    {
        //        var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //        return StatusCode(int.Parse(resp.StatusCode), resp);
        //    }
        //}

        [HttpPost("impersonate")]
        [SwaggerOperation(Summary = "Impersonate User")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> ImpersonateUser(R3UserImpersonate data)
        {
            var userAppData = await _r3DmSvc.GetUserAppData(UserAction, UserAction.Username);
            var canImpersonate = _r3DmSvc.GetImpersonateCapabilty(userAppData.Value);

            if (canImpersonate)
            {
                string roles = "";
                foreach (R3AppRole r3AppRole in userAppData.Value.RoleInformation.Role)
                {
                    roles += r3AppRole.SysRole + ",";
                }
                if (roles.Length > 0) { roles = roles.Substring(0, roles.Length - 1); }
                var ret = await _r3DmSvc.GetUserAppData(UserAction, data.UserId, roles);

                if (ret.IsSuccess && ret.Value.UserInformation != null)
                {
                    if (ret.Value.RoleInformation == null)
                    {
                        ret.Value.RoleInformation = new R3AppRoleInfo();
                    }
                    ret.Value.RoleInformation.Role.AddRange(userAppData.Value.RoleInformation.Role);
                    return Ok(ret.Value);
                }
                else if (ret.IsSuccess && ret.Value.UserInformation == null)
                {
                    var resp = ResponseHelper.CreateFailResult("Invalid data access");

                    return StatusCode(int.Parse(resp.StatusCode), resp);
                }
                else
                {
                    var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                    return StatusCode(int.Parse(resp.StatusCode), resp);
                }
            }
            return StatusCode(Int16.Parse(ResponseStatusCode.Unauthorized), ResponseStatusCode.Unauthorized + ": Doesn't have authorization to impersonate");
        }

        //[HttpGet("logout")]
        //[SwaggerOperation(Summary = "User Logout")]
        //[SwaggerResponse(200, "Success")]
        //[SwaggerResponse(401, "Unauthorized")]
        //[SwaggerResponse(500, "Internal Server Error")]
        //[ServiceFilter(typeof(TokenValidFilter))]
        //public async Task<IActionResult> Logout()
        //{
        //    var ret = await _r3UserSvc.AuthLogout(UserAction);

        //    if (ret.IsSuccess)
        //    {
        //        return Ok(ret.Value);
        //    }
        //    else
        //    {
        //        var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //        return StatusCode(int.Parse(resp.StatusCode), resp);
        //    }
        //}

        //[HttpGet("getusersession")]
        //[SwaggerOperation(Summary = "Get User Session")]
        //[SwaggerResponse(200, "Success")]
        //[SwaggerResponse(401, "Unauthorized")]
        //[SwaggerResponse(500, "Internal Server Error")]
        //[ServiceFilter(typeof(TokenValidFilter))]
        //public async Task<IActionResult> GetUserSession()
        //{
        //    var ret = await _r3UserSvc.GetUserSession(UserAction);

        //    if (ret.IsSuccess)
        //    {
        //        ret.Value.Token = HeaderAuthorization;

        //        return Ok(ret.Value);
        //    }
        //    else
        //    {
        //        var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

        //        return StatusCode(int.Parse(resp.StatusCode), resp);
        //    }
        //}

        //[HttpGet("tokenvalidation")]
        //[SwaggerOperation(Summary = "User Token Validation")]
        //[SwaggerResponse(200, "Success")]
        //[SwaggerResponse(401, "Unauthorized")]
        //[SwaggerResponse(500, "Internal Server Error")]
        //[ServiceFilter(typeof(TokenValidFilter))]
        //public async Task<IActionResult> TokenValidation()
        //{
        //    var ret = await _r3UserSvc.TokenValidation(UserAction);

        //    return Ok(ret.Value);
        //}
    }
}
