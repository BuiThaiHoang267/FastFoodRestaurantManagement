using System.ComponentModel.DataAnnotations;

namespace FastFoodManagement.Data.DTO.Branch;

public class CreateBranchDTO
{
    [MaxLength(50)] 
    public string Name { get; set; }
    
    [MaxLength(256)] 
    public string? Location { get; set; }
    
    [MaxLength(15)] 
    [Phone]
    public string Phone { get; set; }
    
    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }
}