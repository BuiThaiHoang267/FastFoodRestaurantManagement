using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IOrderRepository
    {
        // Add methods here
    }
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
