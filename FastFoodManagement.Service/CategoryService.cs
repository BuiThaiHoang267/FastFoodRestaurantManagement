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
    public interface ICategoryService
	{
		public Task<List<Category>> GetAllCategories();
		public Task<Category> GetCategoryById(int id);
		public Task AddCategory(Category category);
		public Task DeleteAllCategory();
		public void SaveChanges();
		public Task SuspendChanges();

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
		public async Task AddCategory(Category category)
		{
			await _categoryRepository.Add(category);
			await SuspendChanges();
		}

		public async Task<List<Category>> GetAllCategories()
		{
			List<Category> entity = await _categoryRepository.GetAll().ToListAsync();
			return entity;
		}

		public async Task<Category> GetCategoryById(int id)
		{
			return await _categoryRepository.GetSingleById(id);
		}

		public async Task DeleteAllCategory()
		{
			await _categoryRepository.DeleteAll();
			await SuspendChanges();
		}

		public void SaveChanges()
		{
			_unitOfWork.Commit();
		}

		public async Task SuspendChanges()
		{
			 await _unitOfWork.CommitAsync();
		}
	}
}
