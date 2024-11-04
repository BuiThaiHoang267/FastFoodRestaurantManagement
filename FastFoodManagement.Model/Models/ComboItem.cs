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
		[Required]
		public int ComboId { get; set; }

		[Required]
		public int ProductId { get; set; }

		[Required]
		public int Quantity { get; set; } = default!;

		// Navigation property to Combo
		public virtual Combo Combo { get; set; } = default!;

		// Navigation property to Product
		public virtual Product Product { get; set; } = default!;
	}
}
