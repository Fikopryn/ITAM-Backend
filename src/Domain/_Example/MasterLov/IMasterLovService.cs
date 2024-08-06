using Core.Models;
using Domain.R3Framework.R3User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain._Example.MasterLov
{
    public interface IMasterLovService
    {
        Task<Result<List<MasterLovDto>>> GetAll();
        Task<Result<PagingResponse<MasterLovDto>>> GetAllPaged(PagingRequest pRequest);
        Task<Result<List<string>>> GetAllLovNameDistinct();
        Task<Result<List<MasterLovDto>>> GetByName(string lovName);
        Task<Result<int>> GetSeqNextVal(string seqName);
        Task<Result<int>> GetSeqNextValRsv(string seqName, int rsvPoint);
        Task<Result<MasterLovDto>> GetLovById(R3UserSession userSession, decimal lovId);
        Task<Result<MasterLovDto>> CreateLov(R3UserSession userSession, MasterLovDto createParam);
        Task<Result<MasterLovDto>> UpdateLov(R3UserSession userSession, MasterLovDto updateParam);
        Task<Result<MasterLovDto>> DeleteLov(R3UserSession userSession, decimal lovId);
    }
}
