﻿using AutoMapper;
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

			CreateMap<Product, ProductDTO>();
			CreateMap<ProductDTO, Product>();

			CreateMap<ComboItem,ComboItemDTO>();
			CreateMap<ComboItemDTO, ComboItem>();
		}
	}
}
