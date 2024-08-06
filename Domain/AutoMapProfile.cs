using AutoMapper;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Views;
using Domain.Example.PageView;
using Domain.Example.Table;
using Domain.Example.FileProcessing;
using Domain.InAppSession;
using Domain._Example.AuditTrailActivity;
using Domain._Example.MasterLov;
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.ITAM.AdminCon.Company;
using Domain.ITAM.AdminCon.ProductCatalog;
using Domain.ITAM.AdminCon.AssetStatusFin;
using Domain.ITAM.AdminCon.Employee;
using Domain.ITAM.AdminCon.MasterRole;
using System.Diagnostics.Contracts;
using Domain.ITAM.ConManCon.Contract;
using Domain.ITAM.AdminCon.RequestPurchaseOrder;
using Domain.ITAM.AdminCon.Contract;

namespace Domain
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<TExPerson, ExPersonReadDto>().ReverseMap();
            CreateMap<TExPersonContact, ExPersonContactReadDto>().ReverseMap();
            CreateMap<TExPersonIdentification, ExPersonIdReadDto>().ReverseMap();
            CreateMap<TSession, InAppSessionDto>().ReverseMap();
            CreateMap<TExFileReference, ExFileReferenceDto>().ReverseMap();
            CreateMap<TExAuditTrailActivity, AuditTrailActivityDto>().ReverseMap();
            CreateMap<TExMasterLov, MasterLovDto>().ReverseMap();

            CreateMap<VwExPerson, ExPersonViewDto>().ReverseMap();
            CreateMap<TAssetStatus, AssetStatusDto>().ReverseMap();
            CreateMap<TAssetStatusFin, AssetStatusFinDto>().ReverseMap();
            CreateMap<TCompany, CompanyDto>().ReverseMap();
            CreateMap<TProductCatalog, ProductCatalogDto>().ReverseMap();
            CreateMap<TEmployee, EmployeeDto>().ReverseMap();
            CreateMap<TMasterRole, MasterRoleDto>().ReverseMap();
            //CreateMap<TContract, ContractDto>().ReverseMap();
            CreateMap<TRequestPurchaseOrder, RequestPurchaseOrderDto>().ReverseMap();
           // CreateMap<TContract, Contract1Dto>().ReverseMap();
        }
    }
}
