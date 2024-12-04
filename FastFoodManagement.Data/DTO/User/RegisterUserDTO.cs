namespace FastFoodManagement.Data.DTO.User;

public class RegisterUserDTO
{
    public string Name { get; set; } = default!;
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int RoleId { get; set; }
    public int BranchId { get; set; }
}