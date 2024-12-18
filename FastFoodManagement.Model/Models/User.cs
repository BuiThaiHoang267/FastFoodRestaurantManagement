﻿using FastFoodManagement.Model.Abstract;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFoodManagement.Model.Models
{
    [Table("Users")]
    [Index(nameof(Username), IsUnique = true)]
    public class User : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = default!;

        [Required]
        [MaxLength(256)]
        public string Password { get; set; } = default!;

        [MaxLength(15)]
        public string? Phone { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Required]
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        // Navigation property to Role
        public virtual Role Role { get; set; } = default!;

        // Navigation property to Branch
        public virtual Branch Branch { get; set; } = default!;
	}
}
