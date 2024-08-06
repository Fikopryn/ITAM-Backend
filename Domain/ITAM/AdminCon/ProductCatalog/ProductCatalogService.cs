
using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models.Entities.Tables;
using Domain.ITAM.AdminCon.AssetStatus;
using Domain.ITAM.AdminCon.Company;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.ProductCatalog
{
    public class ProductCatalogService : IProductCatalogService
    {
        //private IMemoryCache _cache;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        //private readonly IMasterLovService _masterLovService;
        public ProductCatalogService (IUnitOfWork uow, IMapper mapper/*, MasterLovService masterLovService*/)
        {
            _uow = uow;
            _mapper = mapper;
            //_cache = cache;
            //_masterLovService = masterLovService;
        }
        //Get
        public async Task<Result<List<ProductCatalogDto>>> GetAllProductCatalog()
        {
            try
            {
                var repoResult = await _uow.ProductCatalog.Set().OrderByDescending(m => m.ProductCatalogId).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<ProductCatalogDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        public async Task<Result<List<ProductCatalogDto>>> GetProductCatalogList(int productCatalogId)
        {
            try
            {
                List<TProductCatalog> productCatalogActivities = new List<TProductCatalog>();
                productCatalogActivities = await _uow.ProductCatalog.Set().Where(a => a.ProductCatalogId.Equals(productCatalogId)).OrderByDescending(b => b.ModifiedBy).ToListAsync();

                var result = _mapper.Map<List<ProductCatalogDto>>(productCatalogActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetProductCatalogList");
            }
        }
        public async Task<Result<List<ProductCatalogDto>>> GetProductCatalogListByCompany(int companyId)
        {
            try
            {
                List<TProductCatalog> productCatalogActivities = new List<TProductCatalog>();
                productCatalogActivities = await _uow.ProductCatalog.Set().Where(a => a.CompanyId.Equals(companyId)).OrderByDescending(b => b.LastModifiedDate).ToListAsync();

                var result = _mapper.Map<List<ProductCatalogDto>>(productCatalogActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusList");
            }
        }
        public async Task<Result<List<ProductCatalogDto>>> GetProductCatalogListByTier1(string prodCatTier1)
        {
            try
            {
                List<TProductCatalog> productCatalogActivities = new List<TProductCatalog>();
                productCatalogActivities = await _uow.ProductCatalog.Set().Where(a => a.ProdCatTier1.Equals(prodCatTier1)).OrderByDescending(b => b.LastModifiedDate).ToListAsync();

                var result = _mapper.Map<List<ProductCatalogDto>>(productCatalogActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusList");
            }
        }
        public async Task<Result<List<ProductCatalogDto>>> GetProductCatalogListByTier2(string prodCatTier2)
        {
            try
            {
                List<TProductCatalog> productCatalogActivities = new List<TProductCatalog>();
                productCatalogActivities = await _uow.ProductCatalog.Set().Where(a => a.ProdCatTier2.Equals(prodCatTier2)).OrderByDescending(b => b.LastModifiedDate).ToListAsync();

                var result = _mapper.Map<List<ProductCatalogDto>>(productCatalogActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusList");
            }
        }
        public async Task<Result<List<ProductCatalogDto>>> GetProductCatalogListByTier3(string prodCatTier3)
        {
            try
            {
                List<TProductCatalog> productCatalogActivities = new List<TProductCatalog>();
                productCatalogActivities = await _uow.ProductCatalog.Set().Where(a => a.ProdCatTier3.Equals(prodCatTier3)).OrderByDescending(b => b.LastModifiedDate).ToListAsync();

                var result = _mapper.Map<List<ProductCatalogDto>>(productCatalogActivities);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ": Error when GetAssetStatusList");
            }
        }

        //put
        public async Task<Result<ProductCatalogDto>> Update(ProductCatalogDto data, R3UserSession userSession)
        {
            try
            {
                var repoResult = await _uow.ProductCatalog.Set()
                    .FirstOrDefaultAsync(m => m.ProductCatalogId == data.ProductCatalogId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.ProductCatalogId = data.ProductCatalogId;
                repoResult.CompanyId = data.CompanyId;
                repoResult.ProdCatTier1 = data.ProdCatTier1;
                repoResult.ProdCatTier2 = data.ProdCatTier2;
                repoResult.ProdCatTier3 = data.ProdCatTier3;
                repoResult.ProductName = data.ProductName;
                repoResult.Manufacturer = data.Manufacturer;
                repoResult.ModelVersion = data.ModelVersion;
                repoResult.ProductNumber = data.ProductNumber;
                repoResult.HyperlinkDataSheet = data.HyperlinkDataSheet;
                repoResult.AdditionalInfo = data.AdditionalInfo;
                repoResult.ProdCatStatus= data.ProdCatStatus;
                repoResult.LastModifiedDate = DateTime.Now;
                repoResult.ModifiedBy = userSession.Username;

                _uow.ProductCatalog.Update(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<ProductCatalogDto>(repoResult);
                return Result.Ok(result);

            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //post
        public async Task<Result<ProductCatalogDto>> CreateProductCatalog(ProductCatalogDto createParam, R3UserSession userSession)
        {
            try
            {
                var repoCheck = await _uow.ProductCatalog.Set().FirstOrDefaultAsync(r => r.ProductCatalogId == createParam.ProductCatalogId);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TProductCatalog>(createParam);

                // dataToAdd.ProductCatalogId = 0;

                dataToAdd.ModifiedBy = userSession.Username;

                dataToAdd.LastModifiedDate = DateTime.Now;

                await _uow.ProductCatalog.Add(dataToAdd);

                await _uow.CompleteAsync();

                var result = _mapper.Map<ProductCatalogDto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        //delete
        public async Task<Result<ProductCatalogDto>> Delete(int productCatalogId)
        {
            try
            {
                // NEED ADD INCLUDE BECAUSE HAVE CHILD FOREIGN KEY
                var repoResult = await _uow.ProductCatalog.Set()
                    .FirstOrDefaultAsync(m => m.ProductCatalogId == productCatalogId);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _uow.ProductCatalog.Remove(repoResult);
                await _uow.CompleteAsync();

                var result = _mapper.Map<ProductCatalogDto>(repoResult);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
