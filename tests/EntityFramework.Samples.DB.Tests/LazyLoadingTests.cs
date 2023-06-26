namespace EntityFramework.Samples.DB.Tests;

public class LazyLoadingTests : LazyLoadDbTestBase
{
    [Fact]
    public void NoLazyLoadingWithOrdersAndOrderItems()
    {
        using var dbContext = CreateDbContext();
        var orders = dbContext
            .Orders
            .Include(x=>x.OrderItems)
            .Where(x => x.Id == 1)
            .ToList();

        Assert.Single(orders);
        var order = orders.First(); // will not fire new query to the database
        Assert.NotNull(order.OrderItems);
    }

    [Fact]
    public void LazyLoadingWithOrdersAndOrderItems()
    {
        using var dbContext = CreateDbContext();
        var orders = dbContext
            .Orders
            .Where(x => x.Id == 1)
            .ToList();

        Assert.Single(orders);
        var order = orders.First(); // will fire a new query to the database
        Assert.NotNull(order.OrderItems);
    }
}