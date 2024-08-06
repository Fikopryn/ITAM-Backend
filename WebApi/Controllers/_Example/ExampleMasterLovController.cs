using Core.Helpers;
using Core.Models;
using Domain._Example.AuditTrailActivity;
using Domain._Example.MasterLov;
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
    public class ExampleMasterLovController : CustomControllerBase
    {
        private readonly IMasterLovService _mlSvc;

        public ExampleMasterLovController(IMasterLovService mlSvc)
        {
            _mlSvc = mlSvc;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Master Lov Data")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAllData()
        {
            var ret = await _mlSvc.GetAll();

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

        [HttpPost("page")]
        [SwaggerOperation(Summary = "Get All Master Lov Data Paged")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAllDataPAged(PagingRequest pagingRequest)
        {
            var ret = await _mlSvc.GetAllPaged(pagingRequest);

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

        [HttpGet("getalllovnamedistinct")]
        [SwaggerOperation(Summary = "Get All Lov Name Distinct")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAllLovNameDistinct()
        {
            var ret = await _mlSvc.GetAllLovNameDistinct();

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

        [HttpGet("{lovName}")]
        [SwaggerOperation(Summary = "Get All Master Lov Data by LovName")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetByName(string lovName)
        {
            var ret = await _mlSvc.GetByName(lovName);

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

        [HttpGet("getbyid")]
        [SwaggerOperation(Summary = "Get Master Lov Data by Lovid")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetLovById([FromQuery] decimal lovid)
        {
            var ret = await _mlSvc.GetLovById(UserAction, lovid);

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
        [SwaggerOperation(Summary = "Create Master Lov")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> CreateLov(MasterLovDto createParam)
        {
            var ret = await _mlSvc.CreateLov(UserAction, createParam);

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

        [HttpPost("update")]
        [SwaggerOperation(Summary = "Update Master Lov")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> UpdateLov(MasterLovDto updateParam)
        {
            var ret = await _mlSvc.UpdateLov(UserAction, updateParam);

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

        [HttpPost("delete")]
        [SwaggerOperation(Summary = "Delete Master Lov")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> DeleteLov(decimal lovId)
        {
            var ret = await _mlSvc.DeleteLov(UserAction, lovId);

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
