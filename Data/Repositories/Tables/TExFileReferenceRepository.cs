using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TExFileReferenceRepository : BaseRepository<TExFileReference>, ITExFileReferenceRepository
    {
        public TExFileReferenceRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
