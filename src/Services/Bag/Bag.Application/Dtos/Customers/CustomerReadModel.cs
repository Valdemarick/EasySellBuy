namespace Bag.Application.Dtos.Customers;

public class CustomerReadModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}