using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwExPersonRepository : BaseRepository<VwExPerson>, IVwExPersonRepository
    {
        public VwExPersonRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
