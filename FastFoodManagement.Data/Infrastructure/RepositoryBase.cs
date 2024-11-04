using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private FastFoodManagementDbContext _context = default!;
        private readonly DbSet<T> _dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected FastFoodManagementDbContext DbContext
        {
            get
            {
                return _context ?? (_context = DbFactory.Init());
            }
        }

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbSet = DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteMulti(Expression<Func<T, bool>> where)
        {
            var entities = _dbSet.Where(where).ToList();
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }
        }

        public T GetSingleById(int id)
        {
            return _dbSet.Find(id)!;
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(expression)!;
        }

        public IQueryable<T> GetAll(string[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        public IQueryable<T> GetMulti(Expression<Func<T, bool>> expression, string[]? includes = null)
        {
            IQueryable<T> query = _dbSet.Where(expression);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        public IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> expression, int page, int pageSize, string[]? includes = null)
        {
            IQueryable<T> query = _dbSet.Where(expression);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public int Count(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Count(expression);
        }

        public bool CheckContains(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Any(expression);
        }
    }
}
