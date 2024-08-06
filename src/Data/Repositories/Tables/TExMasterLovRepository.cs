using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TExMasterLovRepository : BaseRepository<TExMasterLov>, ITExMasterLovRepository
    {
        public TExMasterLovRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
