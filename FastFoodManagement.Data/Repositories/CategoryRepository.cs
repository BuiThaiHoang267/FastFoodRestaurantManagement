using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagement.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task DeleteAll();
    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

		public async Task DeleteAll()
		{
            await _dbSet.ExecuteDeleteAsync();
		}
	}
}
