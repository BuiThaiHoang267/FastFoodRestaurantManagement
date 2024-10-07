using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IPositionRepository
    {

    }
    public class PositionRepository : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
