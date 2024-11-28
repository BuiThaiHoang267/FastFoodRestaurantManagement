using AutoMapper;
using FastFoodManagement.Data.DTO.PaymentMethod;
using FastFoodManagement.Model.Models;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers;

[Route("api/payment-method")]
[ApiController]
public class PaymentMethodController : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly IMapper _mapper;
    public PaymentMethodController(IPaymentMethodService paymentMethodService, IMapper mapper)
    {
        _paymentMethodService = paymentMethodService;
        _mapper = mapper;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<ApiResponse<List<RetrievePaymentMethodDTO>>>> GetAllPaymentMethods()
    {
        try
        {
            var paymentMethods = await _paymentMethodService.GetAllPaymentMethods();
            var paymentMethodDTOs = _mapper.Map<List<RetrievePaymentMethodDTO>>(paymentMethods);
            var response = ApiResponse<List<RetrievePaymentMethodDTO>>.SuccessResponse(paymentMethodDTOs, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrievePaymentMethodDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<RetrievePaymentMethodDTO>>> GetPaymentMethodById(int id)
    {
        try
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethodById(id);
            var paymentMethodDTO = _mapper.Map<RetrievePaymentMethodDTO>(paymentMethod);
            var response = ApiResponse<RetrievePaymentMethodDTO>.SuccessResponse(paymentMethodDTO, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrievePaymentMethodDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<CreatePaymentMethodDTO>>> CreatePaymentMethod(CreatePaymentMethodDTO createPaymentMethodDTO)
    {
        try
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(createPaymentMethodDTO);
            await _paymentMethodService.AddPaymentMethod(paymentMethod);
            var response = ApiResponse<CreatePaymentMethodDTO>.SuccessResponse(_mapper.Map<CreatePaymentMethodDTO>(paymentMethod), code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<CreatePaymentMethodDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePaymentMethodById(int id)
    {
        try
        {
            await _paymentMethodService.DeletePaymentMethodById(id);
            var response = new ApiResponse<RetrievePaymentMethodDTO>(message: "Payment method deleted successfully", code: 200, success: true);
            return Ok(response);
        }
        catch (Exception e)
        {
            var response = ApiResponse<RetrievePaymentMethodDTO>.ErrorResponse(e.Message, new List<string> { e.Message }, 500);
            return BadRequest(response);
        }
    }
}