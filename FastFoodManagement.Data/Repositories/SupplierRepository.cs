using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface ISupplierRepository
    {

    }
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
