using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        // Add methods here
    }
    public class IngredientRepository : RepositoryBase<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
