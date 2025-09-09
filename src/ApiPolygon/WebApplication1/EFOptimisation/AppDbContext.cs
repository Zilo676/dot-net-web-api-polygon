using Microsoft.EntityFrameworkCore;

namespace WebApplication1.EFOptimisation;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasIndex(x => x.CustomerId);
        modelBuilder.Entity<OrderItem>()
            .HasIndex(x => x.OrderId);
    }
}