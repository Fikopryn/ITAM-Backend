using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models.Entities.Tables;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InAppSession
{
    public class InAppSessionService : IInAppSessionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public InAppSessionService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<InAppSessionDto>> FindByIpAddress(string ipAddress)
        {
            try
            {
                var repoResult = await _uow.Sessions.Set()
                    .FirstOrDefaultAsync(m => m.IpAddress == ipAddress);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<InAppSessionDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("ERROR", ipAddress, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage()), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<InAppSessionDto>> FindByRefreshToken(string refreshToken)
        {
            try
            {
                var repoResult = await _uow.Sessions.Set()
                    .FirstOrDefaultAsync(m => m.RefreshToken == refreshToken);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<InAppSessionDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("ERROR", refreshToken, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage()), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<InAppSessionDto>> Save(InAppSessionDto inAppSessionDto)
        {
            try
            {
                var dataToAdd = _mapper.Map<TSession>(inAppSessionDto);

                var checkData = await _uow.Sessions.Set()
                    .FirstOrDefaultAsync(m => m.IpAddress == dataToAdd.IpAddress);

                if (checkData == null)
                {
                    await _uow.Sessions.Add(dataToAdd);
                } else
                {
                    checkData.ExpiredDate = dataToAdd.ExpiredDate;
                    checkData.RefreshToken = dataToAdd.RefreshToken;
                    _uow.Sessions.Update(checkData);
                    
                }
                await _uow.CompleteAsync();

                var result = _mapper.Map<InAppSessionDto>(dataToAdd);
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("{@LogData}", LogData.BuildErrorLog("ERROR", inAppSessionDto.IpAddress, SystemHelper.GetActualAsyncMethodName(), ex.GetMessage(), inAppSessionDto), ex);
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
