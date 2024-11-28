using System.ComponentModel.DataAnnotations;

namespace FastFoodManagement.Data.DTO.Branch;

public class CreateBranchDTO
{
    public string Name { get; set; }
    public string? Location { get; set; }
    [Phone]
    public string Phone { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
}