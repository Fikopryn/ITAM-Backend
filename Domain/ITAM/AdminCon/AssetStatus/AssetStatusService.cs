using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Domain._Example.MasterLov;
using Domain.Example.FileProcessing;
using Domain.Example.Table;
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

namespace Domain.ITAM.AdminCon.AssetStatus
{
    public class AssetStatusService : IAssetStatusService
    {
        private IMemoryCache _cache;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UploaderConfig _uploaderConfig;
        //private readonly IMasterLovService _masterLovService;
        public AssetStatusService(IUnitOfWork uow, IMapper mapper, IOptions<UploaderConfig> uploaderConfig/*, MasterLovService masterLovService*/)
        {
            _uow = uow;
            _mapper = mapper;
            _uploaderConfig = uploaderConfig.Value;
        }

        //Get
        public async Task<Result<List<AssetStatusDto>>> GetAllAssetStatus()
        {
            try
            {
                var repoResult = await _uow.AssetStatus.Set().OrderByDescending(m => m.AssetStatusId).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<AssetStatusDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<List<AssetStatusDto>>> GetAssetStatusList(int assetId)
        {
            try
            {
                List<TAssetStatus> assetStatusActivities = new List<TAssetStatus>();
                assetStatusActivities = await _uow.AssetStatus.Set().Where(a => a.AssetStatusId.Equals(assetId)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<AssetStatusDto>>(assetStatusActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusListId");
            }
        }
        public async Task<Result<List<AssetStatusDto>>> GetAssetStatusListByStatus1(string assetStatus1)
        {
            try
            {
                List<TAssetStatus> assetStatusActivities = new List<TAssetStatus>();
                assetStatusActivities = await _uow.AssetStatus.Set().Where(a => a.AssetStatus1.Equals(assetStatus1)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<AssetStatusDto>>(assetStatusActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusListStatus1");
            }
        }
        public async Task<Result<List<AssetStatusDto>>> GetAssetStatusListByDescription(string assetStatusDescription)
        {
            try
            {
                List<TAssetStatus> assetStatusActivities = new List<TAssetStatus>();
                assetStatusActivities = await _uow.AssetStatus.Set().Where(a => a.AssetStatusDescription.Equals(assetStatusDescription)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<AssetStatusDto>>(assetStatusActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusListDescription");
            }
        }


        //Pengisian Data
        public async Task<Result<AssetStatusDto>> CreateAssetStatus (AssetStatusDto createParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.AssetStatus.Set().FirstOrDefaultAsync(r => r.AssetStatusId == createParam.AssetStatusId);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TAssetStatus>(createParam);

                //dataToAdd.AssetStatusId = 0;
                dataToAdd.ModifiedBy = userSession.Username;

                dataToAdd.LastModifiedDate = DateTime.Now;

                await _uow.AssetStatus.Add(dataToAdd);

                await _uow.CompleteAsync();

                var result = _mapper.Map<AssetStatusDto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //Update Data
        public async Task<Result<AssetStatusDto>> Update(AssetStatusDto data, R3UserSession userSession)
        {
            try
            {
                var repoResult = await _uow.AssetStatus.Set()
                    .FirstOrDefaultAsync(m => m.AssetStatusId == data.AssetStatusId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.AssetStatusId = data.AssetStatusId;
                repoResult.AssetStatus1 = data.AssetStatus1;
                repoResult.AssetStatusDescription = data.AssetStatusDescription;
                repoResult.AssociationStatus = data.AssociationStatus;
                repoResult.ModifiedBy = userSession.Username;
                repoResult.LastModifiedDate = DateTime.Now;

                _uow.AssetStatus.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<AssetStatusDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //Delete Data
        public async Task<Result<AssetStatusDto>> Delete(int assetStatusId)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.AssetStatus.Set()
                    .FirstOrDefaultAsync(m => m.AssetStatusId == assetStatusId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.AssetStatus.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<AssetStatusDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
