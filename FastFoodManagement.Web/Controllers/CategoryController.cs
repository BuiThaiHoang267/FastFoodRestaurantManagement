﻿using AutoMapper;
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
		public async Task<ActionResult<ApiResponse<List<CategoryDTO>>>> GetAllCategories()
		{
			try
			{
				var categories = await _categoryService.GetAllCategories();
				var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
				var response = ApiResponse<List<CategoryDTO>>.SuccessResponse(categoryDTOs, code: 200);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<CategoryDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[HttpPost("create")]
		public async Task<ActionResult<ApiResponse<CategoryDTO>>> CreateCategory(CategoryDTO categoryDTO)
		{
			try
			{
				var category = _mapper.Map<Category>(categoryDTO);

				await _categoryService.AddCategory(category);

				var response = ApiResponse<CategoryDTO>.SuccessResponse(categoryDTO, "Category create successfully", 201);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<CategoryDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}
	}
}
