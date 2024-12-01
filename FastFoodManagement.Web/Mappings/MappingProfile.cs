using AutoMapper;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Data.DTO;
using FastFoodManagement.Data.DTO.Branch;
using FastFoodManagement.Data.DTO.Order;
using FastFoodManagement.Data.DTO.PaymentMethod;
using FastFoodManagement.Data.DTO.User;

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

			CreateMap<OrderItem, RetrieveOrderItemDTO>();
			CreateMap<RetrieveOrderItemDTO, OrderItem>();
			CreateMap<OrderItem, CreateOrderItemDTO>();
			CreateMap<CreateOrderItemDTO, OrderItem>();
			CreateMap<OrderItem, UpdateOrderItemDTO>();
			CreateMap<UpdateOrderItemDTO, OrderItem>();

			CreateMap<Order, RetrieveOrderDTO>();
			CreateMap<RetrieveOrderDTO, Order>();
			CreateMap<Order, CreateOrderDTO>();
			CreateMap<CreateOrderDTO, Order>();
			CreateMap<Order, UpdateOrderDTO>();
			CreateMap<UpdateOrderDTO, Order>();

			CreateMap<User, RetrieveUserDTO>();
			CreateMap<RetrieveUserDTO, User>();
			CreateMap<User, RegisterUserDTO>();
			CreateMap<RegisterUserDTO, User>();
		}
	}
}
