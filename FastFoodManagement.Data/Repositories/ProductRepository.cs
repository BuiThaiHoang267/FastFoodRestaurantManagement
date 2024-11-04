using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        // Add more specific methods here when necessary
        // Get product by category
        IQueryable<Product> GetByCategory(int categoryId);
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IQueryable<Product> GetByCategory(int categoryId)
        {
            return this.DbContext.Products.Where(p => p.CategoryId == categoryId);
        }
    }
}
