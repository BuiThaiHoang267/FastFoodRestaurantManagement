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

    public class StatisticRevenueChartDTO
    {
        public decimal TotalRevenue { get; set; }

		public AxisChartDTO RevenueChartByDay { get; set; } = new AxisChartDTO();
		public AxisChartDTO RevenueChartByDate { get; set; } = new AxisChartDTO();
		public AxisChartDTO RevenueChartByTime { get; set; } = new AxisChartDTO();
	}

	public class TopProductDTO
	{
		public AxisChartDTO TopProductByRevenue { get; set; } = new AxisChartDTO();
		public AxisChartDTO TopProductBySale { get; set; } = new AxisChartDTO();
	}

	public class DateRangeDTO
	{
		private DateTime _startDate;
		private DateTime _endDate;

		// Getter và Setter cho StartDate
		public DateTime StartDate
		{
			get => _startDate.ToLocalTime();  // Khi lấy giá trị, chuyển sang múi giờ địa phương
			set => _startDate = value.ToUniversalTime();  // Khi gán giá trị, chuyển thành UTC
		}

		// Getter và Setter cho EndDate
		public DateTime EndDate
		{
			get => _endDate.ToLocalTime();  // Khi lấy giá trị, chuyển sang múi giờ địa phương
			set => _endDate = value.ToUniversalTime();  // Khi gán giá trị, chuyển thành UTC
		}
	}

	public class AxisChartDTO
	{
		public List<string> Labels { get; set; } = new List<string>();
		public List<decimal> Data { get; set; } = new List<decimal>();
	}
}
