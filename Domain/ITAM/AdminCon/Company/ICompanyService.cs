using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.Company
{
    public interface ICompanyService
    {
        Task<Result<List<CompanyDto>>> GetAllCompany();
        Task<Result<List<CompanyDto>>> GetCompanyList(int companyId);
        Task<Result<List<CompanyDto>>> GetCompanyListByCompanyName(string companyName);
        Task<Result<List<CompanyDto>>> GetCompanyListByCompanyType(string companyType);
        Task<Result<List<CompanyDto>>> GetCompanyListByAbbreviation(string abbreviation);
        Task<Result<List<CompanyDto>>> GetCompanyListByWebsite(string website);
        Task<Result<CompanyDto>> Update(CompanyDto updateParam, R3UserSession userSession);
        Task<Result<CompanyDto>> CreateCompany(CompanyDto createParam, R3UserSession userSession);
        Task<Result<CompanyDto>> Delete(int companyId);
    }
}
