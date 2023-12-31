namespace EntityFramework.Samples.DB.Tests;
public abstract class EagerLoadDbTestBase: IDisposable
{
    readonly DbContextOptions<SampleShopDbContext> _contextOptions;
    readonly SqliteConnection _connection;
    protected EagerLoadDbTestBase()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<SampleShopDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var dbContext = new TestSampleShopDbContext(_contextOptions);
        if (dbContext.Database.EnsureCreated())
        {
            dbContext.AddRange(
                new Customer { Id = 1, Name = "Test" },
                new Customer { Id = 2, Name = "Test2" },
                new CustomerOrder{ Id = 1, CustomerId = 1, OrderId = 1},
                new Order { Id = 1, CreatedOn = DateTime.Today },
                new OrderItem { Id = 1, OrderId = 1, ItemId = 1, OrderPrice = 3.00m, CreatedOn = DateTime.Today },
                new OrderItem { Id = 2, OrderId = 1, ItemId = 2, OrderPrice = 5.50m, CreatedOn = DateTime.Today },
                new Item { Id = 1, Name = "TestItem1", Price = 1.23m },
                new Item { Id = 2, Name = "TestItem2", Price = 5.00m }
            );
            dbContext.SaveChanges();
        }
        //else
        //{
        //    Assert.Fail("Database must be created");
        //}
    }

    protected SampleShopDbContext CreateDbContext() => new TestSampleShopDbContext(_contextOptions);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
    public void Dispose() => _connection.Dispose();
}