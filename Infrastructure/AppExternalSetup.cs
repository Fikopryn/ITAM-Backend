using Core;
using Domain.R3Framework.R3ApprovalCenter;
using Domain.R3Framework.R3DataManagement;
using Domain.R3Framework.R3Mailer;
using Domain.R3Framework.R3OAuth;
using Domain.R3Framework.R3port;
using Domain.R3Framework.R3User;
using Domain.R3Framework.R3Workflow;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class AppExternalSetup
    {
        public static IServiceCollection AddAppExternals(this IServiceCollection services)
        {
            services.AddHttpClient(HttpClientName.R3Client);

            #region R3 Framework
            services.AddScoped<IR3ApprovalCenterService, R3ApprovalCenterService>();
            services.AddScoped<IR3DataManagementService, R3DataManagementService>();
            services.AddScoped<IR3Mailer, R3Mailer>();
            services.AddScoped<IR3OAuthService, R3OAuthService>();
            services.AddScoped<IR3portService, R3portService>();
            services.AddScoped<IR3UserService, R3UserService>();
            services.AddScoped<IR3WorkflowService, R3WorkflowService>();
            #endregion

            return services;
        }
    }
}
