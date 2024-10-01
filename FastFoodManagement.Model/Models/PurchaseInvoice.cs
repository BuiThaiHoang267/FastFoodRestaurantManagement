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
    [Table("PurchaseInvoices")]
    public class PurchaseInvoice: Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        // Navigation property to Branch
        public virtual Branch Branch { get; set; } = default!;

        // Navigation property to Supplier
        public virtual Supplier Supplier { get; set; } = default!;

        // Navigation property to Employee
        public virtual Employee Employee { get; set; } = default!;

        // Navigation property to PurchaseInvoiceItem
        public virtual ICollection<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; } = new List<PurchaseInvoiceItem>();
    }
}
