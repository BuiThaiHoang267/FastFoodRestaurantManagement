namespace FastFoodManagement.Data.DTO.Order;

public class RetrieveOrderItemDTO
{
    public int Id { get; set; }
	public int OrderId { get; set; }
	public int ProductId { get; set; }
	public string Status { get; set; } = "Pending";
	public string ProductName { get; set; } = default!;
    public decimal ProductPrice { get; set; }
    public decimal ProductCostPrice { get; set; }
    public string? ProductImage { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
	public int OrderNumberOrder { get; set; }
	public List<ComboItemDTO> ProductComboItems { get; set; } = new List<ComboItemDTO>();
}