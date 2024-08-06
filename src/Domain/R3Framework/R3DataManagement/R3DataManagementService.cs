using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Data;
using System.Text;

namespace Domain.R3Framework.R3DataManagement
{
    public class R3DataManagementService : IR3DataManagementService
    {
        private readonly R3tinaConfig _r3Config;
        private readonly R3OAuthConfig _r3OAuthConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public R3DataManagementService(
            IOptions<R3tinaConfig> r3Config,
            IOptions<R3OAuthConfig> r3OAuthConfig,
            IHttpClientFactory httpClientFactory)
        {
            _r3Config = r3Config.Value;
            _r3OAuthConfig = r3OAuthConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<R3AppUserData>> GetUserAppData(R3UserSession userAction, string userId, string roles = "")
        {
            var user = userId != null ? userId : userAction.Username;
            var url = _r3Config.R3DataManagement + $"getUserSessionRest/session/{user}/{_r3Config.AppName}";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);
                request.Headers.Add("x-user-role", roles); // for impersonate purpose

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3AppUserData>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public bool GetImpersonateCapabilty(R3AppUserData r3AppUserData)
        {
            if (r3AppUserData.RoleInformation != null)
            {
                foreach (R3AppRole r3AppRole in r3AppUserData.RoleInformation.Role)
                {
                    if (r3AppRole.SysRole.ToLower() == "admin")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        //public async Task<Result<R3AppUserData>> GetUserImpersonate(R3UserSession userAction, string userId)
        //{
        //    var url = _r3Config.R3DataManagement + $"getUserSessionRest/session/{userId}/{_r3Config.AppName}";

        //    try
        //    {
        //        var request = new HttpRequestMessage(HttpMethod.Get, url);
        //        request.Headers.Add("Authorization", userAction.Token);
        //        request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
        //        request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
        //        request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);

        //        var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();

        //        var responseString = await response.Content.ReadAsStringAsync();
        //        var retResp = JsonConvert.DeserializeObject<R3AppUserData>(responseString);

        //        return Result.Ok(retResp);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
        //        return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
        //    }
            
        //}

        public async Task<Result<R3EmployeeInfo>> GetEmployee(R3UserSession userAction, string userId, string roles = "")
        {
            var url = _r3Config.R3DataManagement + $"getEmployeeRest/employee/{userId}";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);
                request.Headers.Add("x-user-role", roles); // for impersonate purpose

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3Employees>(responseString);

                return Result.Ok(retResp.Employee.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<R3Structurals>> GetStructural(R3UserSession userAction, string userId, StructuralType structuralType, string roles = "")
        {
            var url = _r3Config.R3DataManagement + $"getStructuralRest/structural";

            var payload = new R3StructuralRequest()
            {
                USERID = userId.ToUpper(),
                STRUCTURALTYPE = structuralType.ToString().ToUpper()
            };

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json")
                };

                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);
                request.Headers.Add("x-user-role", roles); // for impersonate purpose

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3Structurals>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url, payload), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        public async Task<Result<List<Dictionary<string, string>>>> GetGlobalQuery(R3UserSession userAction, QueryParam queryParam)
        {
            var url = _r3Config.R3DataManagement + $"globalQueryRest/query";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(queryParam), Encoding.UTF8, "application/json")
                };
                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, Dictionary<string, string>>>>>(responseString);
                if (retResp != null)
                {
                    return Result.Ok(retResp["resultData"].SelectMany(a => a.Values).ToList());
                }
                else
                {
                    return Result.Fail("No Result");
                }
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url, queryParam), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        public async Task<Result<CascaderResult>> GetCascaderHierarchy(R3UserSession userAction, CascaderFilter cascaderFilter)
        {
            var url = _r3Config.R3tinaApi + $"Lov/GetCascaderPositionByFilter?";

            if (cascaderFilter.Division !=  null)
            {
                url += "Division=" + cascaderFilter.Division;
            }
            if (cascaderFilter.Subdivision != null)
            {
                url += "&Subdivision=" + cascaderFilter.Subdivision;
            }
            if (cascaderFilter.Department != null)
            {
                url += "&Department=" + cascaderFilter.Department;
            }
            url += "&Level=" + cascaderFilter.Level;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("token", userAction.Token.Replace("Bearer ", ""));
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<CascaderResult>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
            //var url = _r3Config.R3tinaApi + $"Lov/GetCascaderPositionByFilter";

            //try
            //{
            //    var request = new HttpRequestMessage(HttpMethod.Get, url);
            //    request.Headers.Add("token", userAction.Token.Replace("Bearer ", ""));
            //    request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
            //    request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
            //    request.Headers.Add("realm", _r3OAuthConfig.RealmName);

            //    var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
            //    var response = await client.SendAsync(request);
            //    response.EnsureSuccessStatusCode();

            //    var responseString = await response.Content.ReadAsStringAsync();
            //    //var retResp = JsonConvert.DeserializeObject<List<Cascader>>(responseString);
            //    if (responseString != null)
            //    {
            //        return Result.Ok(new List<Cascader>());
            //    }
            //    else
            //    {
            //        return Result.Ok(new List<Cascader>());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url, cascaderFilter), ex);
            //    return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            //}
        }
        public async Task<Result<CascaderRawResult>> GetCascaderRaw(R3UserSession userAction)
        {
            var url = _r3Config.R3tinaApi + $"Lov/GetR3tinaCascaderRaw";
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("token", userAction.Token.Replace("Bearer ", ""));
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<CascaderRawResult>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<string>> GetCascaderFullStructure(R3UserSession userAction, int maxLvl, int mode, string tenantParam)
        {
            var url = _r3Config.R3tinaApi + $"Lov/GetR3tinaCascaderFullStructure?mode=" + mode + (maxLvl != 0 ? "&maxLvl=" + maxLvl : "") + (tenantParam != null ? "&tenantParam=" + tenantParam : "");
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("token", userAction.Token.Replace("Bearer ", ""));
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JObject.Parse(responseString);//JsonConvert.DeserializeObject<JsonObject>(responseString);

                return Result.Ok(responseString);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
