using AutoMapper;
using FastFoodManagement.Data.DTO.AuditLog;
using FastFoodManagement.Data.DTO.Branch;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers
{
    [Route("api/audit-log")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
		private readonly IAuditLogService _auditLogService;
		private readonly IMapper _mapper;

		public AuditLogController(IAuditLogService auditLogService, IMapper mapper)
		{
			_auditLogService = auditLogService;
			_mapper = mapper;
		}

		[HttpGet("recent")]
		public async Task<ActionResult<ApiResponse<List<AuditLogDTO>>>> GetAuditLogRecent()
		{
			try
			{
				var auditLogs = await _auditLogService.GetAuditLogRecent();
				var auditLogDTOs = _mapper.Map<List<AuditLogDTO>>(auditLogs);
				var response = ApiResponse<List<AuditLogDTO>>.SuccessResponse(auditLogDTOs, code: 200);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<RetrieveBranchDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}
	}
}
