    using Core.Interfaces.IRepositories.Tables;
    using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TAssetStatusRepository : BaseRepository<TAssetStatus>, ITAssetStatusRepository
    {
        public TAssetStatusRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
