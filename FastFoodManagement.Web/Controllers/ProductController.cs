using AutoMapper;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using FastFoodManagement.Web.ViewModels;
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
				var products = await _productService.GetAllProducts();
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

		[HttpGet("category/{categoryId:int}")]
		public async Task<ActionResult<List<ProductDTO>>> GetProductByCategory(int categoryId)
		{
			try
			{
				var products = await _productService.GetByCategory(categoryId);
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
