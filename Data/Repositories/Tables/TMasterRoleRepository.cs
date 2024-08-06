using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TMasterRoleRepository : BaseRepository<TMasterRole>, ITMasterRoleRepository
    {
        public TMasterRoleRepository(ApplicationContext context) : base(context) 
        {
        }
    }
}
