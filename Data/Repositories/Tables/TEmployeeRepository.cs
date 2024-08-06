using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TEmployeeRepository : BaseRepository<TEmployee>, ITEmployeeRepository
    {
        public TEmployeeRepository(ApplicationContext context) : base(context) 
        { 
        }
    }
}
