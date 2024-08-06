using Core.Interfaces;
using Data;
using Domain._Example.AuditTrailActivity;
using Domain._Example.MasterLov;
using Domain.Example.FileProcessing;
using Domain.Example.PageView;
using Domain.Example.Table;
using Domain.InAppSession;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class AppServiceSetup
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuditTrailActivityService, AuditTrailActivityService>();
            services.AddTransient<IMasterLovService, MasterLovService>();
            // Example. Can be deleted.
            services.AddTransient<IFileProcessingService, FileProcessingService>();
            services.AddTransient<IPageViewService, PageViewService>();
            services.AddTransient<ITableService, TableService>();
            services.AddTransient<IInAppSessionService, InAppSessionService> ();

            return services;
        }
    }
}
