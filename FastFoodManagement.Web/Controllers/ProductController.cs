using AutoMapper;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using FastFoodManagement.Data.DTO;
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
		public async Task<ActionResult<List<ProductDTO>>> GetAllProduct()
		{
			try
			{
				var products = await _productService.GetAllDetail();
				var response = ApiResponse<List<ProductDTO>>.SuccessResponse(products);
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
				var products = await _productService.GetByCategory(categoryId);
				var response = ApiResponse<List<ProductDTO>>.SuccessResponse(products);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[HttpPost("create")]
		public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDTO)
		{
			try
			{
				var product = _mapper.Map<Product>(productDTO);
				await _productService.Add(product);
				var response = ApiResponse<ProductDTO>.SuccessResponse(productDTO);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[HttpDelete("delete/{id:int}")]
		public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
		{
			try
			{
				await _productService.DeleteById(id);
				var response = ApiResponse<ProductDTO>.SuccessResponse(null);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<ProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		// Delete All Products
		[HttpDelete("delete/all")]
		public async Task<ActionResult<ProductDTO>> DeleteAllProduct()
		{
			try
			{
				await _productService.DeleteAll();
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
	}
}
