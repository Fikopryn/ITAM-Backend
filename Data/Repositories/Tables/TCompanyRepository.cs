using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TCompanyRepository : BaseRepository<TCompany>, ITCompanyRepository
    {
        public TCompanyRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
