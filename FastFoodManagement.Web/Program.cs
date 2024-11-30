using System.Collections;
using System.Text;
using DotNetEnv;
using FastFoodManagement.Data;
using FastFoodManagement.Web.Extensions;
using FastFoodManagement.Web.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")))
		};
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

app.UseAuthorization();

app.MapControllers();

app.Run();
