namespace EntityFramework.Samples.DB.Tests;

public class EagerLoadingTests : DbTestBase
{
    [Fact]
    public void NoEagerLoadingWithOrdersAndOrderItems()
    {
        using var dbContext = CreateDbContext();
        var orders = dbContext
            .Orders
            .Where(x => x.Id == 1)
            .ToList();

        Assert.Single(orders);
        var order = orders.First();
        Assert.Null(order.OrderItems);
    }

    [Fact]
    public void EagerLoadingWithOrdersAndOrderItems()
    {
        using var dbContext = CreateDbContext();
        var orders = dbContext
            .Orders
            .Include(x => x.OrderItems)
            .Where(x => x.Id == 1)
            .ToList();

        Assert.Single(orders);
        var order = orders.First();
        Assert.NotNull(order.OrderItems);
        Assert.Null(order.OrderItems.First().Item);

    }

    [Fact]
    public void EagerLoadingWithOrdersAndOrderItemsAndItem()
    {
        using var dbContext = CreateDbContext();
        var orders = dbContext
            .Orders
            .Include(x => x.OrderItems)
            .ThenInclude(x=>x.Item)
            .Where(x => x.Id == 1)
            .ToList();

        Assert.Single(orders);
        var order = orders.First();
        Assert.NotNull(order.OrderItems);
        Assert.NotNull(order.OrderItems.First().Item);
    }
}