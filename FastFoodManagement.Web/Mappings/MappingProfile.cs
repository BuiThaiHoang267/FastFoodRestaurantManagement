using AutoMapper;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Data.DTO;

namespace FastFoodManagement.Web.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Category, CategoryDTO>();
			CreateMap<CategoryDTO, Category>();

			CreateMap<Product, ProductDTO>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
			CreateMap<ProductDTO, Product>();

			CreateMap<ComboItem,ComboItemDTO>();
			CreateMap<ComboItemDTO, ComboItem>();
		}
	}
}
