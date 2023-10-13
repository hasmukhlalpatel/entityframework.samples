namespace EntityFramework.Samples.DB.Tests
{
    public class EagerLoadingWithSelectManyTests : EagerLoadDbTestBase
    {
        [Fact]
        public void EagerLoadingOrderButOrderItemsOnly()
        {
            using var dbContext = CreateDbContext();
            var orderItems = dbContext
                .Orders
                .Where(x => x.Id == 1)
                .SelectMany(x => x.OrderItems )
                .ToList();

            Assert.Single(orderItems);
            var orderItem = orderItems.FirstOrDefault();
            Assert.Null(orderItem);
        }
    }
}