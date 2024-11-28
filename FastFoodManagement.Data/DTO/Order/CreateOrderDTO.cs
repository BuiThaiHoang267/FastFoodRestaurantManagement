namespace FastFoodManagement.Data.DTO.Order;

public class CreateOrderDTO
{
    public int NumberOrder { get; set; }
    public int BranchId { get; set; }
    public int PaymentMethodId { get; set; }
}