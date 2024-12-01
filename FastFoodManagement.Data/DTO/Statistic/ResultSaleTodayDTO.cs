namespace FastFoodManagement.Data.DTO.Statistic
{
	public class ResultSaleTodayDTO
	{
		public decimal TotalRevenueToday { get; set; }
		public decimal TotalProfitToday { get; set; }
		public int TotalOrdersToday { get; set; }

		public decimal TotalRevenueYesterday { get; set; }
		public decimal TotalProfitYesterday { get; set; }
		public int TotalOrdersYesterday { get; set; }

		public float PercentRevenueChange => CalculatePercentageChange(TotalRevenueYesterday, TotalRevenueToday);
		public float PercentProfitChange => CalculatePercentageChange(TotalProfitYesterday, TotalProfitToday);
		public float PercentOrdersChange => CalculatePercentageChange(TotalOrdersYesterday, TotalOrdersToday);

		public float CalculatePercentageChange(decimal oldValue, decimal newValue)
		{
			if (oldValue == 0)
			{
				return newValue == 0 ? 0 : newValue > 0 ? 100 : -100;
			}
			return (float)Math.Round((newValue - oldValue) / oldValue * 100, 2);
		}
	}
}
