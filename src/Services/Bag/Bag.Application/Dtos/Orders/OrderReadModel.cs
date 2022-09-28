namespace Bag.Application.Dtos.Orders;

public class OrderReadModel
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerPhoneNumber { get; set; } = null!;
    public string SellerName { get; set; } = null!;
    public string SellerPhoneNumber { get; set; } = null!;
    public DateTimeOffset CreatedOn { get; set; }
    public decimal TotalAmount { get; set; }
}