using FastFoodManagement.Data.DTO.Statistic;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers
{
	[Route("api/statistic")]
	[ApiController]
	public class StatisticsController : ControllerBase
	{
		public IStatisticsService _statisticsService;
		public StatisticsController(IStatisticsService statisticsService)
		{
			_statisticsService = statisticsService;
		}

		[HttpGet("result-today")]
		public async Task<ActionResult<ResultSaleTodayDTO>> StatisticResultSaleToday()
		{
			try
			{
				var result = await _statisticsService.StatisticsSaleToday();
				var response = ApiResponse<ResultSaleTodayDTO>.SuccessResponse(result);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				var response = ApiResponse<ResultSaleTodayDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[HttpGet("revenue-chart")]
		public async Task<ActionResult<StatisticRevenueChartDTO>> StatisticRevenueChart([FromQuery] DateRangeDTO date)
		{
			try
			{
				var result = await _statisticsService.StatisticsRevenueChart(date);
				var response = ApiResponse<StatisticRevenueChartDTO>.SuccessResponse(result);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				var response = ApiResponse<StatisticRevenueChartDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}

		[HttpGet("top-product")]
		public async Task<ActionResult<TopProductDTO>> StatisticTopProduct([FromQuery] DateRangeDTO date)
		{
			try
			{
				var result = await _statisticsService.Top10Product(date);
				var response = ApiResponse<TopProductDTO>.SuccessResponse(result);
				return Ok(response);
			}
			catch (System.Exception ex)
			{
				var response = ApiResponse<TopProductDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}
	}
}
