using Core.Interfaces;
using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;
using Data;
using Domain._Example.AuditTrailActivity;
using Domain._Example.MasterLov;
using Domain.Example.FileProcessing;
using Domain.Example.PageView;
using Domain.Example.Table;
using Domain.InAppSession;
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.ITAM.AdminCon.Company;
using Domain.ITAM.AdminCon.ProductCatalog;
using Domain.ITAM.AdminCon.AssetStatusFin;
using Microsoft.Extensions.DependencyInjection;
using Domain.ITAM.AdminCon.Employee;
using Domain.ITAM.AdminCon.MasterRole;
using Domain.ITAM.ConManCon.Contract;
using Domain.ITAM.AdminCon.RequestPurchaseOrder;
using Domain.ITAM.AdminCon.Contract;

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
            services.AddTransient<IAssetStatusService, AssetStatusService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IProductCatalogService, ProductCatalogService>();
            services.AddTransient<IAssetStatusFinService, AssetStatusFinService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IMasterRoleService, MasterRoleService>();
            services.AddTransient<IRequestPurchaseOrderService, RequestPurchaseOrderService>();
            //services.AddTransient<IContract1Service, Contract1Service>();

            return services;
        }
    }
}