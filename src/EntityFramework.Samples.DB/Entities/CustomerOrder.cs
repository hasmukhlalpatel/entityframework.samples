namespace EntityFramework.Samples.DB.Entities;

public class CustomerOrder
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public int OrderId { get; set; }

    public Customer Customer { get; set; }
    public Order Order { get; set; }
}