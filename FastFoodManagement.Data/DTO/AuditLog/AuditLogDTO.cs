namespace FastFoodManagement.Data.DTO.AuditLog
{
	public class AuditLogDTO
	{
		public int Id { get; set; }
		public string UserName { get; set; } = default!;
		public string Action { get; set; } = default!;
		public string TableName { get; set; } = default!;
		public string Description { get; set; } = default!;
		public DateTime CreatedAt { get; set; }
	}
}
