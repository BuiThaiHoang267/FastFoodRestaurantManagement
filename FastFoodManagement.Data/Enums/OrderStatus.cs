namespace FastFoodManagement.Data.Enums;

public enum OrderStatus
{
    Pending,
    Completed,
    Cancelled
}

public static class OrderStatusExtensions
{
    public static string ToStringValue(this OrderStatus status)
    {
        switch (status)
        {
            case OrderStatus.Pending:
                return "Pending";
            case OrderStatus.Completed:
                return "Completed";
            case OrderStatus.Cancelled:
                return "Cancelled";
            default:
                return string.Empty;
        }
    }

    public static OrderStatus FromStringValue(string status)
    {
        return status switch
        {
            "Pending" => OrderStatus.Pending,
            "Completed" => OrderStatus.Completed,
            "Cancelled" => OrderStatus.Cancelled,
            _ => throw new ArgumentException("Invalid status value", nameof(status)),
        };
    }
    
    public static bool IsValidStatus(string status)
    {
        return status switch
        {
            "Pending" => true,
            "Completed" => true,
            "Cancelled" => true,
            _ => false,
        };
    }
}