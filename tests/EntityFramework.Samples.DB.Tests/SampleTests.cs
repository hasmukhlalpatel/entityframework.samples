namespace EntityFramework.Samples.DB.Tests;

public class SampleTests : DbTestBase
{
    [Fact]
    public void TestCreateDatabaseWithASampleCustomer()
    {
        using var dbContext = CreateDbContext();
        var customer = dbContext.Customers.First(x => x.Id == 1);
        Assert.NotNull(customer);
    }

    [Fact]
    public void TestCreateAnOrderWithASampleCustomer()
    {
        using var dbContext = CreateDbContext();
        var order = dbContext.Orders.First(x => x.Id == 1);
        Assert.NotNull(order);
    }
}