using Core.Models;
using Domain.R3Framework.R3OAuth;
using Infrastructure.Customs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Options;
using Core.Helpers;

namespace Infrastructure.Filters
{
    public class TokenValidFilter : IAsyncActionFilter
    {
        private readonly IR3OAuthService _r3OAuthSvc;
        private readonly R3OAuthConfig _r3OAuthConfig;

        public TokenValidFilter(
            IR3OAuthService r3OAuthService,
            IOptions<R3OAuthConfig> r3OAuthConfig)
        {
            _r3OAuthSvc = r3OAuthService;
            _r3OAuthConfig = r3OAuthConfig.Value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken);
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress;

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                var userCheck = await _r3OAuthSvc.Introspect(new R3OAuthIntrospect { client_id = _r3OAuthConfig.ClientId, client_secret = _r3OAuthConfig.ClientSecret, token = authToken.ToString().Split("Bearer ")[1] });

                if (userCheck.IsSuccess && userCheck.Value.active == true)
                {
                    context.HttpContext.Request.Headers.TryGetValue("ImpersonateAs", out StringValues impersonateAs);

                    if (impersonateAs.Any())
                    {
                        userCheck.Value.ImpersonateAs = impersonateAs.FirstOrDefault();
                    }

                    userCheck.Value.Token = authToken;
                    userCheck.Value.Username = userCheck.Value.Username.ToUpper();

                    ((CustomControllerBase)context.Controller).HeaderAuthorization = authToken;
                    ((CustomControllerBase)context.Controller).UserAction = userCheck.Value;

                    await next();
                }
                else
                {
                    context.Result = new ContentResult() { Content = "{ \"StatusCode\": \"403\", \"Message\": \"Session Invalid\" }", StatusCode = 403 };
                }
            }
            else
            {
                var resp = new ResponseHelper();
                resp.StatusCode = "401";
                resp.Message = "Please login to access this feature";
                context.Result = new UnauthorizedObjectResult(resp);
            }
        }
    }
}
