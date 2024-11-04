using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
	public interface IComboItemRepository : IRepository<ComboItem>
	{

	}
	public class ComboItemRepository : RepositoryBase<ComboItem>, IComboItemRepository
	{
		public ComboItemRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
	}
}
