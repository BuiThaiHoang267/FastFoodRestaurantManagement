using System.Collections;
using System.Net;
using System.Text;
using Azure.Core;
using DotNetEnv;
using FastFoodManagement.Data;
using FastFoodManagement.Web.Common;
using FastFoodManagement.Web.Extensions;
using FastFoodManagement.Web.Mappings;
using FastFoodManagement.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<FastFoodManagementDbContext>(option =>
//	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure DbContext with connection string
builder.Services.AddDbContext<FastFoodManagementDbContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
			ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
			// Hoàng s?a ch? này
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("Jwt:SecretKey is not set")))
		};
		// Handle authentication failure
		options.Events = new JwtBearerEvents
		{
			OnAuthenticationFailed = context =>
			{
				Console.WriteLine($"Authentication failed: {context.Exception.Message}");
				context.Response.StatusCode = 401;
				context.Response.ContentType = "application/json";

				var response = new ApiResponse<string>
				{
					Status = "Error",
					Code = 401,
					Success = false,
					Message = "Authentication failed. Invalid token or expired.",
					Errors = new List<string> { context.Exception.Message },
					Data = null
				};

				return context.Response.WriteAsJsonAsync(response);
			},
			OnChallenge = context =>
			{
				// Console.WriteLine(context.Response.StatusCode);
				// Console.WriteLine(context.Response.HasStarted);
				if (context.Response.StatusCode == 401)
				{
					Console.WriteLine("Token challenge triggered.");
					context.Response.ContentType = "application/json";
					var response = new ApiResponse<string>
					{
						Status = "Error",
						Code = 401,
						Success = false,
						Message = "You are not authorized to access this resource.",
						Errors = new List<string> { "Invalid or missing token." },
						Data = null
					};
					return context.Response.WriteAsJsonAsync(response);
				}
				return Task.CompletedTask;
			}
		};
	});


// Swagger
builder.Services.AddSwaggerGen(c =>
{
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter JWT with Bearer into field",
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

//Dependency Injection
builder.Services.InfrastructureDJ();
builder.Services.RepositoriesDJ();
builder.Services.ServicesDJ();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Add Configuration CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

// Auto Migration
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<FastFoodManagementDbContext>();
	dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Use authentication middleware
app.UseAuthentication();

app.UseMiddleware<TokenExceptionHandler>();

app.UseAuthorization();

app.MapControllers();

app.Run();
