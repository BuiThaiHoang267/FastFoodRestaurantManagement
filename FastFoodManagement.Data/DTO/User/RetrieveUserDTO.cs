using FastFoodManagement.Data.DTO.Role;

namespace FastFoodManagement.Data.DTO.User;

public class RetrieveUserDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; }
    public string Phone { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public string RoleCode { get; set; }
    public int BranchId { get; set; }
    public string BranchName { get; set; }
    public bool IsActive { get; set; }
}