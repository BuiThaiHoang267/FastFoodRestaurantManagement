namespace FastFoodManagement.Data.DTO.Order;

public class UpdateOrderItemDTO
{
    public string? Status { get; set; }
    public int? Quantity { get; set; }
    public decimal? UnitPrice { get; set; }
}