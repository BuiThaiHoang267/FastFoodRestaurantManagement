using AutoMapper;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Data.DTO;
using FastFoodManagement.Data.DTO.Branch;
using FastFoodManagement.Data.DTO.PaymentMethod;

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

			CreateMap<ComboItem,ComboItemDTO>();
			CreateMap<ComboItemDTO, ComboItem>();

			CreateMap<PaymentMethod, CreatePaymentMethodDTO>();
			CreateMap<CreatePaymentMethodDTO, PaymentMethod>();
			CreateMap<PaymentMethod, RetrievePaymentMethodDTO>();
			CreateMap<RetrievePaymentMethodDTO, PaymentMethod>();
			
			CreateMap<Branch, CreateBranchDTO>();
			CreateMap<CreateBranchDTO, Branch>();
			CreateMap<Branch, RetrieveBranchDTO>();
			CreateMap<RetrieveBranchDTO, Branch>();
			CreateMap<UpdateBranchDTO, Branch>();
			CreateMap<Branch, UpdateBranchDTO>();
		}
	}
}
