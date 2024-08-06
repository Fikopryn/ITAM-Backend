using Core.Helpers;
using Core.Models;
using Domain.R3Framework.R3Mailer;
using Domain.R3Framework.R3port;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.R3Framework
{
    [Route("api/r3/report")]
    [ApiController]
    public class R3portController : CustomControllerBase
    {
        private readonly IR3portService _r3portService;

        public R3portController(
            IR3portService r3portService)
        {
            _r3portService = r3portService;
        }

        [HttpPost("getreport")]
        [SwaggerOperation(Summary = "Get Report, specify supported output format(xlsx & pdf) or default to xlsx")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> SendEmail([FromQuery] R3portInputDto r3PortInputDto)
        {
            var ret = await _r3portService.GetReportFile(UserAction, r3PortInputDto);

            if (ret.IsSuccess)
            {
                object result = File(ret.Value.fileData, ret.Value.fileType, ret.Value.fileName);
                Response.Headers.Add("content-type", ret.Value.fileType);
                return (IActionResult) result;
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

    }
}
