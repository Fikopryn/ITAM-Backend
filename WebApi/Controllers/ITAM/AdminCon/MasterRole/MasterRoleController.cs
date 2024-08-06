using Core.Helpers;
using Core.Models;
using Domain.ITAM.AdminCon.MasterRole;
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
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.ITAM.AdminCon.Company;


namespace WebApi.Controllers.ITAM.AdminCon.MasterRole
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterRoleController : CustomControllerBase
    {
        public readonly IMasterRoleService _masterRoleService;

        public MasterRoleController(IMasterRoleService masterRoleService)
        {
            _masterRoleService = masterRoleService;
        }

        [HttpGet("All-Data")]
        [SwaggerOperation(Summary = "Get All Master Role")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetAllData()
        {
            var ret = await _masterRoleService.GetAllMasterRole();

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

        [HttpGet("Role Id")]
        [SwaggerOperation(Summary = "Get Master Role Data by Role Id")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetMasterRole([Required] int roleId)
        {
            var ret = await _masterRoleService.GetMasterRoleList(roleId);

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

        [HttpGet("Role Name")]
        [SwaggerOperation(Summary = "Get Master Role Data by Role NAme")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetMasterRoleByName ([Required] string roleName)
        {
            var ret = await _masterRoleService.GetMasterRoleByName(roleName);

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

        [HttpGet("Role Desc")]
        [SwaggerOperation(Summary = "Get Master Role Data by Role Desc")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetMasterRole([Required] string roleDescription)
        {
            var ret = await _masterRoleService.GetMasterRoleByDesc (roleDescription);

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
        [SwaggerOperation(Summary = "Update Data Master Role")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> UpdateMasterRole (MasterRoleDto updateParam)
        {
            var ret = await _masterRoleService.Update(updateParam, UserAction);

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
        [SwaggerOperation(Summary = "Create Master Role")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> CreateMasterRole(MasterRoleDto createParam)
        {
            var ret = await _masterRoleService.CreateMasterRole(createParam, UserAction);

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
        [HttpDelete("{Delete}")]
        [SwaggerOperation(Summary = "Delete Master Role")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> Delete(int roleId)
        {
            var ret = await _masterRoleService.Delete(roleId);

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
