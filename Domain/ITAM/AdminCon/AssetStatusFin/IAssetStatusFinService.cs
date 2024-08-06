using Domain.R3Framework.R3User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.AssetStatusFin
{
    public interface IAssetStatusFinService
    {
        Task<Result<List<AssetStatusFinDto>>> GetAllAssetStatusFin();
        Task<Result<List<AssetStatusFinDto>>> GetAssetStatusFinById(int assetId);
        Task<Result<List<AssetStatusFinDto>>> GetAssetStatusFinByFin1(string assetStatusFin1);
        Task<Result<List<AssetStatusFinDto>>> GetAssetStatusFinByDesc(string assetStatusFinDesc);
        Task<Result<AssetStatusFinDto>> CreateAssetStatusFin(AssetStatusFinDto createParam, R3UserSession userSession);
        Task<Result<AssetStatusFinDto>> Update(AssetStatusFinDto data, R3UserSession userSession);
        Task<Result<AssetStatusFinDto>> Delete(int assetStatusFinId);
    }
}
