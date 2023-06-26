namespace EntityFramework.Samples.DB.Entities;

public class Order
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }
    
    public DateTime UpdatedOn { get; set; }
    
    public virtual ICollection<OrderItem> OrderItems { get; set; }

}