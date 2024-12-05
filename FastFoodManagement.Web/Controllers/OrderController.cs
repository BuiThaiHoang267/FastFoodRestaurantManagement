using AutoMapper;
using FastFoodManagement.Data.DTO.Order;
using FastFoodManagement.Data.Enums;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<ApiResponse<List<RetrieveOrderDTO>>>> GetAllOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrders();
            
            var orderDTOs = _mapper.Map<List<RetrieveOrderDTO>>(orders);
            var response = ApiResponse<List<RetrieveOrderDTO>>.SuccessResponse(orderDTOs, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveOrderDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<RetrieveOrderDTO>>> GetOrderById(int id)
    {
        try
        {
            var order = await _orderService.GetOrderById(id);
            var orderDTO = _mapper.Map<RetrieveOrderDTO>(order);
            var response = ApiResponse<RetrieveOrderDTO>.SuccessResponse(orderDTO, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveOrderDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<CreateOrderDTO>>> CreateOrder(CreateOrderDTO orderDTO)
    {
        try
        {
			foreach (var orderItemDTO in orderDTO.OrderItems)
			{
				orderItemDTO.Status = OrderItemStatusExtensions.ToStringValue(OrderItemStatus.Pending);
			}

			var order = _mapper.Map<Order>(orderDTO);

            order.Status = OrderStatusExtensions.ToStringValue(OrderStatus.Pending);

			await _orderService.AddOrder(order);
            var response = ApiResponse<CreateOrderDTO>.SuccessResponse(orderDTO, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<CreateOrderDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteOrderById(int id)
    {
        try
        {
            await _orderService.DeleteOrderById(id);
            var response = new ApiResponse<RetrieveOrderDTO>(message: $"Order ${id} deleted successfully", code: 200, success: true);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveOrderDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpPatch("update/{id:int}")]
    public async Task<ActionResult<UpdateOrderDTO>> UpdateOrder(int id, UpdateOrderDTO orderDTO)
    {
        try
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                var response = ApiResponse<UpdateOrderDTO>.ErrorResponse("Order not found",
                    new List<string> { "Order not found" }, 404);
                return NotFound(response);
            }

            if (orderDTO.NumberOrder != null)
            {
                order.NumberOrder = (int)orderDTO.NumberOrder;
            }
            if (orderDTO.Status != null)
            {
                if (!OrderStatusExtensions.IsValidStatus(orderDTO.Status))
                {
                    var response = ApiResponse<UpdateOrderDTO>.ErrorResponse("Invalid status",
                        new List<string> { "Invalid status" }, 400);
                    return BadRequest(response);
                }
                else
                {
                    order.Status = orderDTO.Status;
                }
            }

            await _orderService.UpdateOrder(order);
            var response1 = ApiResponse<UpdateOrderDTO>.SuccessResponse(orderDTO, code: 200);
            return Ok(response1);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<UpdateOrderDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }

    [HttpGet("item/all/{id:int}")]
    public async Task<ActionResult<ApiResponse<List<RetrieveOrderItemDTO>>>> GetAllOrderItemsByOrderId(int id)
    {
        try
        {
            var orderItems = await _orderService.GetAllOrderItemsByOrderId(id);
            var orderItemDTOs = _mapper.Map<List<RetrieveOrderItemDTO>>(orderItems);
            var response = ApiResponse<List<RetrieveOrderItemDTO>>.SuccessResponse(orderItemDTOs, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveOrderItemDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpGet("item/{id:int}")]
    public async Task<ActionResult<ApiResponse<RetrieveOrderItemDTO>>> GetOrderItemById(int id)
    {
        try
        {
            var orderItem = await _orderService.GetOrderItemById(id);
            var orderItemDTO = _mapper.Map<RetrieveOrderItemDTO>(orderItem);
            var response = ApiResponse<RetrieveOrderItemDTO>.SuccessResponse(orderItemDTO, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveOrderItemDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpPost("item/create")]
    public async Task<ActionResult<ApiResponse<CreateOrderItemDTO>>> AddOrderItem(CreateOrderItemDTO orderItemDTO)
    {
        try
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDTO);
            await _orderService.AddOneOrderItem(orderItem);
            var response = ApiResponse<CreateOrderItemDTO>.SuccessResponse(orderItemDTO, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<CreateOrderItemDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpDelete("item/delete/{id:int}")]
    public async Task<ActionResult> DeleteOrderItemById(int id)
    {
        try
        {
            await _orderService.DeleteOrderItemById(id);
            var response = new ApiResponse<RetrieveOrderItemDTO>(message: $"Order item {id} deleted successfully", code: 200, success: true);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveOrderItemDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpPatch("item/update/{id:int}")]
    public async Task<ActionResult<UpdateOrderItemDTO>> UpdateOrderItem(int id, UpdateOrderItemDTO orderItemDTO)
    {
        try
        {
            var orderItem = await _orderService.GetOrderItemById(id);

            if (orderItemDTO.Quantity != null)
            {
                orderItem.Quantity = (int)orderItemDTO.Quantity;
            }
            if (orderItemDTO.Status != null)
            {
                if (!OrderItemStatusExtensions.IsValidStatus(orderItemDTO.Status))
                {
                    var response = ApiResponse<UpdateOrderItemDTO>.ErrorResponse("Invalid status",
                        new List<string> { "Invalid status" }, 400);
                    return BadRequest(response);
                }
                else
                {
                    orderItem.Status = orderItemDTO.Status;
                }
            }
            if (orderItemDTO.UnitPrice != null)
            {
                orderItem.UnitPrice = (decimal)orderItemDTO.UnitPrice;
            }

            await _orderService.UpdateOrderItem(orderItem);
            var response1 = ApiResponse<UpdateOrderItemDTO>.SuccessResponse(orderItemDTO, code: 200);
            return Ok(response1);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<UpdateOrderItemDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }

    [HttpGet]
	public async Task<ActionResult<List<RetrieveOrderDTO>>> GetOrderByFilters(
        string? id, string? paymentMethods, string? branches, string? startDate, string? endDate
        )
	{
		try
		{
			var orders = await _orderService.GetOrderByFilters(id, paymentMethods, branches, startDate, endDate);
			var orderDTOs = _mapper.Map<List<RetrieveOrderDTO>>(orders);
			var response = ApiResponse<List<RetrieveOrderDTO>>.SuccessResponse(orderDTOs, code: 200);
			return Ok(response);
		}
		catch (Exception ex)
		{
			var response = ApiResponse<RetrieveOrderDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
			return BadRequest(response);
		}
	}

    [HttpGet("status-pending/{branchId:int}")]
    public async Task<ActionResult<List<RetrieveOrderDTO>>> GetOrdersByStatusPending(int branchId)
	{
		try
		{
			var orders = await _orderService.GetOrderPending(branchId);
			var orderDTOs = _mapper.Map<List<RetrieveOrderDTO>>(orders);
			var response = ApiResponse<List<RetrieveOrderDTO>>.SuccessResponse(orderDTOs, code: 200);
			return Ok(response);
		}
		catch (Exception ex)
		{
			var response = ApiResponse<RetrieveOrderDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
			return BadRequest(response);
		}
	}

    [HttpGet("item/status-cooked/{branchId:int}")]
	public async Task<ActionResult<List<RetrieveOrderItemDTO>>> GetOrderItemsByStatusCooked(int branchId)
	{
		try
		{
			var orderItems = await _orderService.GetOrderItemsCooked(branchId);
			var orderItemDTOs = _mapper.Map<List<RetrieveOrderItemDTO>>(orderItems);
			var response = ApiResponse<List<RetrieveOrderItemDTO>>.SuccessResponse(orderItemDTOs, code: 200);
			return Ok(response);
		}
		catch (Exception ex)
		{
			var response = ApiResponse<RetrieveOrderItemDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
			return BadRequest(response);
		}
	}
}