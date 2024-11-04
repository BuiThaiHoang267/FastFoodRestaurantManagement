using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetByCategory(int categoryId);
        IEnumerable<Product> GetAllProducts();
        void SaveChanges();
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
        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            return _productRepository.GetByCategory(categoryId);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
