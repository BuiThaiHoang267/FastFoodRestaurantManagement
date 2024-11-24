using FastFoodManagement.Data.DTO;
using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagement.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<ProductDTO> GetAllDetail();
        Task<List<ProductDTO>> GetByCategory(int categoryId);
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

		public IQueryable<ProductDTO> GetAllDetail()
		{
			IQueryable<ProductDTO> query = (from product in _dbSet
											join category in _context.Categories on product.CategoryId equals category.Id
											join comboItem in _context.ComboItems on product.Id equals comboItem.ComboId into comboGroup
											from comboItem in comboGroup.DefaultIfEmpty()
											join productInCombo in _context.Products on comboItem.ProductId equals productInCombo.Id into productInComboGroup
											from productInCombo in productInComboGroup.DefaultIfEmpty()
											group new { comboItem, productInCombo } by new
											{
												product.Id,
												product.Name,
												product.Price,
												product.Image,
												product.Type,
												product.CategoryId,
												CategoryName = category.Name
											} into g
											select new ProductDTO
											{
												Id = g.Key.Id,
												Name = g.Key.Name,
												Price = g.Key.Price,
												Image = g.Key.Image,
												Type = g.Key.Type,
												CategoryId = g.Key.CategoryId,
												CategoryName = g.Key.CategoryName,
												ComboItems = g.Any(x => x.comboItem != null) ? g.Select(x => new ComboItemDTO
												{
													ComboId = x.comboItem.ComboId,
													ProductId = x.productInCombo.Id,
													Name = x.productInCombo.Name,
													Price = x.productInCombo.Price
												}).ToList() : new List<ComboItemDTO>()
											});
			return query;
		}

		public async Task<List<ProductDTO>> GetByCategory(int categoryId)
		{
            var products = await GetAllDetail().Where(p => p.CategoryId == categoryId).ToListAsync();
			return products;
		}
	}
}
