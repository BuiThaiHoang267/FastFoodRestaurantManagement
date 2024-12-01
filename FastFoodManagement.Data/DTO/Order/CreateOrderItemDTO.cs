namespace FastFoodManagement.Data.DTO.Order;

public class CreateOrderItemDTO
{
    public int ProductId { get; set; }
    public string Status { get; set; } = "Pending";
	public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}