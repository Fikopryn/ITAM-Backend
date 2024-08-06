using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.ConManCon.Contract
{
    public interface IContractService
    {
        Task<Result<List<ContractDto>>> GetAllContract();
        Task<Result<List<ContractDto>>> GetContractListByNo(string contractNo);
        Task<Result<List<ContractDto>>> GetContractListByCompany(int companyId);
        Task<Result<List<ContractDto>>> GetContractListByContractOwner(string contractOwner);
        Task<Result<List<ContractDto>>> GetContractListBySubject(string subject);
        Task<Result<List<ContractDto>>> GetContractListBySupplierId(int supplierId);
        Task<Result<List<ContractDto>>> GetContractListByStartContract(DateTime startContract);
        Task<Result<List<ContractDto>>> GetContractListByEndContract(DateTime endContract);
        Task<Result<List<ContractDto>>> GetContractListByValue(decimal value);
        Task<Result<ContractDto>> Update(ContractDto updateParam, R3UserSession userSession);
        Task<Result<ContractDto>> CreateContract(ContractDto createParam, R3UserSession userSession);
        Task<Result<ContractDto>> Delete(string contractNo);

    }
}
