using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Domain.ITAM.ConManCon.Contract;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.Contract
{
    public class Contract1Service : IContract1Service
    {
        //private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UploaderConfig _uploaderConfig;
        private readonly IContract1Service _Contract1Service;

        public Contract1Service(IUnitOfWork uow, IMapper mapper, IOptions<UploaderConfig> uploaderConfig, IContract1Service Contract1Service)
        {
            _uow = uow;
            _mapper = mapper;
            _uploaderConfig = uploaderConfig.Value;
            _Contract1Service = Contract1Service;
        }

        //get
        public async Task<Result<List<Contract1Dto>>> GetAllContract()
        {
            try
            {
                var repoResult = await _uow.Contract.Set().OrderByDescending(m => m.ContractNo).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + "Data not found!");

                var result = _mapper.Map<List<Contract1Dto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + "!" + ex.GetMessage());
            }
        }

        public async Task<Result<List<Contract1Dto>>> GetContractListByNo(string contractNo)
        {
            try
            {
                List<TContract> contractActivities = new List<TContract>();
                contractActivities = await _uow.Contract.Set().Where(a => a.ContractNo.Equals(contractNo)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<Contract1Dto>>(contractActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }

        public async Task<Result<List<Contract1Dto>>> GetContractListByCompany(int companyId)
        {
            try
            {
                List<TContract> contractActivities = new List<TContract>();
                contractActivities = await _uow.Contract.Set().Where(a => a.CompanyId.Equals(companyId)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<Contract1Dto>>(contractActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }

        public async Task<Result<List<Contract1Dto>>> GetContractListByContractOwner(string contractOwner)
        {
            try
            {
                List<TContract> contractActivities = new List<TContract>();
                contractActivities = await _uow.Contract.Set().Where(a => a.ContractOwner.Equals(contractOwner)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<Contract1Dto>>(contractActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }

        public async Task<Result<List<Contract1Dto>>> GetContractListBySubject(string subject)
        {
            try
            {
                List<TContract> contractActivities = new List<TContract>();
                contractActivities = await _uow.Contract.Set().Where(a => a.Subject.Equals(subject)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<Contract1Dto>>(contractActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }

        public async Task<Result<List<Contract1Dto>>> GetContractListBySupplierId(int supplierId)
        {
            try
            {
                List<TContract> contractActivities = new List<TContract>();
                contractActivities = await _uow.Contract.Set().Where(a => a.SupplierId.Equals(supplierId)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<Contract1Dto>>(contractActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }

        public async Task<Result<List<Contract1Dto>>> GetContractListByStartContract(DateTime startContract)
        {
            try
            {
                List<TContract> contractActivities = new List<TContract>();
                contractActivities = await _uow.Contract.Set().Where(a => a.StartContract.Equals(startContract)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<Contract1Dto>>(contractActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }

        public async Task<Result<List<Contract1Dto>>> GetContractListByEndContract(DateTime endContract)
        {
            try
            {
                List<TContract> contractActivities = new List<TContract>();
                contractActivities = await _uow.Contract.Set().Where(a => a.EndContract.Equals(endContract)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<Contract1Dto>>(contractActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }

        public async Task<Result<List<Contract1Dto>>> GetContractListByValue(decimal value)
        {
            try
            {
                List<TContract> contractActivities = new List<TContract>();
                contractActivities = await _uow.Contract.Set().Where(a => a.ContractValue.Equals(value)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<Contract1Dto>>(contractActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetCompanyListByName");
            }
        }

        //update
        public async Task<Result<Contract1Dto>> Update(Contract1Dto updateParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.Contract.Set().FirstOrDefaultAsync(r => r.ContractNo == updateParam.ContractNo);
                if (repoCheck == null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record not found!");
                }

                repoCheck.CompanyId = updateParam.CompanyId;
                repoCheck.ContractOwner = updateParam.ContractOwner;
                repoCheck.ContractNo = updateParam.ContractNo;
                repoCheck.Subject = updateParam.Subject;
                repoCheck.SupplierId = updateParam.SupplierId;
                repoCheck.StartContract = updateParam.StartContract;
                repoCheck.EndContract = updateParam.EndContract;
                repoCheck.ContractValue = updateParam.ContractValue;
                repoCheck.ContractDoc = updateParam.ContractDoc;
                repoCheck.LastModifiedDate = DateTime.Now;
                repoCheck.ModifiedBy = userSession.Username;

                _uow.Contract.Update(repoCheck);
                await _uow.CompleteAsync();

                var result = _mapper.Map<Contract1Dto>(repoCheck);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //Create
        public async Task<Result<Contract1Dto>> CreateContract(Contract1Dto createParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.Contract.Set().FirstOrDefaultAsync(r => r.ContractNo == createParam.ContractNo);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TContract>(createParam);

                //dataToAdd.CompanyId = 0;

                dataToAdd.ModifiedBy = userSession.Username;

                dataToAdd.LastModifiedDate = DateTime.Now;

                await _uow.Contract.Add(dataToAdd);

                await _uow.CompleteAsync();

                var result = _mapper.Map<Contract1Dto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //Delete
        public async Task<Result<Contract1Dto>> Delete(string contractNo)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.Contract.Set()
                    .FirstOrDefaultAsync(m => m.ContractNo == contractNo);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.Contract.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<Contract1Dto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
