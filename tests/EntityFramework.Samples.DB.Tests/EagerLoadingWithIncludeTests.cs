namespace EntityFramework.Samples.DB.Tests;

public class EagerLoadingWithIncludeTests : EagerLoadDbTestBase
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
    public void EagerLoadingWithChildrenItemFilter()
    {
        using var dbContext = CreateDbContext();
        var orders = dbContext
            .Orders
            .Include(x => x.OrderItems.Where(item=>item.OrderPrice >= 5))
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
            .ThenInclude(x => x.Item)
            .Where(x => x.Id == 1)
            .ToList();

        Assert.Single(orders);
        var order = orders.First();
        Assert.NotNull(order.OrderItems);
        Assert.NotNull(order.OrderItems.First().Item);
    }

    [Fact]
    public void EagerLoadingWithGranChildrenFilter()
    {
        using var dbContext = CreateDbContext();
        var customerOrders = dbContext
            .CustomerOrders
            .Include(x => x.Order)
            .ThenInclude(x => x.OrderItems.Where(y => y.OrderPrice > 5))
            .ThenInclude(x => x.Item)
            .Where(x => x.Id == 1)
            .ToList();

        Assert.Single(customerOrders);
        var customerOrder = customerOrders.First();
        Assert.NotNull(customerOrder.Order);
        Assert.NotNull(customerOrder.Order.OrderItems.First().Item);
    }
}