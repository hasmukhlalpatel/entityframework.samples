namespace EntityFramework.Samples.DB.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public int ItemId { get; set; }

    public decimal OrderPrice { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
    public Order Order { get; set; }
    public Item Item { get; set; }
}