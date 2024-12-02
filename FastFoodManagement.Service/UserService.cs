using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastFoodManagement.Data.DTO.User;
using FastFoodManagement.Data.Infrastructure;
using FastFoodManagement.Data.Repositories;
using FastFoodManagement.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FastFoodManagement.Service;

public interface IUserService
{
    Task<User> RegisterUser(RegisterUserDTO userDto);
    Task<string> AuthenticateUser(string username, string password);
    Task<List<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task DeleteUserById(int id);
    Task<User> UpdateUser(User user);
    public void SaveChanges();
    public Task SuspendChanges();
}

public class UserService : IUserService 
{
    private IUserRepository _userRepository;
    private IRoleRepository _roleRepository;
    private IBranchRepository _branchRepository;
    private IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IBranchRepository branchRepository,
        IUnitOfWork unitOfWork
        )
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> RegisterUser(RegisterUserDTO userDto)
    {
        var existingUser = await _userRepository
            .GetMulti(
                user => user.Username == userDto.Username, 
                new string[] { "Role", "Branch" }
            )
            .FirstOrDefaultAsync();
        if (existingUser != null)
        {
            throw new Exception("User name exist");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

        var branch = await _branchRepository.GetSingleById(userDto.BranchId);
        if (branch == null)
        {
            throw new Exception("Invalid Branch.");
        }

        // Ensure the role exists before assigning it
        var role = await _roleRepository.GetSingleById(userDto.RoleId);
        if (role == null)
        {
            throw new Exception("Invalid Role.");
        }
        
        var newUser = new User
        {
            Name = userDto.Name,
            Username = userDto.Username,
            Password = hashedPassword,
            RoleId = userDto.RoleId,
            BranchId = userDto.BranchId,
            IsActive = true
        };
        
        if (userDto.Phone != null)
        {
            newUser.Phone = userDto.Phone;
        }
        if (userDto.Email != null)
        {
            newUser.Email = userDto.Email;
        }
        
        await _userRepository.Add(newUser);
        try
        {
            await SuspendChanges();
        }
        catch (Exception ex)
        {
            // Log the inner exception for more details
            var innerException = ex.InnerException?.Message ?? "No inner exception";
            throw new Exception($"Error committing changes: {ex.Message} Inner Exception: {innerException}");
        }
        
        return newUser;
    }

    public async Task<string> AuthenticateUser(string username, string password)
    {
        var user = await _userRepository
            .GetMulti(
                user => user.Username == username,
                new string[] { "Role", "Branch" }
            )
            .FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("User name not exist");
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new Exception("Invalid password");
        }

        return GenerateJwtToken(user);
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        List<User> entity = await _userRepository
            .GetMulti(
                u => true,
                new string[] { "Role", "Branch" })
            .ToListAsync();
        return entity;
    }
    
    public async Task<User> GetUserById(int id)
    {
        var user = await _userRepository
            .GetMulti(
                u => true,
                new string[] { "Role", "Branch" })
            .FirstOrDefaultAsync();
        return user;
    }
    
    public async Task DeleteUserById(int id)
    {
        await _userRepository.DeleteById(id);
        await SuspendChanges();
    }
    
    public async Task<User> UpdateUser(User user)
    {
        await _userRepository.Update(user);
        await SuspendChanges();
        return user;
    }
    
    public void SaveChanges()
    {
        _unitOfWork.Commit();
    }

    public async Task SuspendChanges()
    {
        await _unitOfWork.CommitAsync();
    }
    
    private string GenerateJwtToken(User user)
    {
        Console.WriteLine(user.Role.Name);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Name", user.Name),
            new Claim("Role Id", user.RoleId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim("Branch Id", user.BranchId.ToString()),
            new Claim("Branch", user.Branch.Name)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            claims: claims,
            expires: DateTime.Now.AddHours(48),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);  // Return JWT token as string
    }
}