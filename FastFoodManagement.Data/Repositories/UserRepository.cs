using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IUserRepository
    {

    }
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
