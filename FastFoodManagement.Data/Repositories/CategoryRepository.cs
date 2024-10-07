using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface ICategoryRepository
    {
        // Add more specific methods here when necessary
    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
