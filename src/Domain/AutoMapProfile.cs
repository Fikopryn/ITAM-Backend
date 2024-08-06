using AutoMapper;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Views;
using Domain.Example.PageView;
using Domain.Example.Table;
using Domain.Example.FileProcessing;
using Domain.InAppSession;
using Domain._Example.AuditTrailActivity;
using Domain._Example.MasterLov;

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
        }
    }
}
