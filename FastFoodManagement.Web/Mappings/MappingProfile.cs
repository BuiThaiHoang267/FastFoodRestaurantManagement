using AutoMapper;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Web.ViewModels;

namespace FastFoodManagement.Web.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Category, CategoryDTO>();
			CreateMap<CategoryDTO, Category>();
			CreateMap<Product, ProductDTO>();
			CreateMap<ProductDTO, Product>();
		}
	}
}
