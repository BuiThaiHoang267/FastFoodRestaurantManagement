using FastFoodManagement.Data.DTO;
using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagement.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetAllDetail();
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

		public IQueryable<Product> GetAllDetail()
		{
			var query = this.GetAll()
				.Where(p => p.DeletedAt == null)
				.Include(p => p.Category)
				.Include(p => p.ComboItems)
				.ThenInclude(c => c.Product);
			return query;
		}
	}
}
