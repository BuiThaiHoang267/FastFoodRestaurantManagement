using AutoMapper;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using FastFoodManagement.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers
{
	[Route("api/category")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		[HttpGet("all")]
		public IEnumerable<Category> GetAllCategories()
		{
			return _categoryService.GetAllCategories();
		}

		[HttpPost("create")]
		public Task<ActionResult<ApiResponse<Category>>> CreateCategory(CategoryDTO categoryDTO)
		{
			try
			{
				var category = _mapper.Map<Category>(categoryDTO);

				_categoryService.AddCategory(category);
				ApiResponse<CategoryDTO> response = new ApiResponse<CategoryDTO>("post successfully", categoryDTO);
				return Task.FromResult<ActionResult<ApiResponse<Category>>>(Ok(response));
			}
			catch (Exception ex)
			{
				ApiResponse<CategoryDTO> response = new ApiResponse<CategoryDTO>(ex.Message, []);
				return Task.FromResult<ActionResult<ApiResponse<Category>>>(BadRequest(response));
			}
		}
	}
}
