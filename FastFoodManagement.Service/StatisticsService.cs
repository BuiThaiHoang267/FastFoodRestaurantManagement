using FastFoodManagement.Data.DTO.Statistic;
using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Service
{
	public interface IStatisticsService
	{
		public Task<decimal> CalculateTotalProfitAsync(IEnumerable<Order> orders);
		public Task<ResultSaleTodayDTO> StatisticsSaleToday();
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
	}
}
