using Core.Helpers;
using Core.Models;
using Domain.Example.PageView;
using Infrastructure.Customs;
using Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.Example
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplePageViewController : CustomControllerBase
    {
        private readonly IPageViewService _exVwSvc;

        public ExamplePageViewController(
            IPageViewService exVwSvc)
        {
            _exVwSvc = exVwSvc;
        }

        [HttpPost("page")]
        [SwaggerOperation(Summary = "Get Page Data from View")]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        [ServiceFilter(typeof(TokenValidFilter))]
        public async Task<IActionResult> GetPageData(PagingRequest pRequest)
        {
            var ret = await _exVwSvc.GetPageData(pRequest);

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
