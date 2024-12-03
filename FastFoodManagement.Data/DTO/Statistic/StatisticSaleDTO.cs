using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Data.DTO.Statistic
{
    public class StatisticSaleDTO
    {
        public decimal TotalRevenue { get; set; }
		public decimal TotalProfit { get; set; }
        public decimal TotalCost { get; set; }
		public int TotalOrder { get; set; }
        
        public List<decimal> Revenue { get; set; } = new List<decimal>();
		public List<decimal> Profit { get; set; } = new List<decimal>();
		public List<decimal> Cost { get; set; } = new List<decimal>();
		public List<string> Labels { get; set; } = new List<string>();
		public AxisChartDTO RevenueByBranch { get; set; } = new AxisChartDTO();
	}

	public class StatisticProductDTO
	{
		public AxisChartDTO TopProductByRevenue { get; set; } = new AxisChartDTO();
		public AxisChartDTO TopProductBySale { get; set; } = new AxisChartDTO();
		public AxisChartDTO TopProductByProfit { get; set; } = new AxisChartDTO();
	}
}
