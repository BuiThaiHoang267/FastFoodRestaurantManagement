using System.ComponentModel.DataAnnotations;

namespace FastFoodManagement.Data.DTO.User;

public class UpdateUserDTO
{
    [MaxLength(50)]
    public string? Name { get; set; }
    
    [MaxLength(15)]
    public string? Phone { get; set; }
    
    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }
    
    public bool? IsActive { get; set; }
    
    public int? RoleId { get; set; }
    
    public int? BranchId { get; set; }
}