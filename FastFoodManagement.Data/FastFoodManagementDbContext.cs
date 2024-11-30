using FastFoodManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastFoodManagement.Model.Abstract;

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

            modelBuilder.Entity<ComboItem>()
                .HasOne(ci => ci.Combo)
                .WithMany(ci => ci.ComboItems)
                .HasForeignKey(ci => ci.ComboId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ComboItem>()
                .HasOne(ci => ci.Product)
                .WithMany(ci => ci.ProductInComboItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) 
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Branch)
                .WithMany(b => b.Users)
                .HasForeignKey(u => u.BranchId)
                .OnDelete(DeleteBehavior.SetNull);
        }
        
        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }
        
        // This method will automatically set the CreatedAt and UpdatedAt fields
        private void SetAuditFields()
        {
            // Loop through all the entries in the change tracker
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable && 
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (IAuditable)entry.Entity;

                // Set CreatedAt only on new entities (State == Added)
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;  // Set to current UTC time
                }

                // Always set UpdatedAt on both Added and Modified entities
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
