using FastFoodManagement.Data.DTO.Statistic;
using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Service
{
	public interface IStatisticsService
	{
		public Task<decimal> CalculateTotalProfitAsync(IEnumerable<Order> orders);
		public Task<ResultSaleTodayDTO> StatisticsSaleToday();
		public Task<StatisticRevenueChartDTO> StatisticsRevenueChart(DateRangeDTO date);
		public Task<TopProductDTO> Top10Product(DateRangeDTO date);
	}
	public class StatisticsService : IStatisticsService
	{
		private IUnitOfWork _unitOfWork;
		private IOrderRepository _orderRepository;
		private IOrderItemRepository _orderItemRepository;
		private IProductRepository _productRepository;

		public StatisticsService(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IProductRepository productRepository)
		{
			_unitOfWork = unitOfWork;
			_orderRepository = orderRepository;
			_orderItemRepository = orderItemRepository;
			_productRepository = productRepository;
		}

		public async Task<decimal> CalculateTotalProfitAsync(IEnumerable<Order> orders)
		{
			var orderIds = orders.Select(o => o.Id).ToList();

			var orderItems = await _orderItemRepository.GetMulti(oi => orderIds.Contains(oi.OrderId), new[] { "Product" }).ToListAsync();

			decimal totalProfit = 0;

			foreach (var orderItem in orderItems)
			{
				totalProfit += orderItem.Quantity * (orderItem.Product.Price - orderItem.Product.CostPrice);
			}

			return totalProfit;
		}

		public async Task<ResultSaleTodayDTO> StatisticsSaleToday()
		{
			var today = DateTime.Now.Date;
			var yesterday = today.AddDays(-1);

			var ordersToday = await _orderRepository.GetMulti(o => o.CreatedAt.HasValue && o.CreatedAt.Value.Date == today).ToListAsync();
			var totalRevenueToday = ordersToday.Sum(o => o.TotalPrice);
			var totalProfitToday = await CalculateTotalProfitAsync(ordersToday);
			var totalOrdersToday = ordersToday.Count;

			var ordersYesterday = await _orderRepository.GetMulti(o => o.CreatedAt.HasValue && o.CreatedAt.Value.Date == yesterday).ToListAsync();
			var totalRevenueYesterday = ordersYesterday.Sum(o => o.TotalPrice);
			var totalProfitYesterday = await CalculateTotalProfitAsync(ordersYesterday);
			var totalOrdersYesterday = ordersYesterday.Count;

			var result = new ResultSaleTodayDTO
			{
				TotalRevenueToday = totalRevenueToday,
				TotalProfitToday = totalProfitToday,
				TotalOrdersToday = totalOrdersToday,
				TotalRevenueYesterday = totalRevenueYesterday,
				TotalProfitYesterday = totalProfitYesterday,
				TotalOrdersYesterday = totalOrdersYesterday
			};

			return result;
		}

		public async Task<StatisticRevenueChartDTO> StatisticsRevenueChart(DateRangeDTO date)
		{
			var orders = await _orderRepository.GetMulti(
				o => o.CreatedAt.HasValue && o.CreatedAt >= date.StartDate && o.CreatedAt <= date.EndDate)
				.ToListAsync();

			var totalRevenue = orders.Sum(o => o.TotalPrice);
			var RevenueChartByDay = new AxisChartDTO();
			var RevenueChartByDate = new AxisChartDTO();
			var RevenueChartByTime = new AxisChartDTO();

			var groupByDay = orders.GroupBy(o => o.CreatedAt.Value.DayOfWeek).OrderBy(g => (int)g.Key);
			foreach (var group in groupByDay)
			{
				RevenueChartByDay.Labels.Add(group.Key.ToString());
				RevenueChartByDay.Data.Add(group.Sum(o => o.TotalPrice));
			}

			var groupByDate = orders.GroupBy(o => o.CreatedAt.Value.Date);
			foreach (var group in groupByDate)
			{
				RevenueChartByDate.Labels.Add(group.Key.ToString("dd/MM"));
				RevenueChartByDate.Data.Add(group.Sum(o => o.TotalPrice));
			}

			var groupByTime = orders.GroupBy(o => o.CreatedAt.Value.Hour).OrderBy(g => (int)g.Key);
			foreach (var group in groupByTime)
			{
				var formattedTime = new TimeSpan(group.Key, 0, 0).ToString(@"hh\:mm");
				RevenueChartByTime.Labels.Add(formattedTime);
				RevenueChartByTime.Data.Add(group.Sum(o => o.TotalPrice));
			}

			var result = new StatisticRevenueChartDTO
			{
				TotalRevenue = totalRevenue,
				RevenueChartByDay = RevenueChartByDay,
				RevenueChartByDate = RevenueChartByDate,
				RevenueChartByTime = RevenueChartByTime
			};
			return result;
		}

		public async Task<TopProductDTO> Top10Product(DateRangeDTO date)
		{
			var orders = await _orderRepository.GetMulti(
				o => o.CreatedAt.HasValue && o.CreatedAt >= date.StartDate && o.CreatedAt <= date.EndDate,
				new[] {"OrderItems", "OrderItems.Product"})
				.ToListAsync();

			// top 10 product by revenue
			var topProductByRevenue = orders.SelectMany(o => o.OrderItems)
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
			var topProductByRevenueChart = new AxisChartDTO();
			foreach (var product in topProductByRevenue)
			{
				topProductByRevenueChart.Labels.Add(product.ProductName);
				topProductByRevenueChart.Data.Add(product.TotalRevenue);
			}

			// top 10 product by sale
			var topProductBySale = orders.SelectMany(o => o.OrderItems)
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
			var topProductBySaleChart = new AxisChartDTO();
			foreach (var product in topProductBySale)
			{
				topProductBySaleChart.Labels.Add(product.ProductName);
				topProductBySaleChart.Data.Add(product.TotalSale);
			}

			var result = new TopProductDTO
			{
				TopProductByRevenue = topProductByRevenueChart,
				TopProductBySale = topProductBySaleChart
			};

			return result;
		}
	}
}
