namespace Bag.Application.Dtos.Orders;

public abstract class OrderManipulateModel
{
    public int CustomerId { get; set; }
    public int SellerId { get; set; }
    public decimal TotalAmount { get; set; }
}