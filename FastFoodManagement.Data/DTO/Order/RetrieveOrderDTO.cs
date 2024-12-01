using FastFoodManagement.Data.Enums;

namespace FastFoodManagement.Data.DTO.Order;

public class RetrieveOrderDTO
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
	public int NumberOrder { get; set; }
    public int BranchId { get; set; }
    public int PaymentMethodId { get; set; }
    public string? BranchName { get; set; }
	public string? PaymentMethodName { get; set; }
	public DateTime UpdatedAt { get; set; }
    public ICollection<RetrieveOrderItemDTO> OrderItems { get; set; } = new List<RetrieveOrderItemDTO>();
}