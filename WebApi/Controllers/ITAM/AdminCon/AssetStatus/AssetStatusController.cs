using Core.Helpers;
using Core.Models;
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.Example.FileProcessing;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.ITAM.AdminCon.AssetStatus
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetStatusController : CustomControllerBase
    {
        public readonly IAssetStatusService _assetStatusService;
        private readonly UploaderConfig _uploaderConfig;

        public AssetStatusController(IAssetStatusService assetStatusService, IOptions<UploaderConfig> uploaderConfig)
        {
            _assetStatusService = assetStatusService;
            _uploaderConfig = uploaderConfig.Value;
        }


        [HttpGet("All-Data")]
        [SwaggerOperation(Summary = "Get All AssetStatus")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAllData()
        {
            var ret = await _assetStatusService.GetAllAssetStatus();

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

        [HttpGet("Id")]
        [SwaggerOperation(Summary = "Get Asset Status Data by Asset Id")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAssetStatus([Required] int assetStatusId)
        {
            var ret = await _assetStatusService.GetAssetStatusList(assetStatusId);

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

        [HttpGet("Status 1")]
        [SwaggerOperation(Summary = "Get Asset Status Data by ASset Status 1")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAssetStatusByStatus1([Required] string assetStatus1)
        {
            var ret = await _assetStatusService.GetAssetStatusListByStatus1(assetStatus1);

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

        [HttpGet("Description")]
        [SwaggerOperation(Summary = "Get Asset Status Data by Asset Desc")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAssetStatusByDesc([Required] string assetStatusDescription)
        {
            var ret = await _assetStatusService.GetAssetStatusListByDescription(assetStatusDescription);

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

        [HttpPut("Update")]
        [SwaggerOperation(Summary = "Update D3ata Asset Status")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> UpdateAssetStatus(AssetStatusDto Data)
        {
            var ret = await _assetStatusService.Update(Data, UserAction);

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

        [HttpPost("create")]
        [SwaggerOperation(Summary = "Create AssetStatus")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> CreateAssetStatus(AssetStatusDto createParam)
        {
            var ret = await _assetStatusService.CreateAssetStatus (createParam,UserAction);

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

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete Asset Status")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> Delete(int assetStatusId)
        {
            var ret = await _assetStatusService.Delete(assetStatusId);

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
