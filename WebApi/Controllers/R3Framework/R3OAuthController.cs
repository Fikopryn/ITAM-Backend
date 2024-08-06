using Core.Helpers;
using Domain.R3Framework.R3OAuth;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.R3Framework
{
    [Route("api/r3/oauth")]
    [ApiController]
    public class R3OAuthController : CustomControllerBase
    {
        private readonly IR3OAuthService _r3OauthService;

        public R3OAuthController (IR3OAuthService r3OauthService)
        {
            _r3OauthService = r3OauthService;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "User Login")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(IpAddressFilter))]
        //[Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> Login(R3OAuthLogin data)
        { 
            /*R3OAuthLogin r3OAuthLogin = JsonConvert.DeserializeObject<R3OAuthLogin>(data)*/;
            var ret = await _r3OauthService.Login(data, IpAddress);

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

        [HttpPost("logindecoded")]
        [SwaggerOperation(Summary = "User Login Decoded")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(IpAddressFilter))]
        //[Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> LoginDecoded(R3OAuthLogin data)
        {
            /*R3OAuthLogin r3OAuthLogin = JsonConvert.DeserializeObject<R3OAuthLogin>(data)*/
            ;
            var ret = await _r3OauthService.Login(data, IpAddress, false);

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

        [HttpPost("refresh_token")]
        [SwaggerOperation(Summary = "Refresh Token")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(IpAddressFilter))]
        public async Task<IActionResult> RefreshToken(R3OAuthLogin data)
        {
            var ret = await _r3OauthService.RefreshToken(data, IpAddress);

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
        [HttpPost("introspect")]
        [SwaggerOperation(Summary = "Check Token")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> Introspect(R3OAuthIntrospect data)
        {
            var ret = await _r3OauthService.Introspect(data);

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
        [HttpPost("logout")]
        [SwaggerOperation(Summary = "Logout")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> Logout(R3OAuthLogout data)
        {
            var ret = await _r3OauthService.Logout(data);

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
