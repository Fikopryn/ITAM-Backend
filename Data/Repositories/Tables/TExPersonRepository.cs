using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TExPersonRepository : BaseRepository<TExPerson>, ITExPersonRepository
    {
        public TExPersonRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
