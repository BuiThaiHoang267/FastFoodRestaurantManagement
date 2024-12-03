using FastFoodManagement.Data.DTO.Statistic;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFoodManagement.Service
{
	public interface IStatisticalReportService
	{
		public Task<StatisticSaleDTO> StatisticsSale(string? branchId, DateRangeDTO date);

		public Task<StatisticProductDTO> StatisticsProduct(DateRangeDTO date, string? branchId, string? categoryId);
	}
	public class StatisticalReportService: IStatisticalReportService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderItemRepository _orderItemRepository;
		private readonly IProductRepository _productRepository;

		public StatisticalReportService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository)
		{
			_orderRepository = orderRepository;
			_orderItemRepository = orderItemRepository;
			_productRepository = productRepository;
		}

		

		public async Task<StatisticSaleDTO> StatisticsSale(string? branchId, DateRangeDTO date)
		{
			// Lấy danh sách đơn hàng dựa trên branchId và thời gian
			List<Order> orders = branchId == null
				? await _orderRepository.GetMulti(
					o => o.CreatedAt.HasValue && o.CreatedAt >= date.StartDate && o.CreatedAt <= date.EndDate,
					new[] { "OrderItems", "OrderItems.Product" })
					.ToListAsync()
				: await _orderRepository.GetMulti(
					o => o.CreatedAt.HasValue && o.CreatedAt >= date.StartDate && o.CreatedAt <= date.EndDate && o.BranchId == int.Parse(branchId),
					new[] { "OrderItems", "OrderItems.Product" })
					.ToListAsync();

			// Tính toán tổng doanh thu, lợi nhuận, chi phí và số lượng đơn hàng
			var TotalRevenue = orders.Sum(o => o.TotalPrice);
			var TotalProfit = orders.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * (oi.Product.Price - oi.Product.CostPrice)));
			var TotalCost = TotalRevenue - TotalProfit;
			var TotalOrder = orders.Count;

			// Các danh sách lưu trữ dữ liệu cho biểu đồ
			var Revenue = new List<decimal>();
			var Profit = new List<decimal>();
			var Cost = new List<decimal>();
			var Labels = new List<string>();

			// Tính toán thời lượng giữa ngày bắt đầu và ngày kết thúc
			var durationDate = date.EndDate.Date - date.StartDate.Date;

			// Nhóm dữ liệu dựa trên khoảng thời gian
			var groupByDate = durationDate.Days >= 365
			? orders.GroupBy(o => (o.CreatedAt.Value.Year, 0, 0)) // Chỉ dùng Year
			: durationDate.Days >= 32
				? orders.GroupBy(o => (o.CreatedAt.Value.Year, o.CreatedAt.Value.Month, 0)) // Year và Month
				: orders.GroupBy(o => (o.CreatedAt.Value.Year, o.CreatedAt.Value.Month, o.CreatedAt.Value.Day)); // Full Date


			// Tính toán doanh thu, lợi nhuận, chi phí theo từng nhóm và thiết lập nhãn
			foreach (var group in groupByDate)
			{
				Revenue.Add(group.Sum(o => o.TotalPrice));
				Profit.Add(group.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * (oi.Product.Price - oi.Product.CostPrice))));
				Cost.Add(group.Sum(o => o.TotalPrice) - group.Sum(o => o.OrderItems.Sum(oi => oi.Quantity * (oi.Product.Price - oi.Product.CostPrice))));

				var (year, month, day) = group.Key;

				if (durationDate.Days >= 365)
				{
					Labels.Add(year.ToString());
				}
				else if (durationDate.Days >= 32)
				{
					Labels.Add($"{month}-{year}");
				}
				else
				{
					Labels.Add($"{day}-{month}");
				}
			}

			// Tính toán doanh thu theo chi nhánh
			var RevenueByBranch = StatisticRevenueByBranch(date);

			var result = new StatisticSaleDTO
			{
				TotalRevenue = TotalRevenue,
				TotalProfit = TotalProfit,
				TotalCost = TotalCost,
				TotalOrder = TotalOrder,
				Revenue = Revenue,
				Profit = Profit,
				Cost = Cost,
				Labels = Labels,
				RevenueByBranch = RevenueByBranch
			};

			return result;
		}

		private AxisChartDTO StatisticRevenueByBranch(DateRangeDTO date)
		{
			var orders = _orderRepository.GetMulti(
				o => o.CreatedAt.HasValue && o.CreatedAt >= date.StartDate && o.CreatedAt <= date.EndDate,
				new[] { "Branch" })
				.ToList();

			var groupByBranch = orders.GroupBy(o => o.BranchId);
			var RevenueByBranch = new AxisChartDTO();
			foreach (var group in groupByBranch)
			{
				RevenueByBranch.Labels.Add(group.First().Branch.Name);
				RevenueByBranch.Data.Add(group.Sum(o => o.TotalPrice));
			}

			return RevenueByBranch;
		}

		public async Task<StatisticProductDTO> StatisticsProduct(DateRangeDTO date, string? branchId, string? categoryId)
		{
			var query = _orderItemRepository.GetMulti(
				oi => oi.Order.CreatedAt.HasValue && oi.Order.CreatedAt >= date.StartDate && oi.Order.CreatedAt <= date.EndDate,
				new[] { "Product" });

			if(branchId != null && branchId != "")
			{
				query = query.Where(oi => oi.Order.BranchId == int.Parse(branchId));
			}

			if (categoryId != null && categoryId != "")
			{
				query = query.Where(oi => oi.Product.CategoryId == int.Parse(categoryId));
			}

			var orderItems = await query.ToListAsync();

			// Top 10 sản phẩm theo doanh thu
			var TopProductByRevenue = orderItems
				.GroupBy(oi => oi.ProductId)
				.Select(g => new
				{
					ProductName = g.First().Product.Name,
					ProductId = g.Key,
					TotalRevenue = g.Sum(oi => oi.Quantity * oi.Product.Price)
				})
				.OrderByDescending(g => g.TotalRevenue)
				.Take(10)
				.ToList();
			var TopProductByRevenueChart = new AxisChartDTO();
			foreach (var product in TopProductByRevenue)
			{
				TopProductByRevenueChart.Labels.Add(product.ProductName);
				TopProductByRevenueChart.Data.Add(product.TotalRevenue);
			}

			// Top 10 sản phẩm theo số lượng bán
			var TopProductBySale = orderItems
				.GroupBy(oi => oi.ProductId)
				.Select(g => new
				{
					ProductName = g.First().Product.Name,
					ProductId = g.Key,
					TotalSale = g.Sum(oi => oi.Quantity)
				})
				.OrderByDescending(g => g.TotalSale)
				.Take(10)
				.ToList();
			var TopProductBySaleChart = new AxisChartDTO();
			foreach (var product in TopProductBySale)
			{
				TopProductBySaleChart.Labels.Add(product.ProductName);
				TopProductBySaleChart.Data.Add(product.TotalSale);
			}

			// Top 10 sản phẩm theo lợi nhuận
			var TopProductBySaleSale = orderItems
				.GroupBy(oi => oi.ProductId)
				.Select(g => new
				{
					ProductName = g.First().Product.Name,
					ProductId = g.Key,
					TotalProfit = g.Sum(oi => oi.Quantity * (oi.Product.Price - oi.Product.CostPrice))
				})
				.OrderByDescending(g => g.TotalProfit)
				.Take(10)
				.ToList();
			var TopProductByProfitChart = new AxisChartDTO();
			foreach (var product in TopProductBySaleSale)
			{
				TopProductByProfitChart.Labels.Add(product.ProductName);
				TopProductByProfitChart.Data.Add(product.TotalProfit);
			}

			var result = new StatisticProductDTO
			{
				TopProductByRevenue = TopProductByRevenueChart,
				TopProductBySale = TopProductBySaleChart,
				TopProductByProfit = TopProductByProfitChart
			};

			return result;
		}
	}
}
