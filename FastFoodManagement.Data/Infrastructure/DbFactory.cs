using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private FastFoodManagementDbContext? dbContext;
        private readonly DbContextOptions<FastFoodManagementDbContext> options;

        public DbFactory(DbContextOptions<FastFoodManagementDbContext> options)
        {
            this.options = options;
        }

        public FastFoodManagementDbContext Init()
        {
            return dbContext ?? (dbContext = new FastFoodManagementDbContext(options));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
