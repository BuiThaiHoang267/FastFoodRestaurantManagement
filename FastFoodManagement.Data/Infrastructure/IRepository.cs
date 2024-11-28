using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteById(int id);
        Task DeleteMulti(Expression<Func<T, bool>> where);
        Task<T> GetSingleById(int id);
        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[]? includes = null);
        IQueryable<T> GetAll(string[]? includes = null);
		IQueryable<T> GetMulti(Expression<Func<T, bool>> expression, string[]? includes = null);
        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> expression, int page, int pageSize, string[]? includes = null);
        Task<int> Count(Expression<Func<T, bool>> expression);
        Task<bool> CheckContains(Expression<Func<T, bool>> expression);
    }
}
