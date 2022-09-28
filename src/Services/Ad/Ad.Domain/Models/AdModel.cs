using Ad.Domain.Models.Enums;

namespace Ad.Domain.Models;

public class Ad : BaseModel
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public int UserInfoId { get; set; }
    public int AddressId { get; set; }
    public States State { get; set; }
    public Categories Category { get; set; }

    public UserInfo UserInfo { get; set; } = null!;
    public Address Address { get; set; } = null!;
}