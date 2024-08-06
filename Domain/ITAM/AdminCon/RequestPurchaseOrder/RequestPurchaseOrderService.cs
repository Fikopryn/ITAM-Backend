using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Data;
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

namespace Domain.ITAM.AdminCon.RequestPurchaseOrder
{
    public class RequestPurchaseOrderService : IRequestPurchaseOrderService
    {
        private IMemoryCache _cache;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UploaderConfig _uploaderConfig;
        private readonly IRequestPurchaseOrderService _requestPurchaseOrderService;
        public RequestPurchaseOrderService (IUnitOfWork uow, IMapper mapper, IOptions<UploaderConfig> uploaderConfig/*, MasterLovService masterLovService*/)
        {
            _uow = uow;
            _mapper = mapper;
            _uploaderConfig = uploaderConfig.Value;
        }

        //get
        public async Task<Result<List<RequestPurchaseOrderDto>>> GetAllRequestPurchaseOrder()
        {
            try
            {
                var repoResult = await _uow.RequestPurchaseOrder.Set().OrderByDescending(m => m.Rpono).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<RequestPurchaseOrderDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<List<RequestPurchaseOrderDto>>> GetRequestPurchaseOrderByRpoNo(string rpoNo)
        {
            try
            {
                List<TRequestPurchaseOrder> requestPurchaseOrders = new List<TRequestPurchaseOrder>();
                requestPurchaseOrders = await _uow.RequestPurchaseOrder.Set().Where(a => a.Rpono.Equals(rpoNo)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<RequestPurchaseOrderDto>>(requestPurchaseOrders);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusListStatus1");
            }
        }
        public async Task<Result<List<RequestPurchaseOrderDto>>> GetRequestPurchaseOrderByContractNo(string contractNo)
        {
            try
            {
                List<TRequestPurchaseOrder> requestPurchaseOrders = new List<TRequestPurchaseOrder>();
                requestPurchaseOrders = await _uow.RequestPurchaseOrder.Set().Where(a => a.ContractNo.Equals(contractNo)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<RequestPurchaseOrderDto>>(requestPurchaseOrders);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusListStatus1");
            }
        }
        public async Task<Result<List<RequestPurchaseOrderDto>>> GetRequestPurchaseOrderByRpoSubject(string rpoSubject)
        {
            try
            {
                List<TRequestPurchaseOrder> requestPurchaseOrders = new List<TRequestPurchaseOrder>();
                requestPurchaseOrders = await _uow.RequestPurchaseOrder.Set().Where(a => a.Rposubject.Equals(rpoSubject)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<RequestPurchaseOrderDto>>(requestPurchaseOrders);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusListStatus1");
            }
        }

        //put
        public async Task<Result<RequestPurchaseOrderDto>> Update(RequestPurchaseOrderDto data, R3UserSession userSession)
        {
            try
            {
                var repoResult = await _uow.RequestPurchaseOrder.Set()
                    .FirstOrDefaultAsync(m => m.Rpono == data.Rpono);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.Rpono = data.Rpono;
                repoResult.ContractNo = data.ContractNo;
                repoResult.Rposubject = data.Rposubject;
                repoResult.AdditionalInfo = data.AdditionalInfo;
                repoResult.Rostart = data.Rostart;
                repoResult.Roend = data.Roend;
                repoResult.TotalRpo = data.TotalRpo;
                repoResult.ModifiedBy = userSession.Username;
                repoResult.LastModifiedDate = DateTime.Now;

                _uow.RequestPurchaseOrder.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<RequestPurchaseOrderDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //post
        public async Task<Result<RequestPurchaseOrderDto>> CreateRequestPurchaseOrder(RequestPurchaseOrderDto createParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.RequestPurchaseOrder.Set().FirstOrDefaultAsync(r => r.Rpono == createParam.Rpono);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TRequestPurchaseOrder>(createParam);

                //dataToAdd.AssetStatusId = 0;
                dataToAdd.ModifiedBy = userSession.Username;

                dataToAdd.LastModifiedDate = DateTime.Now;

                await _uow.RequestPurchaseOrder.Add(dataToAdd);

                await _uow.CompleteAsync();

                var result = _mapper.Map<RequestPurchaseOrderDto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //delete
        //Delete Data
        public async Task<Result<RequestPurchaseOrderDto>> Delete(string rpoNo)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.RequestPurchaseOrder.Set()
                    .FirstOrDefaultAsync(m => m.Rpono == rpoNo);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.RequestPurchaseOrder.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<RequestPurchaseOrderDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

    }
}
