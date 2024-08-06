using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.RequestPurchaseOrder
{
    public interface IRequestPurchaseOrderService
    {
        Task<Result<List<RequestPurchaseOrderDto>>> GetAllRequestPurchaseOrder();
        Task<Result<List<RequestPurchaseOrderDto>>> GetRequestPurchaseOrderByRpoNo(string rpoNo);
        Task<Result<List<RequestPurchaseOrderDto>>> GetRequestPurchaseOrderByContractNo(string contractNo);
        Task<Result<List<RequestPurchaseOrderDto>>> GetRequestPurchaseOrderByRpoSubject(string rpoSubject);
        Task<Result<RequestPurchaseOrderDto>> Update(RequestPurchaseOrderDto data, R3UserSession userSession);
        Task<Result<RequestPurchaseOrderDto>> CreateRequestPurchaseOrder(RequestPurchaseOrderDto createParam, R3UserSession userSession);
        Task<Result<RequestPurchaseOrderDto>> Delete(string rpoNo);
        }
}
