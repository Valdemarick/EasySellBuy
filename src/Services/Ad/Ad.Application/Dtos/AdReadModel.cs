using Ad.Domain.Models.Enums;

namespace Ad.Application.Dtos;

public class AdReadModel
{
    public int Id { get; set; } 
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTimeOffset CreatedOn { get; set; } 
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public States State { get; set; }
    public Categories Category { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
}