﻿using FastFoodManagement.Data.DTO.Statistic;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers
{
	[Route("api/statistic")]
	[ApiController]
	public class StatisticsController : Controller
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
	}
}
