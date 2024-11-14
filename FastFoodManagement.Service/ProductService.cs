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
        void SaveChanges();
        Task SuspendSaveChanges();
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
        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll().ToListAsync();
        }

        public async Task<List<Product>> GetByCategory(int categoryId)
        {
            return await _productRepository.GetByCategory(categoryId);
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
