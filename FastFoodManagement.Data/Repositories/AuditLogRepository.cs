using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Model.Models;

namespace FastFoodManagement.Data.Repositories
{
    public interface IAuditLogRepository : IRepository<AuditLog>
	{
		// Add more specific methods here when necessary
	}
	public class AuditLogRepository : RepositoryBase<AuditLog>, IAuditLogRepository
	{
		public AuditLogRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
    }
}
