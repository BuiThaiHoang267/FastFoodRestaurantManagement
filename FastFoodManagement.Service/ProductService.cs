using FastFoodManagement.Data.DTO;
using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Service
{
    public interface IProductService
    {
        Task<List<Product>> GetAllDetail();
		Task<List<Product>> GetDetailByFilter(string? name, string? categories, string? types);
		Task<List<Product>> GetDetailByCategory(int categoryId);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetById(int id);
        Task<List<Product>> GetTypeProduct();
        Task DeleteById(int id, string name);
        Task SoftDeleteById(int id, string name);
		Task Add(Product product, string name);
		void SaveChanges();
        Task SuspendSaveChanges();
        Task DeleteAll(string name);
    }
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IComboItemRepository _comboItemRepository;
        private IUnitOfWork _unitOfWork;
		public ProductService(IProductRepository productRepository, IComboItemRepository comboItemRepository, IUnitOfWork unitOfWork) 
        {
            _productRepository = productRepository;
            _comboItemRepository = comboItemRepository;
            _unitOfWork = unitOfWork;
        }

		public async Task Add(Product product, string name)
		{
			await _productRepository.Add(product);
            await SuspendSaveChanges();
		}

		public async Task DeleteAll(string name)
		{
			await _productRepository.DeleteAll();
			await SuspendSaveChanges();
		}

		public async Task DeleteById(int id, string name)
		{
            await _productRepository.DeleteMulti(p => p.Id == id);
			await SuspendSaveChanges();
		}

		public async Task SoftDeleteById(int id, string name)
		{
			var product = _productRepository.GetMulti(p => p.Id == id).FirstOrDefault();
			if (product == null)
			{
				throw new Exception("Product not found");
			}
			var comboItems = await _comboItemRepository.GetMulti(ci => ci.ProductId == id).ToListAsync();
			if (comboItems.Count > 0)
			{
				throw new Exception("Product is in use");
			}
			product.DeletedAt = DateTime.Now;
			await _productRepository.Update(product);
			await SuspendSaveChanges();
		}

		public async Task<List<Product>> GetAllDetail()
		{
			return await _productRepository.GetAllDetail().ToListAsync();
		}

		public async Task<List<Product>> GetAllProducts()
        {
			return await _productRepository.GetAll().ToListAsync();
		}

        public async Task<List<Product>> GetDetailByCategory(int categoryId)
        {
			if(categoryId == 0)
			{
				return await _productRepository.GetAllDetail().ToListAsync();
			}
			else
			{
				return await _productRepository.GetAllDetail().Where(p => p.CategoryId == categoryId).ToListAsync();
			}
        }

		public Task<Product> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Product>> GetDetailByFilter(string? name, string? categories, string? types)
		{
			var query = _productRepository.GetAllDetail();
			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(p => p.Name.ToLower().Contains(name));
			}

			if (!string.IsNullOrEmpty(categories))
			{
				var categoryIds = categories.Split(',').Select(int.Parse).ToList();
				query = query.Where(p => categoryIds.Contains(p.CategoryId));
			}

			if (!string.IsNullOrEmpty(types))
			{
				var typeNames = types.Split(',').ToList();
				query = query.Where(p => typeNames.Contains(p.Type));
			}

			return await query.ToListAsync();
		}

		public async Task<List<Product>> GetTypeProduct()
		{
            var data = await _productRepository.GetAll().Where(p => p.Type == "Product").ToListAsync();
			return data;
		}

		public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

		public async Task SuspendSaveChanges()
		{
            await _unitOfWork.CommitAsync();
		}
	}
}
