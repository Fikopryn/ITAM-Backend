using Domain.R3Framework.R3User;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.AssetStatus
{
    public interface  IAssetStatusService
    {
        Task<Result<List<AssetStatusDto>>> GetAllAssetStatus();
        Task<Result<List<AssetStatusDto>>> GetAssetStatusList(int assetId);
        Task<Result<List<AssetStatusDto>>> GetAssetStatusListByStatus1(string assetStatus1);
        Task<Result<List<AssetStatusDto>>> GetAssetStatusListByDescription(string assetStatusDescription);
        Task<Result<AssetStatusDto>> CreateAssetStatus(AssetStatusDto createParam, R3UserSession userSession);
        Task<Result<AssetStatusDto>> Update(AssetStatusDto data, R3UserSession userSession);
        Task<Result<AssetStatusDto>> Delete(int assetStatusId);
    }
}
