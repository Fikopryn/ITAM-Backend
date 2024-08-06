using Core.Models;
using Domain.R3Framework.R3OAuth;
using Infrastructure.Customs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Options;
using Serilog;
using Core.Helpers;
using Core;

namespace Infrastructure.Filters
{
    public class IpAddressFilter : IAsyncActionFilter
    {
        public IpAddressFilter()
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
            Log.Information("{@LogData}", LogData.BuildInformationLog(LogType.Authorization, ipAddress.ToString(), SystemHelper.GetActualAsyncMethodName(), "Logged IpAddress:" + ipAddress));
            ((CustomControllerBase)context.Controller).IpAddress = ipAddress.ToString();
            await next();
        }
    }
}
