namespace EntityFramework.Samples.DB.Entities;
public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }
}

public class CustomerOrder
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public int OrderId { get; set; }

    public Customer Customer { get; set; }
    public Order Order { get; set; }
}

public class Order
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public List<OrderItem> OrderItems { get; set; }

}

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

public class Item
{
    public int Id { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }
}
