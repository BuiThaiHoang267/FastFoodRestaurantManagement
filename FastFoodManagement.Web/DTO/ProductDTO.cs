namespace FastFoodManagement.Web.ViewModels
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Type { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public string? Image { get; set; }
		public int CategoryId { get; set; }
	}
}
