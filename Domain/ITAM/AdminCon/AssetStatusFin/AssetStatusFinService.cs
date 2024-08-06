using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Domain._Example.MasterLov;
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.ITAM.AdminCon.AssetStatusFin;
using Domain.ITAM.AdminCon.ProductCatalog;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.ITAM.AdminCon.AssetStatusFin
{
    public class AssetStatusFinService : IAssetStatusFinService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly UploaderConfig _uploaderConfig;
        public AssetStatusFinService(IUnitOfWork uow, IMapper mapper, IOptions<UploaderConfig> uploaderConfig)
        {
            _uow = uow;
            _mapper = mapper;
            _uploaderConfig = uploaderConfig.Value;

        }

        //Get
        public async Task<Result<List<AssetStatusFinDto>>> GetAllAssetStatusFin()
        {
            try
            {
                var repoResult = await _uow.AssetStatusFin.Set().OrderByDescending(m => m.AssetStatusFinId).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<AssetStatusFinDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        public async Task<Result<List<AssetStatusFinDto>>> GetAssetStatusFinById(int assetid)
        {
            try
            {
                List<TAssetStatusFin> assetStatusFinActivities = new List<TAssetStatusFin>();
                assetStatusFinActivities = await _uow.AssetStatusFin.Set().Where(a => a.AssetStatusFinId.Equals(assetid)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<AssetStatusFinDto>>(assetStatusFinActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusListId");
            }
        }
        public async Task<Result<List<AssetStatusFinDto>>> GetAssetStatusFinByFin1(string assetStatusFin1)
        {
            try
            {
                List<TAssetStatusFin> assetStatusFinActivities = new List<TAssetStatusFin>();
                assetStatusFinActivities = await _uow.AssetStatusFin.Set().Where(a => a.AssetStatusFin1.Equals(assetStatusFin1)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<AssetStatusFinDto>>(assetStatusFinActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusFin By 1");
            }
        }
        public async Task<Result<List<AssetStatusFinDto>>> GetAssetStatusFinByDesc(string assetStatusFinDesc)
        {
            try
            {
                List<TAssetStatusFin> assetStatusFinActivities = new List<TAssetStatusFin>();
                assetStatusFinActivities = await _uow.AssetStatusFin.Set().Where(a => a.AssetStatusFinDesc.Equals(assetStatusFinDesc)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<AssetStatusFinDto>>(assetStatusFinActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusFin By Desc");
            }
        }

        //Update
        public async Task<Result<AssetStatusFinDto>> Update(AssetStatusFinDto data, R3UserSession userSession)
        {
            try
            {
                var repoResult = await _uow.AssetStatusFin.Set()
                    .FirstOrDefaultAsync(m => m.AssetStatusFinId == data.AssetStatusFinId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.AssetStatusFinId = data.AssetStatusFinId;
                repoResult.AssetStatusFin1 = data.AssetStatusFin1;
                repoResult.AssetStatusFinDesc = data.AssetStatusFinDesc;
                repoResult.AssociationStatus = data.AssociationStatus;
                repoResult.LastModifiedDate = DateTime.Now;
                repoResult.ModifiedBy = userSession.Username;

                _uow.AssetStatusFin.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<AssetStatusFinDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //Create
        public async Task<Result<AssetStatusFinDto>> CreateAssetStatusFin ( AssetStatusFinDto createParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.AssetStatusFin.Set().FirstOrDefaultAsync(r => r.AssetStatusFinId == createParam.AssetStatusFinId);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TAssetStatusFin>(createParam);

                dataToAdd.ModifiedBy = userSession.Username;

                dataToAdd.LastModifiedDate = DateTime.Now;

                await _uow.AssetStatusFin.Add(dataToAdd);

                dataToAdd.AssetStatusFinId = 0; 

                await _uow.CompleteAsync();

                var result = _mapper.Map<AssetStatusFinDto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //delete
        public async Task<Result<AssetStatusFinDto>> Delete(int assetStatusFinId)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.AssetStatusFin.Set()
                    .FirstOrDefaultAsync(m => m.AssetStatusFinId == assetStatusFinId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.AssetStatusFin.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<AssetStatusFinDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
