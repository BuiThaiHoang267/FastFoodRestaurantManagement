namespace FastFoodManagement.Data.DTO.User;

public class RegisterUserDTO
{
    public string Name { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int RoleId { get; set; }
    public int BranchId { get; set; }
}