namespace Bag.Domain.Entities;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }
    public int SellerId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public decimal TotalAmount { get; set; }

    public Customer Customer { get; set; } = null!;
    public Seller Seller { get; set; } = null!;
}