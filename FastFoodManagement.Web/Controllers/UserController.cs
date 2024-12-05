using AutoMapper;
using FastFoodManagement.Data.DTO.User;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodManagement.Web.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [Authorize]
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<RetrieveUserDTO>>> RegisterUser(RegisterUserDTO registerUserDTO)
    {
        try
        {
            var user = await _userService.RegisterUser(registerUserDTO, User.FindFirst("Name")?.Value);
            var userDTO = _mapper.Map<RetrieveUserDTO>(user);
            var response = ApiResponse<RetrieveUserDTO>.SuccessResponse(userDTO, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<RetrieveUserDTO>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<string>>> LoginUser(LoginUserDTO loginUserDTO)
    {
        try
        {
            var token = await _userService.AuthenticateUser(
                loginUserDTO.Username,
                loginUserDTO.Password
            );
            var response = ApiResponse<string>.SuccessResponse(token, code: 200);
            return Ok(response);
        }
        catch (Exception ex)
        {
            var response = ApiResponse<string>.ErrorResponse(ex.Message, new List<string> { ex.Message }, 401);
            return Unauthorized(response);
        }
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<ApiResponse<List<RetrieveUserDTO>>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsers();
            var userDTOs = _mapper.Map<List<RetrieveUserDTO>>(users);
            var response = ApiResponse<List<RetrieveUserDTO>>.SuccessResponse(userDTOs, code: 200);
            return Ok(response);
        }
        catch (Exception e)
        {
            var response = ApiResponse<RetrieveUserDTO>.ErrorResponse(e.Message, new List<string> { e.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<RetrieveUserDTO>>> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            var userDTO = _mapper.Map<RetrieveUserDTO>(user);
            var response = ApiResponse<RetrieveUserDTO>.SuccessResponse(userDTO, code: 200);
            return Ok(response);
        }
        catch (Exception e)
        {
            var response = ApiResponse<RetrieveUserDTO>.ErrorResponse(e.Message, new List<string> { e.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUserById(int id)
    {
        try
        {
            await _userService.DeleteUserById(id, User.FindFirst("Name")?.Value);
            var response = new ApiResponse<RetrieveUserDTO>(message: $"User ${id} deleted successfully", code: 200, success: true);
            return Ok(response);
        }
        catch (Exception e)
        {
            var response = ApiResponse<RetrieveUserDTO>.ErrorResponse(e.Message, new List<string> { e.Message }, 500);
            return BadRequest(response);
        }
    }
    
    [Authorize]
    [HttpPatch("update/{id:int}")]
    public async Task<ActionResult<ApiResponse<RetrieveUserDTO>>> UpdateUser(int id, UpdateUserDTO updateUserDTO)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                var response = ApiResponse<RetrieveUserDTO>.ErrorResponse("User not found", new List<string> { "User not found" }, 404);
                return NotFound(response);
            }
            
            if (updateUserDTO.Name != null)
            {
                user.Name = updateUserDTO.Name;
            }
            if (updateUserDTO.Password != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDTO.Password);
            }
            if (updateUserDTO.Phone != null)
            {
                user.Phone = updateUserDTO.Phone;
            }
            if (updateUserDTO.Email != null)
            {
                user.Email = updateUserDTO.Email;
            }
            if (updateUserDTO.IsActive != null)
            {
                user.IsActive = updateUserDTO.IsActive.Value;
            }
            if (updateUserDTO.RoleId != null)
            {
                user.RoleId = updateUserDTO.RoleId.Value;
            }
            if (updateUserDTO.BranchId != null)
            {
                user.BranchId = updateUserDTO.BranchId.Value;
            }
            
            await _userService.UpdateUser(user, User.FindFirst("Name")?.Value);
            var userDTO = _mapper.Map<RetrieveUserDTO>(user);
            var response1 = ApiResponse<RetrieveUserDTO>.SuccessResponse(userDTO, code: 200);
            return Ok(response1);
        }
        catch (Exception e)
        {
            var response = ApiResponse<RetrieveUserDTO>.ErrorResponse(e.Message, new List<string> { e.Message }, 500);
            return BadRequest(response);
        }
    }

    [HttpGet()]
    public async Task<ActionResult<List<RetrieveUserDTO>>> GetUserByFilters
        (
            string? roles, 
            string? branches, 
            string? startDate, 
            string? endDate
        )
    {
        try
        {
            var users = await _userService.GetUserByFilters(roles, branches, startDate, endDate);
			var userDTOs = _mapper.Map<List<RetrieveUserDTO>>(users);
			var response = ApiResponse<List<RetrieveUserDTO>>.SuccessResponse(userDTOs);
			return Ok(response);
		}
        catch ( Exception e ) {
			var response = ApiResponse<RetrieveUserDTO>.ErrorResponse(e.Message, new List<string> { e.Message }, 500);
			return BadRequest(response);
		}
	}
}