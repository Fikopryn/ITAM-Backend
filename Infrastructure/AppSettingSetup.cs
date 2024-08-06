
using Core.Models;
using Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class AppSettingSetup
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettingsConfig>(config.GetSection(nameof(AppSettingsConfig)));
            services.Configure<R3OAuthConfig>(config.GetSection(nameof(R3OAuthConfig)));
            services.Configure<R3portConfig>(config.GetSection(nameof(R3portConfig)));
            services.Configure<R3tinaConfig>(config.GetSection(nameof(R3tinaConfig)));
            services.Configure<WorkflowConfig>(config.GetSection(nameof(WorkflowConfig)));
            services.Configure<MailerConfig>(config.GetSection(nameof(MailerConfig)));
            services.Configure<UploaderConfig>(config.GetSection(nameof(UploaderConfig)));

            services.AddScoped<TokenValidFilter>();
            services.AddScoped<IpAddressFilter>();

            return services;
        }

    }
}
