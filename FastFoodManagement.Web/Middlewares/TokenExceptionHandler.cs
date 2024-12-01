using FastFoodManagement.Web.Common;
using Microsoft.IdentityModel.Tokens;

namespace FastFoodManagement.Web.Middlewares;

public class TokenExceptionHandler
{
    private readonly RequestDelegate _next;

    public TokenExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        Console.WriteLine("TokenExceptionHandler middleware executing...");
        Console.WriteLine(httpContext.Response.StatusCode);
        try
        {
            // Let the authentication middleware do its work
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error:" + ex.Message);
            // Catch specific authentication-related exceptions
            if (ex is SecurityTokenException || ex is UnauthorizedAccessException || 
                ex is ArgumentNullException || ex is ArgumentException)
            {
                httpContext.Response.StatusCode = 401;
                httpContext.Response.ContentType = "application/json";
                var response = new ApiResponse<string>
                {
                    Status = "Error",
                    Code = 401,
                    Success = false,
                    Message = "Authentication failed. Please check your token.",
                    Errors = new List<string> { ex.Message },
                    Data = null
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
            else
            {
                if (!httpContext.Response.HasStarted)
                {
                    httpContext.Response.StatusCode = 500;
                    httpContext.Response.ContentType = "application/json";
                    var response = new ApiResponse<string>
                    {
                        Status = "Error",
                        Code = 500,
                        Success = false,
                        Message = "An unexpected error occurred.",
                        Errors = new List<string> { ex.Message },
                        Data = null
                    };
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
                else
                {
                    Console.WriteLine("Response has already started, unable to modify it.");
                }
            }
        }
    }
}