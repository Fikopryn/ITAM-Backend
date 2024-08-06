using Core.Helpers;
using Core.Models;
using Domain.ITAM.AdminCon.AssetStatusFin;
using Domain.Example.FileProcessing;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.ITAM.AdminCon.AssetStatusFin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetStatusFinController : CustomControllerBase
    {
        public readonly IAssetStatusFinService _assetStatusFinService;
        private readonly UploaderConfig _uploaderConfig;

        public AssetStatusFinController(IAssetStatusFinService assetStatusFinService, IOptions<UploaderConfig> uploaderConfig)
        {
            _assetStatusFinService = assetStatusFinService;
            _uploaderConfig = uploaderConfig.Value;
        }

        [HttpGet("All-Data")]
        [SwaggerOperation(Summary = "Get All AssetStatusFin")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAllData()
        {
            var ret = await _assetStatusFinService.GetAllAssetStatusFin();

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
        [SwaggerOperation(Summary = "Get Asset Status Finance Data by asset Id")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAssetStatusFinById([Required] int assetId)
        {
            var ret = await _assetStatusFinService.GetAssetStatusFinById(assetId);

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

        [HttpGet("Id1")]
        [SwaggerOperation(Summary = "Get Asset Status Finance Data by asset Id1")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAssetStatusFinId1([Required] string assetStatusFinId1)
        {
            var ret = await _assetStatusFinService.GetAssetStatusFinByFin1(assetStatusFinId1);

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

        [HttpGet("Desc")]
        [SwaggerOperation(Summary = "Get Asset Status Finance Data by asset desc")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAssetStatusFinDesc([Required] string assetStatusFinDesc)
        {
            var ret = await _assetStatusFinService.GetAssetStatusFinByDesc(assetStatusFinDesc);

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
        [SwaggerOperation(Summary = "Update Data Asset Status Fin")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> Update(AssetStatusFinDto Data)
        {
            var ret = await _assetStatusFinService.Update(Data, UserAction);

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
        [SwaggerOperation(Summary = "Create AssetStatusFin")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> CreateAssetStatusFin(AssetStatusFinDto createParam)
        {
            var ret = await _assetStatusFinService.CreateAssetStatusFin ( createParam, UserAction);

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
        [HttpDelete("{DeleteAssetStatusFin}")]
        [SwaggerOperation(Summary = "Delete Asset Status Fin By Id")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> Delete(int assetStatusFinId)
        {
            var ret = await _assetStatusFinService.Delete(assetStatusFinId);

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
