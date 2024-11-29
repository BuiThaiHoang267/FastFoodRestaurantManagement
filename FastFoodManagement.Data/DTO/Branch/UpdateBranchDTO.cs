using System.ComponentModel.DataAnnotations;

namespace FastFoodManagement.Data.DTO.Branch;

public class UpdateBranchDTO
{
    [MaxLength(50)] 
    public string? Name { get; set; }
    
    [MaxLength(256)] 
    public string? Location { get; set; }
    
    [MaxLength(15)] 
    [Phone]
    public string? Phone { get; set; }
    
    [MaxLength(100)]
    [EmailAddress]
    public string? Email { get; set; }
    
    public bool? IsActive { get; set; }
}