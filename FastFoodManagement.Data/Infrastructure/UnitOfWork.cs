using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private FastFoodManagementDbContext? dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public FastFoodManagementDbContext DbContext => dbContext ??= dbFactory.Init();

        public void Commit()
        {
            DbContext.SaveChanges();
        }

		public async Task CommitAsync()
		{
			await DbContext.SaveChangesAsync();
		}
    }
}
