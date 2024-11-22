using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagement.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetByCategory(int categoryId);
		Task DeleteAll();
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

		public async Task DeleteAll()
		{
		    await _dbSet.ExecuteDeleteAsync();
		}

		public async Task<List<Product>> GetByCategory(int categoryId)
		{
            var products = await GetMulti(p => p.CategoryId == categoryId).ToListAsync();
            return products;
		}
	}
}
