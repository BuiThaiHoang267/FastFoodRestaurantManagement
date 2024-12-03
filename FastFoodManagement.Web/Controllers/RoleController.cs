using AutoMapper;
using FastFoodManagement.Data.DTO.Role;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers
{
	[Route("api/role")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		private readonly IMapper _mapper;
		public RoleController(IRoleService roleService, IMapper mapper)
		{
			_roleService = roleService;
			_mapper = mapper;
		}

		[HttpGet("all")]
		public async Task<ActionResult<RetrieveRoleDTO>> GetRoleAll()
		{
			try
			{
				var roles = await _roleService.GetAll();
				var roleDTOs = _mapper.Map<List<RetrieveRoleDTO>>(roles);
				var response = ApiResponse<List<RetrieveRoleDTO>>.SuccessResponse(roleDTOs, code: 200);
				return Ok(response);
			}
			catch (Exception ex)
			{
				var response = ApiResponse<RetrieveRoleDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
				return BadRequest(response);
			}
		}
	}
}
