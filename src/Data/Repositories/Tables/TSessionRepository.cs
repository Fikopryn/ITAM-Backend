using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TSessionRepository : BaseRepository<TSession>, ITSessionRepository
    {
        public TSessionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
