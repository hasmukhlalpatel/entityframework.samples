namespace EntityFramework.Samples.DB.InMemory.Tests;

public class SampleEagerLoadDbLoadTests : InMemoryDbTestBase
{
    [Fact]
    public void TestCreateDatabaseWithASampleCustomer()
    {
        using var dbContext = CreateDbContext();
        var customer = dbContext.Customers.First(x => x.Id == 1);
        Assert.NotNull(customer);
        Assert.Null(customer.CustomerOrders);
    }

    [Fact]
    public void TestCreateAnOrderWithASampleCustomer()
    {
        using var dbContext = CreateDbContext();
        var order = dbContext.Orders.First(x => x.Id == 1);
        Assert.NotNull(order);
    }
}