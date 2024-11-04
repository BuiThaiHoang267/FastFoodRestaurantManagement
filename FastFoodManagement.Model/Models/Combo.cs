using FastFoodManagement.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodManagement.Model.Models
{
	[Table("Combos")]
	public class Combo: Auditable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; } = default!;

		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[MaxLength(256)]
		public string? Image { get; set; }

		[Required]
		public string Type { get; set; } = "Combo";

		[Required]
		[ForeignKey("Category")]
		public int CategoryId { get; set; }

		// Navigation property to Category
		public virtual Category Category { get; set; } = default!;

		// Navigation property to ComboItem
		public virtual ICollection<ComboItem> ComboItems { get; set; } = new List<ComboItem>();
	}
}
