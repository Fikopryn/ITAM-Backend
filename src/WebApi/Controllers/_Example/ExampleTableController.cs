using Core.Helpers;
using Domain.Example.Table;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.Example
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleTableController : CustomControllerBase
    {
        private readonly ITableService _exTblSvc;

        public ExampleTableController(
            ITableService exTblSvc)
        {
            _exTblSvc = exTblSvc;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get All Data")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAllData()
        {
            var ret = await _exTblSvc.GetAllData();

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

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Data By ID")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetById(int id)
        {
            var ret = await _exTblSvc.GetById(id);

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

        [HttpPost]
        [SwaggerOperation(Summary = "Insert Data")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> Create(ExPersonInsertDto data)
        {
            var ret = await _exTblSvc.Create(data);

            if (ret.IsSuccess)
            {
                return Created(ret.Value.Id.ToString(), ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpPost("{headerId}")]
        [SwaggerOperation(Summary = "Insert Detail Data")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> CreateDetail(int headerId, ExPersonIdInsertDto data)
        {
            var ret = await _exTblSvc.CreateDetail(headerId, data);

            if (ret.IsSuccess)
            {
                return Created(ret.Value.Id.ToString(), ret.Value);
            }
            else
            {
                var resp = ResponseHelper.CreateFailResult(ret.Reasons.First().Message);

                return StatusCode(int.Parse(resp.StatusCode), resp);
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update Data")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> Update(ExPersonReadDto data)
        {
            var ret = await _exTblSvc.Update(data);

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

        [HttpPatch("{id}")]
        [SwaggerOperation(Summary = "Update Specific Data")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> UpdateSpecific(int id, string email)
        {
            var ret = await _exTblSvc.UpdateSpecific(id, email);

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
        [SwaggerOperation(Summary = "Delete Specific Data")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            var ret = await _exTblSvc.Delete(id);

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

        [HttpDelete("detail/{id}")]
        [SwaggerOperation(Summary = "Delete Specific Data")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            var ret = await _exTblSvc.DeleteDetail(id);

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
