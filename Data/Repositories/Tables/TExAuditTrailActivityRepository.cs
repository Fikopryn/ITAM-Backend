using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TExAuditTrailActivityRepository : BaseRepository<TExAuditTrailActivity>, ITExAuditTrailActivityRepository
    {
        public TExAuditTrailActivityRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
