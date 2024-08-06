using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TExPersonContactRepository : BaseRepository<TExPersonContact>, ITExPersonContactRepository
    {
        public TExPersonContactRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
