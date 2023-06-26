namespace EntityFramework.Samples.DB.Tests;

public class SampleTests : DbTestBase
{
    [Fact]
    public void Test()
    {
        using var dbContext = CreateDbContext();
        var customer = dbContext.Customers.First(x => x.Id == 1);
        Assert.NotNull(customer);
    }
}