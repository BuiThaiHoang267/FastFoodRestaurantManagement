namespace FastFoodManagement.Data.DTO.Branch;

public class RetrieveBranchDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int IsActive { get; set; }
}