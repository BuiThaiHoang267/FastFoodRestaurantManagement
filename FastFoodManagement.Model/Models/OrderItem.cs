using FastFoodManagement.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodManagement.Model.Models
{
    [Table("OrderItems")]
    public class OrderItem: Auditable
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
        [ForeignKey("Order")]
		public int OrderId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string ItemType { get; set; } = default!;

		[Required]
		[MaxLength(50)]
		public string Status { get; set; } = default!;

		[Required]
        public int Quantity { get; set; }

		[Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        //Navigation property to Order
        public virtual Order Order { get; set; } = default!;
	}
}
