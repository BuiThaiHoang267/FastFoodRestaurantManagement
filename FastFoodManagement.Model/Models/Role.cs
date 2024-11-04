using FastFoodManagement.Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace FastFoodManagement.Model.Models
{
    [Table("Roles")]
    public class Role : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;

        [MaxLength(256)]
        public string? Description { get; set; }

        // Navigation property to User
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
