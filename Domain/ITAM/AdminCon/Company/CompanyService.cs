using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Domain.Example.FileProcessing;
using Domain.Example.Table;
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.Company
{
    public class CompanyService : ICompanyService
    {
        //private IMemoryCache _cache;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UploaderConfig _uploaderConfig;
        //private readonly IMasterLovService _masterLovService;
        public CompanyService(IUnitOfWork uow, IMapper mapper, IOptions<UploaderConfig> uploaderConfig/*, MasterLovService masterLovService*/)
        {
            _uow = uow;
            _mapper = mapper;
            _uploaderConfig = uploaderConfig.Value;
            //_cache = cache;
            //_masterLovService = masterLovService;
        }

        private string GetNextFilename(string pathFile)
        {
            int i = 1;
            string dir = Path.GetDirectoryName(pathFile);
            string file = Path.GetFileNameWithoutExtension(pathFile) + "{0}";
            string extension = Path.GetExtension(pathFile);

            while (File.Exists(pathFile))
                pathFile = Path.Combine(dir, string.Format(file, "(" + i++ + ")") + extension);

            return pathFile;
        }

        //get

        public async Task<Result<List<CompanyDto>>> GetAllCompany()
        {
            try
            {
                var repoResult = await _uow.Company.Set().OrderByDescending(m => m.CompanyId).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<CompanyDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        public async Task<Result<List<CompanyDto>>> GetCompanyList(int companyId)
        {
            try
            {
                List<TCompany> companyActivities = new List<TCompany>();
                companyActivities = await _uow.Company.Set().Where(a => a.CompanyId.Equals(companyId)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<CompanyDto>>(companyActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListById");
            }
        }
        public async Task<Result<List<CompanyDto>>> GetCompanyListByCompanyName(string companyName)
        {
            try
            {
                List<TCompany> companyActivities = new List<TCompany>();
                companyActivities = await _uow.Company.Set().Where(a => a.CompanyName.Equals(companyName)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<CompanyDto>>(companyActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }
        public async Task<Result<List<CompanyDto>>> GetCompanyListByCompanyType(string companyType)
        {
            try
            {
                List<TCompany> companyActivities = new List<TCompany>();
                companyActivities = await _uow.Company.Set().Where(a => a.CompanyType.Equals(companyType)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<CompanyDto>>(companyActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByType");
            }
        }
        public async Task<Result<List<CompanyDto>>> GetCompanyListByAbbreviation(string abbreviation)
        {
            try
            {
                List<TCompany> companyActivities = new List<TCompany>();
                companyActivities = await _uow.Company.Set().Where(a => a.Abbreviation.Equals(abbreviation)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<CompanyDto>>(companyActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByAbbreviation");
            }
        }
        public async Task<Result<List<CompanyDto>>> GetCompanyListByWebsite(string website)
        {
            try
            {
                List<TCompany> companyActivities = new List<TCompany>();
                companyActivities = await _uow.Company.Set().Where(a => a.Website.Equals(website)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<CompanyDto>>(companyActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByWebsite");
            }
        }

        //put
        public async Task<Result<CompanyDto>> Update (CompanyDto updateParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.Company.Set().FirstOrDefaultAsync(r => r.CompanyId == updateParam.CompanyId);
                if (repoCheck == null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record not found!");
                }

                repoCheck.CompanyId = updateParam.CompanyId;
                repoCheck.CompanyName = updateParam.CompanyName;
                repoCheck.CompanyType = updateParam.CompanyType;
                repoCheck.Abbreviation = updateParam.Abbreviation;
                repoCheck.Website = updateParam.Website;
                repoCheck.CompanyStatus = updateParam.CompanyStatus;
                repoCheck.LastModifiedDate = DateTime.Now;
                repoCheck.ModifiedBy = userSession.Username;

                _uow.Company.Update(repoCheck);
                await _uow.CompleteAsync();

                var result = _mapper.Map<CompanyDto>(repoCheck);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        //Create Data
        public async Task<Result<CompanyDto>> CreateCompany(CompanyDto createParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.Company.Set().FirstOrDefaultAsync(r => r.CompanyId == createParam.CompanyId);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TCompany>(createParam);

                //dataToAdd.CompanyId = 0;

                dataToAdd.ModifiedBy = userSession.Username;

                dataToAdd.LastModifiedDate = DateTime.Now;

                await _uow.Company.Add(dataToAdd);

                await _uow.CompleteAsync();

                var result = _mapper.Map<CompanyDto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //Delete
        public async Task<Result<CompanyDto>> Delete(int companyId)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.Company.Set()
                    .FirstOrDefaultAsync(m => m.CompanyId == companyId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.Company.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<CompanyDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
