namespace FastFoodManagement.Data.DTO
{
    public class ComboItemDTO
    {
        public int? ComboId { get; set; }
        public int? ProductId { get; set; }
        public string? Name { get; set; }
		public decimal? Price { get; set; }
		public int? Quantity { get; set; }
    }
}
