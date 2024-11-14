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
	[Table("ComboItems")]
	public class ComboItem : Auditable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public int ComboId { get; set; }

		[Required]
		public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; } = default!;

		// Navigation property to Combo
		[ForeignKey("ComboId")]
		public virtual Product Combo { get; set; } = default!;
	}
}
