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
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteMulti(Expression<Func<T, bool>> where);
        T GetSingleById(int id);
        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[]? includes = null);
        IQueryable<T> GetAll(string[]? includes = null);
        IQueryable<T> GetMulti(Expression<Func<T, bool>> expression, string[]? includes = null);
        IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> expression, int page, int pageSize, string[]? includes = null);
        int Count(Expression<Func<T, bool>> expression);
        bool CheckContains(Expression<Func<T, bool>> expression);
    }
}
