using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EntityFramework.Samples.DB.InMemory.Tests;

public abstract class InMemoryDbTestBase
{
    readonly DbContextOptions<SampleShopDbContext> _contextOptions;
    protected InMemoryDbTestBase()
    {
        _contextOptions = new DbContextOptionsBuilder<SampleShopDbContext>()
            .UseInMemoryDatabase("SampleShopDbContextTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var dbContext = new TestSampleShopDbContext(_contextOptions);

        dbContext.Database.EnsureDeleted(); //to use same database names with multiple tests
        dbContext.Database.EnsureCreated();

        dbContext.AddRange(
            new Customer { Id = 1, Name = "Test" },
            new Customer { Id = 2, Name = "Test2" },
            new CustomerOrder { Id = 1, CustomerId = 1, OrderId = 1 },
            new Order { Id = 1, CreatedOn = DateTime.Today },
            new OrderItem { Id = 1, OrderId = 1, ItemId = 1, OrderPrice = 3.00m, CreatedOn = DateTime.Today },
            new OrderItem { Id = 2, OrderId = 1, ItemId = 2, OrderPrice = 5.50m, CreatedOn = DateTime.Today },
            new Item { Id = 1, Name = "TestItem1", Price = 1.23m },
            new Item { Id = 2, Name = "TestItem2", Price = 5.00m }
        );
        dbContext.SaveChanges();
    }

    protected SampleShopDbContext CreateDbContext() => new TestSampleShopDbContext(_contextOptions);
}