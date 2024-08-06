using Core;
using Core.Extensions;
using Core.Helpers;
using Core.Models;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Text;

namespace Domain.R3Framework.R3Mailer
{
    public class R3Mailer : IR3Mailer
    {
        private readonly R3tinaConfig _r3Config;
        private readonly R3OAuthConfig _r3OAuthConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public R3Mailer(
            IOptions<R3tinaConfig> r3Config,
            IOptions<R3OAuthConfig> r3OAuthConfig,
            IHttpClientFactory httpClientFactory)
        {
            _r3Config = r3Config.Value;
            _r3OAuthConfig = r3OAuthConfig.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<MailerResponse>> SendEmail(R3UserSession userAction, MailerPayload emailPayload)
        {
            var url = _r3Config.R3Mailer + "send";

            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(emailPayload, serializerSettings), Encoding.UTF8, "application/json")
                };

                request.Headers.Add("Authorization", userAction.Token);
                request.Headers.Add("client-id", _r3OAuthConfig.ClientId);
                request.Headers.Add("client-secret", _r3OAuthConfig.ClientSecret);
                request.Headers.Add("realm-name", _r3OAuthConfig.RealmName);

                var client = _httpClientFactory.CreateClient(HttpClientName.R3Client);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var retResp = JsonConvert.DeserializeObject<MailerResponse>(responseString);

                return Result.Ok(retResp);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog(userAction.Username, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), url, emailPayload), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":Send Email Error. " + ex.GetMessage());
            }
        }
    }
}
