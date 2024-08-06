using Domain.R3Framework.R3User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.ITAM.AdminCon.ProductCatalog
{
    public interface IProductCatalogService
    {
        Task<Result<List<ProductCatalogDto>>> GetAllProductCatalog();
        Task<Result<List<ProductCatalogDto>>> GetProductCatalogList(int productCatalogId);
        Task<Result<List<ProductCatalogDto>>> GetProductCatalogListByCompany(int companyId);
        Task<Result<List<ProductCatalogDto>>> GetProductCatalogListByTier3(string prodCatTier3);
        Task<Result<List<ProductCatalogDto>>> GetProductCatalogListByTier2(string prodCatTier2);
        Task<Result<List<ProductCatalogDto>>> GetProductCatalogListByTier1(string prodCatTier1);
        Task<Result<ProductCatalogDto>> Update(ProductCatalogDto data, R3UserSession userSession);
        Task<Result<ProductCatalogDto>> CreateProductCatalog(ProductCatalogDto createParam, R3UserSession userSession);
        Task<Result<ProductCatalogDto>> Delete(int productCatalogId);
        
    }
}
