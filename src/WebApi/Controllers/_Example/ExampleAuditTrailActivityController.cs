using Core.Helpers;
using Core.Models;
using Domain._Example.AuditTrailActivity;
using Domain.Example.FileProcessing;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace WebApi.Controllers.Example
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleAuditTrailActivityController : CustomControllerBase
    {
        public readonly IAuditTrailActivityService _auditTrailActivityService;

        public ExampleAuditTrailActivityController(IAuditTrailActivityService auditTrailActivityService)
        {
            _auditTrailActivityService = auditTrailActivityService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get Audit Trail Activity Data by Module Name & Module ID")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAuditTrailActivity([Required] string modulName, [Required] Guid modulId)
        {
            var ret = await _auditTrailActivityService.GetAuditTrailActivityList(modulName, modulId);

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
