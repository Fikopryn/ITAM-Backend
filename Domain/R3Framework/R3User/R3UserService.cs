using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using FluentResults;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace Domain.R3Framework.R3User
{
    public class R3UserService : IR3UserService
    {
        private readonly R3tinaConfig _r3Config;
        private readonly IHttpClientFactory _httpClientFactory;

        public R3UserService(
            IOptions<R3tinaConfig> r3Config,
            IHttpClientFactory httpClientFactory)
        {
            _r3Config = r3Config.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<R3UserAuth>> AuthLogin(R3UserLogin data)
        {
            var url = _r3Config.R3OAuth + "auth/login";

            try
            {
                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username", data.UserName),
                    new KeyValuePair<string, string>("password", data.Password)
                };

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new FormUrlEncodedContent(body)
                };

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3UserAuth>(responseString);

                //Log.Information("{@LogData}", LogData.BuildInformationLog(LogType.Authorization, data.UserName, SystemHelper.GetActualAsyncMethodName(), "Authorization success!"));

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("", SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<R3UserLogout>> AuthLogout(R3UserSession userAction)
        {
            var url = _r3Config.R3OAuth + "auth/logout";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", userAction.Token);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3UserLogout>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<R3UserSession>> GetUserSession(string token)
        {
            var url = _r3Config.R3OAuth + "auth/user";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", token);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3UserSession>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("", SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<R3UserSession>> GetUserSession(R3UserSession userAction)
        {
            var url = _r3Config.R3OAuth + "auth/user";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", userAction.Token);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3UserSession>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<bool>> TokenValidation(R3UserSession userAction)
        {
            var url = _r3Config.R3OAuth + "auth/user";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", userAction.Token);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3UserSession>(responseString);

                return Result.Ok(true);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Ok(false);
            }
        }
    }
}
