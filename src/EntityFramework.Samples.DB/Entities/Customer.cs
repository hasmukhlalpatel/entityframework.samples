namespace EntityFramework.Samples.DB.Entities;
public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
}