
using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
	public interface IComboRepository : IRepository<Combo>
	{

	}
	public class ComboRepository : RepositoryBase<Combo>, IComboRepository
	{
		public ComboRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
	}
}
