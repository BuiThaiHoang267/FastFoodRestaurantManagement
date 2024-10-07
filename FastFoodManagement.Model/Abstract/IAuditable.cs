using System;


namespace FastFoodManagement.Model.Abstract
{
    public interface IAuditable
    {
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        int? CreatedBy { get; set; }
        int? UpdatedBy { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
