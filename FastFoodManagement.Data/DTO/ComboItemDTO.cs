namespace FastFoodManagement.Data.DTO
{
    public class ComboItemDTO
    {
        public int? ComboId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; } // Product.Name
		public decimal? ProductPrice { get; set; } // Product.Price
		public int? Quantity { get; set; }
	}
}
