using Core.Helpers;
using Domain;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Test
{
    public class BaseTest
    {
        public readonly IConfiguration _cfg;
        public readonly ServiceProvider _svcProvider;

        public BaseTest()
        {
            _cfg = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();

            var conString = _cfg.GetConnectionString("DefaultConnection");
            var encryptPass = _cfg.GetConnectionString("DefaultEncryptedPassword");

            var _svc = new ServiceCollection();

            _svc
                .AddAppDbContexts(conString.Replace("@password", EncryptionHelper.AesDecrypt(encryptPass)))
                .AddAppServices()
                .AddAppExternals()
                .AddAppSettings(_cfg)
                .AddAutoMapper(typeof(AutoMapProfile));

            _svcProvider = _svc.BuildServiceProvider();
        }
    }
}
