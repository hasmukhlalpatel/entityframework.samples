namespace EntityFramework.Samples.DB.Tests
{
    public class EagerLoadingWithSelectTests : EagerLoadDbTestBase
    {
        [Fact]
        public void EagerLoadingWithOrderItems()
        {
            using var dbContext = CreateDbContext();
            var orders = dbContext
                .Orders
                .Where(x => x.Id == 1)
                .Select(x=> new { Order = x, x.OrderItems })
                .ToList();

            Assert.Single(orders);
            var order = orders.First();
            Assert.Null(order.OrderItems);
        }
    }
}