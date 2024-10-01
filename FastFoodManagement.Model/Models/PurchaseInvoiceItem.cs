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
    [Table("PurchaseInvoiceItems")]
    public class PurchaseInvoiceItem : Auditable
    {
        [Key, Column(Order = 0)]
        public int PurchaseInvoiceId { get; set; }

        [Key, Column(Order = 1)]
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
