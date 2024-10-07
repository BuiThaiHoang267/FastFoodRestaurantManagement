using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;
using System;
using System.Linq;


namespace FastFoodManagement.Data.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        // Add more specific methods here when necessary
    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
