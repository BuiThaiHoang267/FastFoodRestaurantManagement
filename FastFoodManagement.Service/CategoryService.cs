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
    public interface ICategoryService
	{
		public IEnumerable<Category> GetAllCategories();
		public Category GetCategoryById(int id);
		public void AddCategory(Category category);
		public void SaveChanges();
	}
	public class CategoryService : ICategoryService
	{
		private ICategoryRepository _categoryRepository;
		private IUnitOfWork _unitOfWork;
		public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
		{
			_categoryRepository = categoryRepository;
			_unitOfWork = unitOfWork;
		}
		public void AddCategory(Category category)
		{
			_categoryRepository.Add(category);
			SaveChanges();
		}

		public IEnumerable<Category> GetAllCategories()
		{
			return _categoryRepository.GetAll();
		}

		public Category GetCategoryById(int id)
		{
			return _categoryRepository.GetSingleById(id);
		}

		public void SaveChanges()
		{
			_unitOfWork.Commit();
		}
	}
}
