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
    [Table("Branches")]
    public class Branch : Auditable
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

        public bool IsActive { get; set; } = true;

        // Navigation property to User
        public virtual ICollection<User> Users { get; set; } = new List<User>();

        // Navigation property to Order
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        // Navigation property to PurchaseInvoice
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();

        // Navigation property to Employee
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        // Navigation property to Supplier
        public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
    }
}
