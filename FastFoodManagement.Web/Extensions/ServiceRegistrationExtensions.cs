using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Service;

namespace FastFoodManagement.Web.Extensions
{
	public static class ServiceRegistrationExtensions
	{
		// Dependency Injection Infrastructure Services
		public static void InfrastructureDJ(this IServiceCollection services)
		{	
			services.AddScoped<IDbFactory, DbFactory>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}

		// Dependency Injection Repositories
		public static void RepositoriesDJ(this IServiceCollection services)
		{
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
		}

		// Dependency Injection Services
		public static void ServicesDJ(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IProductService, ProductService>();
		}
	}
}
