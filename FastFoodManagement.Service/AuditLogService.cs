using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagement.Service
{
    public interface IAuditLogService
	{
		public Task AddAuditLogAsync(string username, string action, string tableName, string des);
		public Task<List<AuditLog>> GetAuditLogRecent();
		public Task SuspendChanges();
	}
	public class AuditLogService : IAuditLogService
	{
		private readonly IAuditLogRepository _auditLogRepository;
		private readonly IUnitOfWork _unitOfWork;
		public AuditLogService(IAuditLogRepository auditLogRepository, IUnitOfWork unitOfWork)
		{
			_auditLogRepository = auditLogRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task AddAuditLogAsync(string username, string action, string tableName, string des)
		{
			var auditLog = new AuditLog
			{
				UserName = username,
				Action = action,
				TableName = tableName,
				Description = des
			};
			await _auditLogRepository.Add(auditLog);
			await SuspendChanges();
		}

		public async Task<List<AuditLog>> GetAuditLogRecent()
		{
			// Get the most recent audit log (quantity = 50)
			return await _auditLogRepository.GetAll().Take(50).OrderByDescending(a => a.CreatedAt).ToListAsync();
		}

		public async Task SuspendChanges()
		{
			await _unitOfWork.CommitAsync();
		}
	}
}
