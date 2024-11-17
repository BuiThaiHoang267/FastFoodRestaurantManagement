using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodManagement.Data
{
    public class FastFoodManagementDbContext : DbContext
    {
        public FastFoodManagementDbContext(DbContextOptions<FastFoodManagementDbContext> options) : base(options)
        {

        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
		public DbSet<ComboItem> ComboItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite primary key for PurchaseInvoiceItem
            modelBuilder.Entity<PurchaseInvoiceItem>()
                .HasKey(pii => new { pii.PurchaseInvoiceId, pii.IngredientId });
            
            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(o => o.Employee)
                .WithMany(e => e.PurchaseInvoices)
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(o => o.Supplier)
                .WithMany(e => e.PurchaseInvoices)
                .HasForeignKey(o => o.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            // Additional model configuration here
        }
    }
}
