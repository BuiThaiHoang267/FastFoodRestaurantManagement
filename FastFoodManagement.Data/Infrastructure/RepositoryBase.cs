using FastFoodManagement.Model.Abstract;
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
        public FastFoodManagementDbContext _context = default!;
        public readonly DbSet<T> _dbSet;

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

        public async Task Add(T entity)
        {
			await _dbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            _dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
			await Task.CompletedTask;
		}

		public async Task Delete(T entity)
		{
			_dbSet.Remove(entity);
			await Task.CompletedTask;
		}

        public async Task DeleteMulti(Expression<Func<T, bool>> where)
        {
            var entities = _dbSet.Where(where).ToList();
            foreach (var entity in entities)
            {
                _dbSet.Remove(entity);
            }
			await Task.CompletedTask;
		}

        public async Task<T> GetSingleById(int id)
        {
			T? entity = await _dbSet.FindAsync(id);
            if(entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} was not found");
			}
			return entity;
		}

        public async Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[]? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            T? entity = await query.FirstOrDefaultAsync(expression);
            if (entity == null)
            {
                throw new Exception("Entity was not found");
            }
			return entity;
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

		public async Task<int> Count(Expression<Func<T, bool>> expression)
		{
			return await Task.FromResult(_dbSet.Count(expression));
		}

		public async Task<bool> CheckContains(Expression<Func<T, bool>> expression)
		{
			return await Task.FromResult(_dbSet.Any(expression));
		}
    }
}
