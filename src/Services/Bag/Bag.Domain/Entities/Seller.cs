namespace Bag.Domain.Entities;

public class Seller : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = null!;
}