namespace FastFoodManagement.Data.DTO.Order;

public class CreateOrderDTO
{
    public int NumberOrder { get; set; }
    public int BranchId { get; set; }
    public int PaymentMethodId { get; set; }
    public int TotalPrice { get; set; }
	public List<CreateOrderItemDTO> OrderItems { get; set; } = new List<CreateOrderItemDTO>();
}