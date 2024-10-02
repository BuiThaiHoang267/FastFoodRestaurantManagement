using FastFoodManagement.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodManagement.Model.Models
{
    [Table("PurchaseInvoiceItems")]
    public class PurchaseInvoiceItem : Auditable
    {
        public int PurchaseInvoiceId { get; set; }

        public int IngredientId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        // Navigation property to PurchaseInvoice
        public virtual PurchaseInvoice PurchaseInvoice { get; set; } = default!;

        // Navigation property to Ingredient
        public virtual Ingredient Ingredient { get; set; } = default!;
    }
}
