using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TRequestPurchaseOrderRepository : BaseRepository<TRequestPurchaseOrder>, ITRequestPurchaseOrderRepository
    {
        public TRequestPurchaseOrderRepository(ApplicationContext context) : base(context) 
        {
            
        }
    }
}
