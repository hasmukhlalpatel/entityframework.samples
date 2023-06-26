# entityframework.samples
Entityframework samples

## Test setup with SQlite
in the unit tests, EF DB context requires to setup as below without DB connection string "Filename=:memory:"
```
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<SampleShopDbContext>()
            .UseSqlite(_connection)
            .Options;

        var dbContext = new TestSampleShopDbContext(_contextOptions);
```

or on the code with DBContext
```
public class SampleShopDbContext : DbContext
{
    public SampleShopDbContext(DbContextOptions options) 
        : base( options)
    {
        
    }

    public DbSet<Customer> Customers { get; set; }

    ...

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=SampleShop")
            .LogTo(s => Console.WriteLine($"Query:{s}"), new [] { DbLoggerCategory.Database.Command.Name })
            .EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
}
```
## Lazy loading setup

Lazy load will load object as an wehn require so operation will be n+1.
'UseChangeTrackingProxies' requires all entity types to be public, unsealed, have virtual properties, and have a public or protected constructor. 'UseLazyLoadingProxies' requires only the navigation properties be virtual. so Create entity with virtual keyword.

```
public class Order
{
    public int Id { get; set; }
    ...
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
```
It also requires a nuget pacakage 'Microsoft.EntityFrameworkCore.Proxies'.
Test setup will be as below with UseLazyLoadingProxies.
```
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<SampleShopDbContext>()
            .UseSqlite(_connection)
            .UseLazyLoadingProxies()
            .Options;

        var dbContext = new TestSampleShopDbContext(_contextOptions);

```


