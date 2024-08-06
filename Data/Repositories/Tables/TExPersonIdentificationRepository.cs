using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TExPersonIdentificationRepository : BaseRepository<TExPersonIdentification>, ITExPersonIdentificationRepository
    {
        public TExPersonIdentificationRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
