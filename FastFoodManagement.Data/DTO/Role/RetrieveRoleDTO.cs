namespace FastFoodManagement.Data.DTO.Role;

public class RetrieveRoleDTO
{
    public int Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}