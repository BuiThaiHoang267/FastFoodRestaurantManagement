using FastFoodManagement.Web.DTO;

namespace FastFoodManagement.Web.ViewModels
{
	public class ProductDTO
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? Type { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
		public decimal? CostPrice { get; set; }
		public string? Image { get; set; }
		public int? CategoryId { get; set; }
		public List<ComboItemDTO>? ComboItems { get; set; }
	}
}
