using FastFoodManagement.Data;
using FastFoodManagement.Web.Extensions;
using FastFoodManagement.Web.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with connection string
builder.Services.AddDbContext<FastFoodManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Dependency Injection
builder.Services.InfrastructureDJ();
builder.Services.RepositoriesDJ();
builder.Services.ServicesDJ();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Auto Migration
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<FastFoodManagementDbContext>();
	dbContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
