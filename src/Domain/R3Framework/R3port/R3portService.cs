using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.R3Framework.R3port
{
    public class R3portService : IR3portService
    {
        private readonly R3portConfig _r3portConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public R3portService(IOptions<R3portConfig> r3portConfig, IHttpClientFactory httpClientFactory)
        {
            _r3portConfig = r3portConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<R3portOutputDto>> GetReportFile(R3UserSession userAction, R3portInputDto reportParam)
        {
            var url = _r3portConfig.ReportUrl.Replace("@reportOutputType", reportParam.reportOutputType) + reportParam.reportName + ".rptdesign";
            if (reportParam.reportParam != null)
            {
                var base64EncodedBytes = Convert.FromBase64String(reportParam.reportParam);
                var endcodedParameter = Encoding.UTF8.GetString(base64EncodedBytes);
                url = url + endcodedParameter;
            }
            
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "*/*");

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseByte = await response.Content.ReadAsByteArrayAsync();

                R3portOutputDto result = new R3portOutputDto();
                result.fileType = "application/octet-stream";
                result.fileData = responseByte;
                result.fileName = reportParam.reportName + "." + reportParam.reportOutputType;

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("ERROR", "GetReportFile", SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), reportParam), null);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
