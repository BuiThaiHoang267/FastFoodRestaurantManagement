namespace FastFoodManagement.Data.Enums;

public enum OrderItemStatus
{
    Pending,
    Cooking,
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
            case OrderItemStatus.Cooking:
                return "Cooking";
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
            "Cooking" => OrderItemStatus.Cooking,
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
            "Cooking" => true,
            "Completed" => true,
            "Cancelled" => true,
            _ => false,
        };
    }
}