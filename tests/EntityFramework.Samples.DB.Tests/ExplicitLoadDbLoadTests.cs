namespace EntityFramework.Samples.DB.Tests;

public class ExplicitLoadDbLoadTests : EagerLoadDbTestBase
{
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
}