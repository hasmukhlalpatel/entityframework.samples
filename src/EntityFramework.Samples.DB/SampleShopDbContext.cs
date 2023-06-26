using EntityFramework.Samples.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Samples.DB; 
public class SampleShopDbContext : DbContext
{
    public SampleShopDbContext(DbContextOptions options) 
        : base( options)
    {
        
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<CustomerOrder> CustomerOrders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=SampleShop")
            .LogTo(s => Console.WriteLine($"Query:{s}"), new [] { DbLoggerCategory.Database.Command.Name })
            .EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
}
