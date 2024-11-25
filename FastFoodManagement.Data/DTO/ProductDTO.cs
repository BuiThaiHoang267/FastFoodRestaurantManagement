namespace FastFoodManagement.Data.DTO
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string? Type { get; set; }
		public decimal Price { get; set; }
		public decimal? CostPrice { get; set; }
		public string? Image { get; set; }
		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public List<ComboItemDTO>? ComboItems { get; set; }
	}
}
