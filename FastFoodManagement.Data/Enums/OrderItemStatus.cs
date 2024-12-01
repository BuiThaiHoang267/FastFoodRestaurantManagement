namespace FastFoodManagement.Data.Enums;

public enum OrderItemStatus
{
    Pending,
    Cooked,
    Completed,
    Cancelled
}

public static class OrderItemStatusExtensions
{
    public static string ToStringValue(this OrderItemStatus status)
    {
        switch (status)
        {
            case OrderItemStatus.Pending:
                return "Pending";
            case OrderItemStatus.Cooked:
                return "Cooked";
            case OrderItemStatus.Completed:
                return "Completed";
            case OrderItemStatus.Cancelled:
                return "Cancelled";
            default:
                return string.Empty;
        }
    }

    public static OrderItemStatus FromStringValue(string status)
    {
        return status switch
        {
            "Pending" => OrderItemStatus.Pending,
            "Cooked" => OrderItemStatus.Cooked,
            "Completed" => OrderItemStatus.Completed,
            "Cancelled" => OrderItemStatus.Cancelled,
            _ => throw new ArgumentException("Invalid status value", nameof(status)),
        };
    }
    
    public static bool IsValidStatus(string status)
    {
        return status switch
        {
            "Pending" => true,
            "Cooked" => true,
            "Completed" => true,
            "Cancelled" => true,
            _ => false,
        };
    }
}