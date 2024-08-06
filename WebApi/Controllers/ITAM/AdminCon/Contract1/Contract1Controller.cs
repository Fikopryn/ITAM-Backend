using Core.Helpers;
using Core.Models;
using Domain.ITAM.AdminCon.Company;
using Domain.ITAM.AdminCon.Contract;
using Domain.ITAM.ConManCon.Contract;
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

namespace WebApi.Controllers.ITAM.AdminCon.Contract1
{ 
[Route("api/[controller]")]
[ApiController]
public class Contract1Controller : CustomControllerBase
{
    public readonly IContract1Service _contract1Service;
    public readonly UploaderConfig _uploaderConfig;

    public Contract1Controller(IContract1Service contract1Service, IOptions<UploaderConfig> uploaderConfig)
    {
        _contract1Service = contract1Service;
        _uploaderConfig = uploaderConfig.Value;
    }

    [HttpGet("All-Data")]
    [SwaggerOperation(Summary = "Get All Contract")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> GetAllData()
    {
        var ret = await _contract1Service.GetAllContract();

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

    [HttpGet("contractNo")]
    [SwaggerOperation(Summary = "Get Contract Data by Contract No")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> GetContractByContractNo([Required] string contractNo)
    {
        var ret = await _contract1Service.GetContractListByNo(contractNo);

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
    [HttpGet("CompanyId")]
    [SwaggerOperation(Summary = "Get Contract Data by Company Id")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> GetContractByCompanyId([Required] int companyId)
    {
        var ret = await _contract1Service.GetContractListByCompany(companyId);

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
    [HttpGet("ContractOwner")]
    [SwaggerOperation(Summary = "Get Contract Data by Contract Owner")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> GetContractByContractOwner(string contractOwner)
    {
        var ret = await _contract1Service.GetContractListByContractOwner(contractOwner);

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

    [HttpGet("Subject")]
    [SwaggerOperation(Summary = "Get Contract Data by Subject")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> GetContractBySubject(string subject)
    {
        var ret = await _contract1Service.GetContractListBySubject(subject);

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

    [HttpGet("SupplierId")]
    [SwaggerOperation(Summary = "Get Contract Data by Supplier Id")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> GetCintractBySupplierId(int supplierId)
    {
        var ret = await _contract1Service.GetContractListBySupplierId(supplierId);

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
    [SwaggerOperation(Summary = "Update Data Conntract")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> Update(Contract1Dto updateParam)
    {
        var ret = await _contract1Service.Update(updateParam, UserAction);

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
    [SwaggerOperation(Summary = "Create Contract")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> CreateContract(Contract1Dto createParam)
    {
        var ret = await _contract1Service.CreateContract(createParam, UserAction);

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

    [HttpDelete("{Detele Contract}")]
    [SwaggerOperation(Summary = "Delete Contract by COntractNo")]
    [SwaggerResponse(200, "Success")]
    [SwaggerResponse(400, "Bad Request")]
    [SwaggerResponse(500, "Internal Server Error")]
    [ServiceFilter(typeof(TokenValidFilter))]
    public async Task<IActionResult> Delete(string contractNo)
    {
        var ret = await _contract1Service.Delete(contractNo);

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
