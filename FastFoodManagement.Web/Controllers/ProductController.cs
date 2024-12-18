﻿using AutoMapper;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using FastFoodManagement.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}
		
		[HttpGet("all")]
		public async Task<ActionResult<List<Product>>> GetAllProduct()
		{
			try
			{
				var products = await _productService.GetAllProducts();
				var response = ApiResponse<List<Product>>.SuccessResponse(products);
				return Ok(response);
			}
			catch (Exception ex) 
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[HttpGet("category/{categoryId:int}")]
		public async Task<ActionResult<List<ProductDTO>>> GetProductByCategory(int categoryId)
		{
			try
			{
				var products = await _productService.GetDetailByCategory(categoryId);
				var productDTOs = _mapper.Map<List<ProductDTO>>(products);
				var response = ApiResponse<List<ProductDTO>>.SuccessResponse(productDTOs);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[HttpGet]
		public async Task<ActionResult<List<ProductDTO>>> GetProductByFilter(string? name, string? categories, string? types)
		{
			try
			{
				var products = await _productService.GetDetailByFilter(name, categories, types);
				var productDTOs = _mapper.Map<List<ProductDTO>>(products);
				var response = ApiResponse<List<ProductDTO>>.SuccessResponse(productDTOs);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[Authorize]
		[HttpPost("create")]
		public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDTO)
		{
			try
			{
				var product = _mapper.Map<Product>(productDTO);
				await _productService.Add(product, User.FindFirst("Name")?.Value);
				var response = ApiResponse<ProductDTO>.SuccessResponse(productDTO);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[Authorize]
		[HttpDelete("delete/{id:int}")]
		public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
		{
			try
			{
				await _productService.DeleteById(id, User.FindFirst("Name")?.Value);
				var response = ApiResponse<ProductDTO>.SuccessResponse(null);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[Authorize]
		// Delete All Products
		[HttpDelete("delete/all")]
		public async Task<ActionResult<ProductDTO>> DeleteAllProduct()
		{
			try
			{
				await _productService.DeleteAll(User.FindFirst("Name")?.Value);
				var response = ApiResponse<ProductDTO>.SuccessResponse(null);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		// Get Product with Type Product
		[HttpGet("type-product")]
		public async Task<ActionResult<List<ProductDTO>>> GetTypeIsProduct()
		{
			try
			{
				var products = await _productService.GetTypeProduct();
				var productDTOs = _mapper.Map<List<ProductDTO>>(products);
				var response = ApiResponse<List<ProductDTO>>.SuccessResponse(productDTOs);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}
		
		[Authorize]
		[HttpPatch("soft-delete/{id:int}")]
		public async Task<ActionResult<ProductDTO>> SoftDeleteProduct(int id)
		{
			try
			{
				await _productService.SoftDeleteById(id, User.FindFirst("Name")?.Value);
				var response = ApiResponse<ProductDTO>.SuccessResponse(null);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}
	}
}
