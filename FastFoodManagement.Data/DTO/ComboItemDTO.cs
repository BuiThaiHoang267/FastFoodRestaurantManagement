namespace FastFoodManagement.Data.DTO
{
    public class ComboItemDTO
    {
        public int? ComboId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
		public decimal? ProductPrice { get; set; }
		public int? Quantity { get; set; }
	}
}
