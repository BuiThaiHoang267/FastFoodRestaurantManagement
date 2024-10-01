using FastFoodManagement.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodManagement.Model.Models
{
    [Table("Employees")]
    public class Employee : Auditable
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;

        [MaxLength(256)]
        public string? Location { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(15)]
        public string? CCCD { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        [Required]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        [ForeignKey("Position")]
        public int? PositionId { get; set; }

        // Navigation property to Branch
        public virtual Branch Branch { get; set; } = default!;

        // Navigation property to Position
        public virtual Position Position { get; set; } = default!;

        // Navigation property to Order
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        // Navigation property to PurchaseInvoice
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
    }
}
