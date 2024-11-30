using AutoMapper;
using FastFoodManagement.Data.DTO.User;
using FastFoodManagement.Service;
using FastFoodManagement.Web.Common;
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
    
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<RetrieveUserDTO>>> RegisterUser(RegisterUserDTO registerUserDTO)
    {
        try
        {
            var user = await _userService.RegisterUser(registerUserDTO);
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
    public async Task<ActionResult<ApiResponse<RetrieveUserDTO>>> LoginUser(LoginUserDTO loginUserDTO)
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
}