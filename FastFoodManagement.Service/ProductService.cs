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
        Task<List<Product>> GetByCategory(int categoryId);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetById(int id);
        Task DeleteById(int id);
		Task Add(Product product);
		void SaveChanges();
        Task SuspendSaveChanges();
        Task DeleteAll();
    }
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) 
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

		public async Task Add(Product product)
		{
			await _productRepository.Add(product);
            await SuspendSaveChanges();
		}

		public async Task DeleteAll()
		{
			await _productRepository.DeleteAll();
			await SuspendSaveChanges();
		}

		public async Task DeleteById(int id)
		{
            await _productRepository.DeleteMulti(p => p.Id == id);
			await SuspendSaveChanges();
		}

		public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll().ToListAsync();
        }

        public async Task<List<Product>> GetByCategory(int categoryId)
        {
            return await _productRepository.GetByCategory(categoryId);
        }

		public Task<Product> GetById(int id)
		{
			throw new NotImplementedException();
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
