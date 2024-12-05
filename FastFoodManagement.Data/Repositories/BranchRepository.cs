using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        // Add more specific methods here when necessary
    }
    public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
    {
        public BranchRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
