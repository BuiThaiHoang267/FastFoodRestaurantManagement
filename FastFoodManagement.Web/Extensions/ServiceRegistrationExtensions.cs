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
			services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
			services.AddScoped<IBranchRepository, BranchRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IOrderItemRepository, OrderItemRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IComboItemRepository, ComboItemRepository>();
		}

		// Dependency Injection Services
		public static void ServicesDJ(this IServiceCollection services)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IPaymentMethodService, PaymentMethodService>();
			services.AddScoped<IBranchService, BranchService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IStatisticsService, StatisticsService>();
		}
	}
}
