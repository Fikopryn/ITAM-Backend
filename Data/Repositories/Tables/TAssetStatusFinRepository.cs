using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TAssetStatusFinRepository : BaseRepository<TAssetStatusFin>, ITAssetStatusFinRepository
    {
        public TAssetStatusFinRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
