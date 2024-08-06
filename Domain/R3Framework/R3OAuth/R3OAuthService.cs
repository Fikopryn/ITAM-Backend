using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.InAppSession;
using Domain.R3Framework.R3DataManagement;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace Domain.R3Framework.R3OAuth
{
    public class R3OAuthService : IR3OAuthService
    {
        private readonly R3OAuthConfig _r3OAuthConfig;
        private readonly R3tinaConfig _r3tinaConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IInAppSessionService _inAppSessionSvc;

        public R3OAuthService(
            IOptions<R3OAuthConfig> r3OAuthConfig,
            IOptions<R3tinaConfig> r3tinaConfig,
            IHttpClientFactory httpClientFactory,
            IInAppSessionService inAppSessionService)
        {
            _r3OAuthConfig = r3OAuthConfig.Value;
            _r3tinaConfig = r3tinaConfig.Value;
            _httpClientFactory = httpClientFactory;
            _inAppSessionSvc = inAppSessionService;
        }
        public async Task<Result<R3OauthLoginResponse>> Login(R3OAuthLogin r3OAuthLogin, string ipAddress, bool encoded = true)
        {
            var url = _r3OAuthConfig.AuthServerUrl.Replace("RealmName", _r3OAuthConfig.RealmName) + "token";
            try
            {
                var body = new List<KeyValuePair<string, string>>();
                
                if (encoded)
                {
                    body = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("client_id", _r3OAuthConfig.ClientId),
                        new KeyValuePair<string, string>("client_secret", _r3OAuthConfig.ClientSecret),
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", r3OAuthLogin.username),
                        new KeyValuePair<string, string>("encpass", r3OAuthLogin.password),
                        new KeyValuePair<string, string>("sysname", _r3tinaConfig.AppName)
                    };
                } else
                {
                    body = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("client_id", _r3OAuthConfig.ClientId),
                        new KeyValuePair<string, string>("client_secret", _r3OAuthConfig.ClientSecret),
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", r3OAuthLogin.username),
                        new KeyValuePair<string, string>("password", r3OAuthLogin.password),
                        new KeyValuePair<string, string>("sysname", _r3tinaConfig.AppName)
                    };
                }
                

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new FormUrlEncodedContent(body)
                };

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3OauthLoginResponse>(responseString);

                var insertResult = await _inAppSessionSvc.Save(new InAppSessionDto
                {
                    ExpiredDate = DateTime.Now.Date.AddDays(_r3OAuthConfig.ExpiredInDays),
                    IpAddress = ipAddress,
                    RefreshToken = retResp.refresh_token
                });
                Log.Information($"LoginSuccess {retResp.users.username}");
                Log.Information("{@LogData}", retResp.users);
                //Log.Information("{@LogData}", LogData.BuildInformationLog(LogType.Authorization, retResp.users.username, SystemHelper.GetActualAsyncMethodName(), "Authorization success!"));
                //Log.Information("{@LogData}", LogData.BuildInformationLog(LogType.Authorization, retResp.users.username, SystemHelper.GetActualAsyncMethodName(), "Authorization success!", body));
                //Log.Information("{@LogData}", LogData.BuildInformationLog(LogType.Authorization, retResp.users.username, SystemHelper.GetActualAsyncMethodName(), "Authorization success!", insertResult));
                //Log.Error("{@LogData}", LogData.BuildErrorLog(LogType.Authorization, retResp.users.username, SystemHelper.GetActualAsyncMethodName(), "Authorization success!"));
                //Log.Error("{@LogData}", LogData.BuildErrorLog(LogType.Authorization, retResp.users.username, SystemHelper.GetActualAsyncMethodName(), "Authorization success!", body));
                //Log.Error("{@LogData}", LogData.BuildErrorLog(LogType.Authorization, retResp.users.username, SystemHelper.GetActualAsyncMethodName(), "Authorization success!", insertResult));

                return Result.Ok(retResp);
            } catch (Exception ex)
            {
                Log.Information($"LoginFail");
                Log.Error("{@LogData}", LogData.BuildErrorLog("", SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url, ex));
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<R3OauthLoginResponse>> RefreshToken(R3OAuthLogin r3OAuthLogin, string ipAddress)
        {
            var url = _r3OAuthConfig.AuthServerUrl.Replace("RealmName", _r3OAuthConfig.RealmName) + "token";
            try
            {
                var requestorCheck = await _inAppSessionSvc.FindByRefreshToken(r3OAuthLogin.refresh_token);
                if (requestorCheck.IsSuccess)
                {
                    var sameIpAddress = requestorCheck.Value.IpAddress == ipAddress;
                    var sessionExpired = requestorCheck.Value.ExpiredDate > DateTime.Now.Date;
                    if (sameIpAddress && sessionExpired)
                    {
                        var body = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("client_id", _r3OAuthConfig.ClientId),
                            new KeyValuePair<string, string>("client_secret", _r3OAuthConfig.ClientSecret),
                            new KeyValuePair<string, string>("grant_type", "refresh_token"),
                            new KeyValuePair<string, string>("refresh_token", r3OAuthLogin.refresh_token)
                        };

                        var request = new HttpRequestMessage(HttpMethod.Post, url)
                        {
                            Content = new FormUrlEncodedContent(body)
                        };

                        var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();

                        var responseString = await response.Content.ReadAsStringAsync();
                        var retResp = JsonConvert.DeserializeObject<R3OauthLoginResponse>(responseString);

                        var insertResult = await _inAppSessionSvc.Save(new InAppSessionDto
                        {
                            ExpiredDate = requestorCheck.Value.ExpiredDate,
                            IpAddress = ipAddress,
                            RefreshToken = retResp.refresh_token
                        });

                        Log.Information("{@LogData}", LogData.BuildInformationLog(LogType.Authorization, ipAddress, SystemHelper.GetActualAsyncMethodName(), "Refresh token updated!", r3OAuthLogin));

                        return Result.Ok(retResp);
                    }
                    else
                    {
                        var errMsg = "";
                        if (!sameIpAddress)
                        {
                            errMsg += "Different IpAddress";
                        }

                        if (!sessionExpired)
                        {
                            errMsg += errMsg.Length > 1 ? " & Refresh Token Expired" : "Refresh Token Expired";
                        }
                        Log.Error("{@LogData}", LogData.BuildErrorLog("ERROR", ipAddress, SystemHelper.GetActualAsyncMethodName(), errMsg, r3OAuthLogin));
                        return Result.Fail(ResponseStatusCode.Unauthorized + ":" + errMsg);
                    }
                }
                else
                {
                    Log.Error("{@LogData}", LogData.BuildErrorLog("ERROR", ipAddress, SystemHelper.GetActualAsyncMethodName(), "Refresh Token is not valid", r3OAuthLogin));
                    return Result.Fail(ResponseStatusCode.Unauthorized + ":" + "Refresh Token is not valid");
                }
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("", SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<R3UserSession>> Introspect(R3OAuthIntrospect r3OAuthIntrospect)
        {
            var url = _r3OAuthConfig.AuthServerUrl.Replace("RealmName", _r3OAuthConfig.RealmName) + "token/introspect";
            try
            {
                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _r3OAuthConfig.ClientId),
                    new KeyValuePair<string, string>("client_secret", _r3OAuthConfig.ClientSecret),
                    new KeyValuePair<string, string>("token", r3OAuthIntrospect.token)
                };

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new FormUrlEncodedContent(body)
                };

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<R3UserSession>(responseString);

                Log.Debug("{@LogData}", LogData.BuildInformationLog(LogType.Authorization, retResp.Username, SystemHelper.GetActualAsyncMethodName(), "Introspect success!", body));

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("", SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<string>> Logout(R3OAuthLogout r3OAuthLogout)
        {
            var url = _r3OAuthConfig.AuthServerUrl.Replace("RealmName", _r3OAuthConfig.RealmName) + "logout";
            try
            {
                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _r3OAuthConfig.ClientId),
                    new KeyValuePair<string, string>("client_secret", _r3OAuthConfig.ClientSecret),
                    new KeyValuePair<string, string>("refresh_token", r3OAuthLogout.refresh_token)
                };

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new FormUrlEncodedContent(body)
                };
                request.Headers.Add("Authorization", r3OAuthLogout.access_token);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                Log.Information("{@LogData}", LogData.BuildInformationLog(LogType.Authorization, responseString, SystemHelper.GetActualAsyncMethodName(), "Logout success!", r3OAuthLogout));

                return Result.Ok(responseString);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("", SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        
    }
}
