using FastFoodManagement.Data.DTO.Statistic;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers
{
	[Route("api/statistical-report")]
	[ApiController]
	public class StatisticalReportController : ControllerBase
	{
		private readonly IStatisticalReportService _statisticalReportService;

		public StatisticalReportController(IStatisticalReportService statisticalReportService)
		{
			_statisticalReportService = statisticalReportService;
		}

		[HttpGet("sale")]
		public async Task<ActionResult<StatisticSaleDTO>> StatisticsSale([FromQuery] string? branchId, [FromQuery] DateRangeDTO date)
		{
			try
			{
				var result = await _statisticalReportService.StatisticsSale(branchId, date);
				var response = ApiResponse<StatisticSaleDTO>.SuccessResponse(result);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				var response = ApiResponse<StatisticSaleDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[HttpGet("product")]
		public async Task<ActionResult<StatisticProductDTO>> StatisticsProduct([FromQuery] DateRangeDTO date, [FromQuery] string? branchId, [FromQuery] string? categoryId)
		{
			try
			{
				var result = await _statisticalReportService.StatisticsProduct(date, branchId, categoryId);
				var response = ApiResponse<StatisticProductDTO>.SuccessResponse(result);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				var response = ApiResponse<StatisticProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}
	}
}
