using Core.Helpers;
using Core.Models;
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.ITAM.AdminCon.RequestPurchaseOrder;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.ITAM.AdminCon.RequestPurchaseOrder
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestPurchaseOrderController : CustomControllerBase
    {
        public readonly IRequestPurchaseOrderService _requestPurchaseOrderService;

        private readonly UploaderConfig _uploaderConfig;

        public RequestPurchaseOrderController(IRequestPurchaseOrderService requestPurchaseOrderService, IOptions<UploaderConfig> uploaderConfig)
        {
            _requestPurchaseOrderService = requestPurchaseOrderService;
            _uploaderConfig = uploaderConfig.Value;
        }

        [HttpGet("All-Data")]
        [SwaggerOperation(Summary = "Get All RequestPurchaseOrder")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAllData()
        {
            var ret = await _requestPurchaseOrderService.GetAllRequestPurchaseOrder();

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

        [HttpGet("RpoNo")]
        [SwaggerOperation(Summary = "Get Request Purchase Order Data by Rpo No")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetRequestPurchaseOrderByRpoNo([Required] string rpoNo)
        {
            var ret = await _requestPurchaseOrderService.GetRequestPurchaseOrderByRpoNo(rpoNo);

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

        [HttpGet("ContractNo")]
        [SwaggerOperation(Summary = "Get Request Purchase Order Data by Contract No")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetRequestPurchaseOrderByContractNo([Required] string contractNo)
        {
            var ret = await _requestPurchaseOrderService.GetRequestPurchaseOrderByContractNo(contractNo);

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

        [HttpGet("RpoSubject")]
        [SwaggerOperation(Summary = "Get Request Purchase Order Data by Rpo Subject")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetRequestPurchaseOrderByRpoSubject([Required] string rpoSubject)
        {
            var ret = await _requestPurchaseOrderService.GetRequestPurchaseOrderByRpoNo(rpoSubject);

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
        [SwaggerOperation(Summary = "Update Data Request Purchase Order")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> UpdateRequestPurchaseOrder(RequestPurchaseOrderDto Data)
        {
            var ret = await _requestPurchaseOrderService.Update(Data, UserAction);

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
        [SwaggerOperation(Summary = "Create Request Purchase Order")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> CreatePurchaseRequestOrder(RequestPurchaseOrderDto createParam)
        {
            var ret = await _requestPurchaseOrderService.CreateRequestPurchaseOrder(createParam, UserAction);

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