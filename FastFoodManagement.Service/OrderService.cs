using FastFoodManagement.Data.Enums;
using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FastFoodManagement.Service;

public interface IOrderService
{
    public Task<List<Order>> GetAllOrders();
    public Task<Order> GetOrderById(int id);
    public Task<List<Order>> GetOrderByFilters(string? id, string? paymentMethods, string? branches, string? startDate, string? endDate);
    public Task<Order> CreateOrder(Order order);
    public Task AddOrder(Order order);
    public Task DeleteOrderById(int id);
    public Task UpdateOrder(Order order);
    public Task<List<OrderItem>> GetAllOrderItemsByOrderId(int id);
    public Task<OrderItem> GetOrderItemById(int id);
    public Task AddOneOrderItem(OrderItem orderItem);
    public Task DeleteOrderItemById(int id);
    public Task UpdateOrderItem(OrderItem orderItem);
    public Task RecalculateTotalPrice(int orderId);
    public void SaveChanges();
    public Task SuspendChanges();
}

public class OrderService : IOrderService
{
    private IOrderRepository _orderRepository;
    private IOrderItemRepository _orderItemRepository;
    private IPaymentMethodRepository _paymentMethodRepository;
    private IBranchRepository _branchRepository;
    private IProductRepository _productRepository;
    private IUnitOfWork _unitOfWork;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IBranchRepository branchRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _branchRepository = branchRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<Order>> GetAllOrders()
    {
        var orders = await _orderRepository
            .GetMulti(
                o => true, 
                new string[] { "OrderItems.Product", "Branch", "PaymentMethod", "OrderItems.Product.ComboItems", "OrderItems.Product.ComboItems.Product" })
            .ToListAsync();
    
        return orders;
    }

    public async Task<Order> GetOrderById(int id)
    {
        var order = await _orderRepository
            .GetMulti(o => o.Id == id, new string[] { "OrderItems.Product" })
            .FirstOrDefaultAsync();

        if (order == null)
        {
            throw new KeyNotFoundException("Order not found");
        }

        return order;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        // Check if BranchId exists
        var branch = await _branchRepository.GetSingleById(order.BranchId);
        if (branch == null)
        {
            throw new ArgumentException("The specified branch does not exist.");
        }

        // Check if PaymentMethodId exists
        var paymentMethod = await _paymentMethodRepository.GetSingleById(order.PaymentMethodId);
        if (paymentMethod == null)
        {
            throw new ArgumentException("The specified payment method does not exist.");
        }

        // Proceed with creating the order
        await _orderRepository.Add(order);
        await SuspendChanges();

        return order;
    
    }

    public async Task AddOrder(Order order)
    {
        await _orderRepository.Add(order);
        await SuspendChanges();
    }

    public async Task DeleteOrderById(int id)
    {
        var orderItems = await _orderItemRepository.GetMulti(item => item.OrderId == id, null).ToListAsync();
        foreach (var orderItem in orderItems)
        {
            await _orderItemRepository.DeleteById(orderItem.Id);
        }
        
        await _orderRepository.DeleteById(id);
        await SuspendChanges();
    }

    public async Task UpdateOrder(Order order)
    {
        await _orderRepository.Update(order);
        await SuspendChanges();
    }

    public Task<List<OrderItem>> GetAllOrderItemsByOrderId(int id)
    {
        return _orderItemRepository
            .GetMulti(item => item.OrderId == id, new string[] {"Product"})
            .ToListAsync();
    }

    public async Task<OrderItem> GetOrderItemById(int id)
    {
        var orderItem = await _orderItemRepository
            .GetMulti(item => item.Id == id, new []{"Product"})
            .FirstOrDefaultAsync();
        if (orderItem == null)
        {
            throw new KeyNotFoundException("Order item not found");
        }

        return orderItem;
    }

    public async Task AddOneOrderItem(OrderItem orderItem)
    {
        // Check if OrderId exists
        var order = await _orderRepository.GetMulti(
            o => o.Id == orderItem.OrderId,
            new string[] { "OrderItems" })
            .FirstOrDefaultAsync();
        if (order == null)
        {
            throw new ArgumentException("The specified order does not exist.");
        }
        
        // Check if ProductId exists
        var product = await _productRepository.GetSingleById(orderItem.ProductId);
        if (product == null)
        {
            throw new ArgumentException("The specified product does not exist.");
        }
        
        // Check if productId and orderId exists in the orderItem table
        var orderItemExists = await _orderItemRepository
            .GetMulti(item => item.OrderId == orderItem.OrderId && item.ProductId == orderItem.ProductId, null)
            .FirstOrDefaultAsync();
        if (orderItemExists != null)
        {
            orderItemExists.Quantity += 1;
            await _orderItemRepository.Update(orderItemExists);
            
            // Recalculate total price
            decimal totalPrice = 0;
            foreach (var item in order.OrderItems)
            {
                totalPrice += item.UnitPrice * item.Quantity;
            }
            order.TotalPrice = totalPrice;
        }
        else
        {
            orderItem.Status = OrderItemStatusExtensions.ToStringValue(OrderItemStatus.Pending);
            orderItem.Quantity = 1;
            orderItem.UnitPrice = product.Price;
            await _orderItemRepository.Add(orderItem);

            // Recalculate total price
            decimal totalPrice = 0;
            foreach (var item in order.OrderItems)
            {
                totalPrice += item.UnitPrice * item.Quantity;
            }
            order.TotalPrice = totalPrice;
        }
        
        await _orderRepository.Update(order);
        await SuspendChanges();
    }

    public async Task DeleteOrderItemById(int id)
    {
        var orderItem = await _orderItemRepository.GetSingleById(id);
        await _orderItemRepository.DeleteById(id);
        
        // Recalculate total price
        if (orderItem != null)
        {
            var order = await _orderRepository.GetMulti(
                    o => o.Id == orderItem.OrderId,
                    new string[] { "OrderItems" })
                .FirstOrDefaultAsync();
            if (order != null)
            {
                decimal totalPrice = 0;
                foreach (var item in order.OrderItems)
                {
                    totalPrice += item.UnitPrice * item.Quantity;
                }
                order.TotalPrice = totalPrice;
                await _orderRepository.Update(order);
            }
        }
        
        await SuspendChanges();
    }

    public async Task UpdateOrderItem(OrderItem orderItem)
    {
        await _orderItemRepository.Update(orderItem);
        
        // Recalculate total price
        var order = await _orderRepository.GetMulti(
                o => o.Id == orderItem.OrderId,
                new string[] { "OrderItems" })
            .FirstOrDefaultAsync();
        if (order != null)
        {
            decimal totalPrice = 0;
            foreach (var item in order.OrderItems)
            {
                totalPrice += item.UnitPrice * item.Quantity;
            }
            order.TotalPrice = totalPrice;
            await _orderRepository.Update(order);
        }
        
        await SuspendChanges();
    }

    public async Task RecalculateTotalPrice(int orderId)
    {
        Console.WriteLine("Recalculating total price of order");
        // Retrieve the order by ID
        var order = await _orderRepository.GetSingleById(orderId);
        if (order == null)
        {
            throw new ArgumentException("The specified order does not exist.");
        }

        // Get all order items for this order
        var orderItems = await _orderItemRepository.GetMulti(item => item.OrderId == orderId, new string[] { "Product" }).ToListAsync();

        // Recalculate the total price of the order based on the unit price and quantity of each order item
        decimal totalPrice = 0;
        foreach (var orderItem in orderItems)
        {
            totalPrice += orderItem.UnitPrice * orderItem.Quantity;
            Console.WriteLine("Order item quantity" + orderItem.Quantity);
        }

        // Update the total price of the order
        order.TotalPrice = totalPrice;
        Console.WriteLine("Total price:" + totalPrice);

        // Save changes
        await _orderRepository.Update(order);
        await SuspendChanges();
    }

    public void SaveChanges()
    {
        _unitOfWork.Commit();
    }

    public async Task SuspendChanges()
    {
        await _unitOfWork.CommitAsync();
    }

	public async Task<List<Order>> GetOrderByFilters(string? id, string? paymentMethods, string? branches, string? startDate, string? endDate)
	{
        var query = _orderRepository.GetAll()
            .Include(o => o.PaymentMethod)
            .Include(o => o.Branch)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .AsQueryable();
		if (id != null && int.TryParse(id, out var orderId))
		{
			query = query.Where(o => o.Id == orderId);
			return await query.ToListAsync();
		}

		if (paymentMethods != null)
		{
			var paymentMethodIds = paymentMethods.Split(',').Select(int.Parse).ToList();
			query = query.Where(o => paymentMethodIds.Contains(o.PaymentMethodId));
		}

		if (branches != null)
		{
			var branchIds = branches.Split(',').Select(int.Parse).ToList();
			query = query.Where(o => branchIds.Contains(o.BranchId));
		}

		if (startDate != null)
		{
			var start = DateTime.ParseExact(startDate, "dd/MM/yyyy" , CultureInfo.InvariantCulture);
			query = query.Where(o => o.UpdatedAt >= start);
		}

		if (endDate != null)
		{
			var end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1).AddTicks(-1);
			query = query.Where(o => o.UpdatedAt <= end);
		}

        return await query.ToListAsync();
	}
}