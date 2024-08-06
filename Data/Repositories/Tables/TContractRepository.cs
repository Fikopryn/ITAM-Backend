using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TContractRepository : BaseRepository<TContract>, ITContractRepository
    {
        public TContractRepository(ApplicationContext context) : base(context) 
        {
        }
    }
}
