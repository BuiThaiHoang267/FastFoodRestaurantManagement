using FastFoodManagement.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Model.Models
{
	[Table("AuditLogs")]
	public class AuditLog : Auditable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string UserName { get; set; } = default!;

		[Required]
		[MaxLength(50)]
		public string Action { get; set; } = default!;

		[Required]
		[MaxLength(50)]
		public string TableName { get; set; } = default!;

		public string Description { get; set; } = default!;
	}
}
