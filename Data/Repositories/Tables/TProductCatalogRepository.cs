using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TProductCatalogRepository : BaseRepository<TProductCatalog>, ITProductCatalogRepository
    {
        public TProductCatalogRepository (ApplicationContext context) : base(context)
        {
        }
    }
}
