using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using Domain.R3Framework.R3DataManagement;
using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain._Example.MasterLov
{
    public class MasterLovService : IMasterLovService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IR3DataManagementService _r3DMService;

        public MasterLovService(IUnitOfWork uow, IMapper mapper, IR3DataManagementService r3DMService)
        {
            _uow = uow;
            _mapper = mapper;
            _r3DMService = r3DMService;
        }

        public async Task<Result<List<MasterLovDto>>> GetAll()
        {
            try
            {
                var repoResult = await _uow.ExMasterLov.Set().OrderBy(m => m.LovSequence).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<MasterLovDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<PagingResponse<MasterLovDto>>> GetAllPaged(PagingRequest pRequest)
        {
            try
            {
                Expression<Func<TExMasterLov, bool>> _where = m => true;
                foreach (var parameter in pRequest.Parameters)
                {
                    if (!string.IsNullOrWhiteSpace(parameter.SearchValue))
                    {
                        var colData = parameter.Name.ToLower();

                        if (colData == nameof(MasterLovDto.LovName).ToLower())
                        {
                            _where = _where.And(m => m.LovName.ToLower().Equals(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(MasterLovDto.LovDescriptions).ToLower())
                        {
                            _where = _where.And(m => m.LovDescriptions.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(MasterLovDto.LovCategory).ToLower())
                        {
                            _where = _where.And(m => m.LovCategory.ToLower().Equals(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(MasterLovDto.LovKey).ToLower())
                        {
                            _where = _where.And(m => m.LovKey.ToLower().Equals(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(MasterLovDto.LovValue).ToLower())
                        {
                            _where = _where.And(m => m.LovValue.ToLower().Contains(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(MasterLovDto.LovParentKey).ToLower())
                        {
                            _where = _where.And(m => m.LovParentKey.ToLower().Equals(parameter.SearchValue.ToLower()));
                        }

                        if (colData == nameof(MasterLovDto.LovSequence).ToLower())
                        {
                            _where = _where.And(m => m.LovSequence == Decimal.Parse(parameter.SearchValue));
                        }

                        if (colData == nameof(MasterLovDto.IsActive).ToLower())
                        {
                            _where = _where.And(m => m.IsActive == Boolean.Parse(parameter.SearchValue));
                        }
                    }
                }
                var pageCond = new PagingCondition<TExMasterLov>(_where, pRequest);

                var countAll = await _uow.ExMasterLov.Set().CountAsync();
                var resultData = await _uow.ExMasterLov.GetPagedData(pageCond);
                var resultCount = await _uow.ExMasterLov.CountData(pageCond);
                var result = PagingResponse<MasterLovDto>.CreateResponse(resultCount, countAll, _mapper.Map<IEnumerable<MasterLovDto>>(resultData), pRequest.Page, pRequest.Length);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<List<string>>> GetAllLovNameDistinct()
        {
            try
            {
                var repoResult = await _uow.ExMasterLov.Set().OrderBy(m => m.LovName).Select(l => l.LovName).Distinct().ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                return Result.Ok(repoResult);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<List<MasterLovDto>>> GetByName(string lovName)
        {
            try
            {
                var repoResult = await _uow.ExMasterLov.Set().Where(m => m.LovName == lovName && m.IsActive == true).OrderBy(m => m.LovSequence).ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<List<MasterLovDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<int>> GetSeqNextVal(string seqName)
        {
            try
            {
                var repoResult = await _uow.ExMasterLov.Set().Where(m => m.LovName.Equals("SEQUENCE") && m.LovKey.Equals(seqName)).FirstOrDefaultAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = int.Parse(repoResult.LovValue) + 1;
                repoResult.LovValue = result.ToString();
                _uow.ExMasterLov.Update(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<int>> GetSeqNextValRsv(string seqName, int rsvPoint)
        {
            try
            {
                var repoResult = await _uow.ExMasterLov.Set().Where(m => m.LovName == seqName).FirstOrDefaultAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = int.Parse(repoResult.LovValue) + 1;
                var resultReserve = result + rsvPoint;
                repoResult.LovValue = resultReserve.ToString();
                _uow.ExMasterLov.Update(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MasterLovDto>> GetLovById(R3UserSession userSession, decimal lovId)
        {
            try
            {
                var repoResult = await _uow.ExMasterLov.Set().Where(m => m.LovId == lovId).FirstOrDefaultAsync();
                var result = _mapper.Map<MasterLovDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MasterLovDto>> CreateLov(R3UserSession userSession, MasterLovDto createParam)
        {
            try
            {
                var repoCheck = await _uow.ExMasterLov.Set().FirstOrDefaultAsync(r => r.LovId == createParam.LovId);
                if (repoCheck != null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record already exist!");
                }

                var dataToAdd = _mapper.Map<TExMasterLov>(createParam);
                await _uow.ExMasterLov.Add(dataToAdd);

                await _uow.CompleteAsync();

                var result = _mapper.Map<MasterLovDto>(dataToAdd);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MasterLovDto>> UpdateLov(R3UserSession userSession, MasterLovDto updateParam)
        {
            try
            {
                var repoCheck = await _uow.ExMasterLov.Set().FirstOrDefaultAsync(r => r.LovId == updateParam.LovId);
                if (repoCheck == null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record not found!");
                }

                repoCheck.LovName = updateParam.LovName;
                repoCheck.LovDescriptions = updateParam.LovDescriptions;
                repoCheck.LovCategory = updateParam.LovCategory;
                repoCheck.LovKey = updateParam.LovKey;
                repoCheck.LovValue = updateParam.LovValue;
                repoCheck.LovParentKey = updateParam.LovParentKey;
                repoCheck.LovSequence = updateParam.LovSequence;
                repoCheck.LovAttr1 = updateParam.LovAttr1;
                repoCheck.LovAttr2 = updateParam.LovAttr2;
                repoCheck.LovAttr3 = updateParam.LovAttr3;
                repoCheck.IsActive = updateParam.IsActive;

                _uow.ExMasterLov.Update(repoCheck);
                await _uow.CompleteAsync();

                var result = _mapper.Map<MasterLovDto>(repoCheck);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MasterLovDto>> DeleteLov(R3UserSession userSession, decimal lovid)
        {
            try
            {
                var repoCheck = await _uow.ExMasterLov.Set().FirstOrDefaultAsync(r => r.LovId.Equals(lovid));
                if (repoCheck == null)
                {
                    return Result.Fail(ResponseStatusCode.BadRequest + ": Record not found!");
                }
                _uow.ExMasterLov.Remove(repoCheck);
                await _uow.CompleteAsync();

                var result = _mapper.Map<MasterLovDto>(repoCheck);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
