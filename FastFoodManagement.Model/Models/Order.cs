using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = default!;

        [Required]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [Required]
        [ForeignKey("PaymentMethod")]
        public int PaymentMethodId { get; set; }

        // Navigation property to Branch
        public virtual Branch Branch { get; set; } = default!;

        // Navigation property to Employee
        public virtual Employee Employee { get; set; } = default!;

        // Navigation property to PaymentMethod
        public virtual PaymentMethod PaymentMethod { get; set; } = default!;

        // Navigation property to OrderItem
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
