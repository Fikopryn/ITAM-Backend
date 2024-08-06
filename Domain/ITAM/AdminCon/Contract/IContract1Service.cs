using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.Contract
{
    public interface IContract1Service
    {
        Task<Result<List<Contract1Dto>>> GetAllContract();
        Task<Result<List<Contract1Dto>>> GetContractListByNo(string contractNo);
        Task<Result<List<Contract1Dto>>> GetContractListByCompany(int companyId);
        Task<Result<List<Contract1Dto>>> GetContractListByContractOwner(string contractOwner);
        Task<Result<List<Contract1Dto>>> GetContractListBySubject(string subject);
        Task<Result<List<Contract1Dto>>> GetContractListBySupplierId(int supplierId);
        Task<Result<List<Contract1Dto>>> GetContractListByStartContract(DateTime startContract);
        Task<Result<List<Contract1Dto>>> GetContractListByEndContract(DateTime endContract);
        Task<Result<List<Contract1Dto>>> GetContractListByValue(decimal value);
        Task<Result<Contract1Dto>> Update(Contract1Dto updateParam, R3UserSession userSession);
        Task<Result<Contract1Dto>> CreateContract(Contract1Dto createParam, R3UserSession userSession);
        Task<Result<Contract1Dto>> Delete(string contractNo);

    }
}
