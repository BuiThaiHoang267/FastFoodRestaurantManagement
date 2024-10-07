using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Data.Repositories
{
    public interface IEmployeeRepository
    {
        // Add more specific methods here when necessary
    }
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
