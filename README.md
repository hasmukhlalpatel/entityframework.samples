# entityframework.samples
Entityframework samples

## Test setup with SQlite
in the unit tests, EF DB context requires to setup as below without DB connection string "Filename=:memory:"

```
public class Order
{
    public int Id { get; set; }
   
    public ICollection<OrderItem> OrderItems { get; set; }
}
```

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

## Explicit loading

```
    [Fact]
    public void LoadCustomerOrderWithCustomer()
    {
        using var dbContext = CreateDbContext();
        var customerOrders = dbContext.CustomerOrders.First(x => x.Id == 1);
        Assert.NotNull(customerOrders);
        dbContext.Entry(customerOrders).Reference(x=>x.Customer).Load();
    }

    [Fact]
    public void LoadOrderWithOrderItems()
    {
        using var dbContext = CreateDbContext();
        var order = dbContext.Orders.First(x => x.Id == 1);
        Assert.NotNull(order);
        dbContext.Entry(order).Collection(x => x.OrderItems).Load();
    }
```


## In-memory test with In-memory provider
In memory will require Microsoft.EntityFrameworkCore.InMemory pacakge t ouser as in memory database.
In-memory databases are identified by a simple, string name, and it's possible to connect to the same database several times by providing the same name (this is why the sample above must call EnsureDeleted before each test).
### Test testup
```
        _contextOptions = new DbContextOptionsBuilder<SampleShopDbContext>()
            .UseInMemoryDatabase("SampleShopDbContextTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var dbContext = new TestSampleShopDbContext(_contextOptions);

        dbContext.Database.EnsureDeleted(); //to use same database names with multiple tests
        dbContext.Database.EnsureCreated();
```
### Useful links

[Testing without your production database system](https://learn.microsoft.com/en-us/ef/core/testing/testing-without-the-database)
[Creating, seeding and managing a test database](https://learn.microsoft.com/en-us/ef/core/testing/testing-with-the-database)
